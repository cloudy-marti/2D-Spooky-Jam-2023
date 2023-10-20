using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Transform m_objectPlaceHolder;

    GameObject m_currentHandheldObject = null;
    const string m_grabableObjectTag = "Grabbable";
    const string m_interactableObjectTag = "Interactable";

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
