using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform m_objectPlaceHolder;
    [SerializeField] private float m_invincibilityCooldown = 10f;
    [SerializeField] private float m_invincibilityDuration = 3f;
    [SerializeField] private float m_healthPoint = 100;

    private GameObject m_currentHandheldObject = null;
    private const string m_grabableObjectTag = "Grabbable";
    private const string m_interactableObjectTag = "Interactable";
    private float m_invicibilityCountdown = 0;
    private float m_invicibilityDurationCountdown = 0;
    private bool m_invincibilityReady = false;
    private bool m_canTakeDamage = true;
    private GameObject m_collidedObject = null;
    private GameObject m_collidedPlaceholder = null;

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

        m_healthPoint -= _damage;

        if (m_healthPoint > 0f)
        {
            return;
        }

        //TODO: GAME OVER
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

    /// <summary>
    /// Called by the Grab Command in the input manager
    /// </summary>
    public void OnInteract()
    {
        if (m_currentHandheldObject != null && m_collidedPlaceholder != null)
        {
            PlaceObject(m_collidedPlaceholder);
            return;
        }
        else if (m_currentHandheldObject == null && m_collidedObject != null)
        { 
            GrabObject(m_collidedObject);
        }
    }

    /// <summary>
    /// Grabs an object and place it at the same position as <seealso cref="m_objectPlaceHolder"/>
    /// </summary>
    /// <param name="_object">The Object to grab</param>
    /// <returns>True if the object was grabbed, false otherwise</returns>
    public bool GrabObject(GameObject _object)
    {
        if (_object.CompareTag(m_grabableObjectTag) == false)
        {
            return false;
        }
        m_currentHandheldObject = _object;
        _object.transform.parent = m_objectPlaceHolder;
        _object.transform.position = Vector3.zero;
        _object.transform.localPosition = Vector3.zero;
        return true;
    }

    /// <summary>
    /// Places an object previously grabbed and place it on the place holder.
    /// </summary>
    /// <param name="_placeHolder">The place holder where the object will be placed</param>
    /// <returns>True if the object was placed, false otherwise</returns>
    public bool PlaceObject(GameObject _placeHolder)
    {
        if (_placeHolder.CompareTag(m_interactableObjectTag) == false)
        {
            return false;
        }

        m_currentHandheldObject.transform.parent = _placeHolder.transform;
        m_currentHandheldObject.transform.localPosition = Vector3.zero;
        m_currentHandheldObject = null;
        return true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        m_collidedObject = collision.gameObject;
    }

    private void OnCollisionExit(Collision collision)
    {
        m_collidedObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_collidedPlaceholder = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        m_collidedPlaceholder = other.gameObject;
    }
}
