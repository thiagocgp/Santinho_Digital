using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeSimplified : MonoBehaviour
{
    private YoutubePlayer player;

    public string url;
    private VideoPlayer videoPlayer;
    private bool firstTime = true;

    private void Awake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        player = GetComponentInChildren<YoutubePlayer>();
        player.videoPlayer = videoPlayer;
    }

    public void Play()
    {
        player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;
        if (firstTime)
        {
            player.Play(url);
            firstTime = false;
        }
        else
        {
            player.Play();
        }        
    }

    public void Pause()
    {
        player.Pause();
    }
}
