using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GeneralAudioSource : MonoBehaviour
{
    public static GeneralAudioSource Instance => m_instance;
    private static GeneralAudioSource m_instance;

    [SerializeField] private AudioClip m_gameOver;
    [SerializeField] private AudioClip m_mainMusic;

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
        m_audioSource.clip = m_mainMusic;
        m_audioSource.Play();
    }

    public void PlayGameOverMusic()
    {
        m_audioSource.clip = m_gameOver;
        m_audioSource.Play();
    }

    public void PauseMusic()
    {
        m_audioSource.Pause();
    }

    public void ResumeMusic() 
    {
        m_audioSource.UnPause();
    }
}
