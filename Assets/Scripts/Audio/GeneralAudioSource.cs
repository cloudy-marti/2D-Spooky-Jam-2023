using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeneralAudioSource : MonoBehaviour
{
    public static GeneralAudioSource Instance => m_instance;
    private static GeneralAudioSource m_instance;

    [SerializeField] private AudioClip m_gameOver;

    private AudioSource m_audioSource;

    void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
        }

        m_instance = this;
    }

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameOverMusic()
    {
        m_audioSource.clip = m_gameOver;
        m_audioSource.Play();
    }

}
