using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    //public VideoPlayer videoPlayer;
    public GameObject loadingGameObject;
    public MediaPlayerCtrl easyPlayer;

    private void Update()
    {
        /*if (videoPlayer.isPlaying)
        {
            loadingGameObject.SetActive(false);
        }*/
    }

    public void Play()
    {
        //PauseAudio(false);
        //videoPlayer.Play();
        easyPlayer.Play();
    }

    public void Pause()
    {
        //PauseAudio(true);
        //videoPlayer.Pause();
        easyPlayer.Pause();
    }

    private void PauseAudio(bool pause)
    {
        /*if (pause)
            videoPlayer.GetTargetAudioSource(0).Pause();
        else
            videoPlayer.GetTargetAudioSource(0).UnPause();*/
    }

    public void StopLoadingAnim()
    {
        loadingGameObject.SetActive(false);
    }
}
