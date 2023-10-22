using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class TV : MonoBehaviour
{

    private VideoPlayer m_videoPlayer;

    private void Start()
    {
        m_videoPlayer = GetComponent<VideoPlayer>();
    }

    IEnumerator ResumeMainMusic()
    {
        yield return new WaitUntil(() => m_videoPlayer.isPlaying == true);
        yield return new WaitUntil(() => m_videoPlayer.isPlaying == false);
        GeneralAudioSource.Instance.ResumeMusic();
    }
    public void PlayVideo()
    {
        GeneralAudioSource.Instance.PauseMusic();
        m_videoPlayer.Play();
        StartCoroutine(ResumeMainMusic());
    }

}
