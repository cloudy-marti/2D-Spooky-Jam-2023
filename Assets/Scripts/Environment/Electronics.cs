using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(Animator), typeof(AudioSource))]
public class Electronics : MonoBehaviour
{
    [SerializeField] UnityEvent<Transform> m_triggerCallback;
    [SerializeField] float m_idleTransitionTime;

    private Collider m_collider;
    private const string m_playerTag = "Player";
    private Transform m_selfTransform;
    private const string m_isActivatedKey = "isActivated";
    private Animator m_animator;
    private AudioSource m_audioSource;

    private void Start()
    {
        m_collider = GetComponent<Collider>();
        m_collider.isTrigger = true;
        m_selfTransform = transform;
        m_animator = GetComponent<Animator>();
        m_audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        m_triggerCallback.Invoke(m_selfTransform);
        m_animator.SetBool(m_isActivatedKey, true);
        m_audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        m_animator.SetBool(m_isActivatedKey, false);
        StartCoroutine(EndSound());
    }

    IEnumerator EndSound() 
    {
        yield return new WaitForSeconds(m_idleTransitionTime);
        m_audioSource.Stop();
    }
}
