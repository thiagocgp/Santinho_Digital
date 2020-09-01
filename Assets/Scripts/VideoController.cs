using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
    }

    public void Play()
    {
        PauseAudio(false);
        videoPlayer.Play();
    }

    public void Pause()
    {        
        PauseAudio(true);
        videoPlayer.Pause();        
    }

    private void PauseAudio(bool pause)
    {
        //for (ushort trackNumber = 0; trackNumber < videoPlayer.audioTrackCount; ++trackNumber)
        //{
            if (pause)
                videoPlayer.GetTargetAudioSource(0).Pause();
            else
                videoPlayer.GetTargetAudioSource(0).UnPause();
        //}
    }
}
