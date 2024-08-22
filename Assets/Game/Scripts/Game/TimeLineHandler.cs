using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeLineHandler : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public int levelToStart;

    void Start()
    {
        // Subscribe to the stopped event
        timelineDirector.stopped += OnTimelineStopped;
    }

    void OnTimelineStopped(PlayableDirector director)
    {
        if (director == timelineDirector)
        {
            GameManager.Instance.StartLevel(levelToStart);
        }
    }

    void OnDestroy()
    {
        // Unsubscribe when destroyed
        timelineDirector.stopped -= OnTimelineStopped;
    }
}
