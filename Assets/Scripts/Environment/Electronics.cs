using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Electronics : MonoBehaviour
{
    [SerializeField] UnityEvent<Transform> m_triggerCallback;
    private Collider m_collider;
    private const string m_playerTag = "Player";
    private Transform m_selfTransform;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
        m_selfTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        m_triggerCallback.Invoke(m_selfTransform);
    }
}
