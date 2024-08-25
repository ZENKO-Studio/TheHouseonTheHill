using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TimeLineHandler : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Reference to the VideoPlayer
    public int levelToStart; 
    public Canvas canvas; // Index of the level to load

    void Start()
    {
        // Subscribe to the VideoPlayer's loopPointReached event
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);  // Disable the canvas
        }
        // Change to the next scene when the video ends
        GameManager.Instance.StartLevel(levelToStart);
    }

    void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        videoPlayer.loopPointReached -= OnVideoEnd;
    }
}
