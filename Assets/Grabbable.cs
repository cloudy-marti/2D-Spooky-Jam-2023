using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    [SerializeField] UnityEvent m_onPlacedCallback;
    [SerializeField] GameObject m_receptacle;

    public GameObject GetReceptacle()
    { 
        return m_receptacle;
    }

    public void OnObjectPlaced()
    {
        m_onPlacedCallback.Invoke();
    }
}
