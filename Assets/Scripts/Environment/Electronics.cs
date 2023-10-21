using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Electronics : MonoBehaviour
{
    private Collider m_collider;
    private const string m_playerTag = "Player";

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        //TODO: TRIGGER AI PATH
    }
}
