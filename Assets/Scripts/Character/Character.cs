using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform m_objectPlaceHolder;
    [SerializeField] private float m_invincibilityCooldown = 10f;
    [SerializeField] private float m_invincibilityDuration = 3f;
    [SerializeField] private float m_healthPoint = 100;
    [SerializeField] private GameOverScreen m_gameover;

    private Grabbable m_currentHandheldObject = null;
    private const string m_grabableObjectTag = "Grabbable";
    private const string m_receptacleObjectTage = "Receptacle";
    private float m_invicibilityCountdown = 0;
    private float m_invicibilityDurationCountdown = 0;
    private bool m_invincibilityReady = false;
    private bool m_canTakeDamage = true;
    private GameObject m_collidedGrabbable = null;
    private GameObject m_collidedReceptacle = null;
    private bool m_gameOver = false;

    private float DROP_TORQUE = 5f;

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.E)) MakeInvincible();
        //if (Input.GetKeyDown(KeyCode.A)) TakeDamage(1f);

        // If we can take damage and our skill isn't ready, we are not using it, so we count the cooldown for the skill
        if (m_invincibilityReady == false && m_canTakeDamage == true)
        { 
            m_invicibilityCountdown += Time.deltaTime;
            if (m_invicibilityCountdown > m_invincibilityCooldown)
            { 
                m_invincibilityReady = true;
                m_invicibilityCountdown = 0f;
            }
        }

        // If we can't take damage and our skill isn't ready, we are using it, so we count the duration
        if (m_canTakeDamage == false && m_invincibilityReady == false)
        {
            m_invicibilityDurationCountdown += Time.deltaTime;
            if (m_invicibilityDurationCountdown > m_invincibilityDuration)
            {
                m_canTakeDamage = true;
                m_invicibilityDurationCountdown = 0f;
            }
        }
    }

    /// <summary>
    /// Take damage, only possible if <see cref="m_canTakeDamage"/> is true
    /// </summary>
    /// <param name="_damage"></param>
    public void TakeDamage(float _damage)
    {
        if (m_canTakeDamage == false)
        {
            return;
        }

        m_healthPoint = Mathf.Clamp(m_healthPoint - _damage, 0, m_healthPoint);

        if (m_healthPoint > 0f || m_gameOver == true)
        {
            return;
        }

        m_gameOver = true;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<PlayerInput>().enabled = false;
        m_gameover.gameObject.SetActive(true);
        m_gameover.LaunchGameOver();
    }

    /// <summary>
    /// Make yourself invincible, only possible if <see cref="m_invincibilityReady"/> is true
    /// </summary>
    public void MakeInvincible()
    {
        if (m_invincibilityReady == false)
        {
            return;
        }

        m_invincibilityReady = false;
        m_canTakeDamage = false;
    }

    public void OnUseSkill()
    {
        MakeInvincible();
    }

    /// <summary>
    /// Called by the Grab Command in the input manager
    /// </summary>
    public void OnInteract()
    {
        if (m_currentHandheldObject != null && m_collidedReceptacle != null)
        {
            PlaceObject(m_collidedReceptacle);
            return;
        }
        else if (m_currentHandheldObject == null && m_collidedGrabbable != null)
        { 
            GrabObject(m_collidedGrabbable);
        }
        else if (m_currentHandheldObject != null)
        {
            DropObject();
        }
    }

    /// <summary>
    /// Drop grabbed object if any.
    /// </summary>
    /// <returns>True if the object was dropped, false otherwise</returns>
    public bool DropObject()
    {
        if (m_currentHandheldObject == null)
            return false;

        m_currentHandheldObject.GetReceptacle().SetActive(false);
        m_currentHandheldObject.transform.parent = null;
        m_currentHandheldObject.OnObjectDropped();
        Rigidbody rigidbody = m_currentHandheldObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddTorque(new Vector3(0, 0, Random.Range(-1, 1) * DROP_TORQUE));
        m_currentHandheldObject = null;

        return true;
    }

    /// <summary>
    /// Grabs or drop an object
    /// </summary>
    /// <param name="_object">The Object to grab</param>
    /// <returns>True if the object was grabbed, false otherwise</returns>
    public bool GrabObject(GameObject _object)
    {
        if (_object.CompareTag(m_grabableObjectTag) == false || m_currentHandheldObject != null)
        {
            return false;
        }
        m_currentHandheldObject = _object.GetComponent<Grabbable>();
        m_currentHandheldObject.GetReceptacle().SetActive(true);
        m_currentHandheldObject.OnObjectPicked();
        _object.transform.parent = m_objectPlaceHolder;
        _object.transform.position = Vector3.zero;
        _object.transform.localPosition = Vector3.zero;
        _object.GetComponent<Rigidbody>().isKinematic = true;
        return true;
    }

    /// <summary>
    /// Places an object previously grabbed and place it on the place holder.
    /// </summary>
    /// <param name="_placeHolder">The place holder where the object will be placed</param>
    /// <returns>True if the object was placed, false otherwise</returns>
    public bool PlaceObject(GameObject _placeHolder)
    {
        if (_placeHolder.CompareTag(m_receptacleObjectTage) == false || m_currentHandheldObject.GetReceptacle() != _placeHolder)
        {
            return false;
        }

        m_currentHandheldObject.transform.parent = _placeHolder.transform;
        m_currentHandheldObject.transform.localPosition = Vector3.zero;
        m_currentHandheldObject.transform.localRotation = _placeHolder.transform.localRotation;
        foreach (Collider collider in m_currentHandheldObject.GetComponents<Collider>())
        {
            collider.enabled = false;
        }
        m_currentHandheldObject.OnObjectPlaced();
        m_currentHandheldObject = null;
        return true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        { 
            m_collidedGrabbable = other.gameObject;
        }

        if (other.gameObject.CompareTag("Receptacle"))
        { 
            m_collidedReceptacle = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_collidedGrabbable != null)
        { 
            m_collidedGrabbable = null;
        }

        if (m_collidedReceptacle != null)
        { 
            m_collidedReceptacle = null;
        }
    }
}
