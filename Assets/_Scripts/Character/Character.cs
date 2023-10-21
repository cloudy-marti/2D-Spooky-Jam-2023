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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) MakeInvincible();
        if (Input.GetKeyDown(KeyCode.A)) TakeDamage(1f);

        if (m_invincibilityReady == false && m_canTakeDamage == true)
        { 
            m_invicibilityCountdown += Time.deltaTime;
            if (m_invicibilityCountdown > m_invincibilityCooldown)
            { 
                m_invincibilityReady = true;
                m_invicibilityCountdown = 0f;
            }
        }

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

        _object.transform.parent = m_objectPlaceHolder;
        _object.transform.position = Vector3.zero;
        return true;
    }

    /// <summary>
    /// Places an object previously grabbed and place it on the place holder.
    /// </summary>
    /// <param name="_placeHolder">The place holder where the object will be placed</param>
    /// <returns>True if the object was placed, false otherwise</returns>
    public bool PlaceObject(GameObject _placeHolder)
    {
        if (_placeHolder.CompareTag(m_interactableObjectTag) == false || m_currentHandheldObject == null)
        {
            return false;
        }

        _placeHolder.transform.parent = m_currentHandheldObject.transform;
        _placeHolder.transform.position = Vector3.zero;
        return true;
    }
}
