using UnityEngine;
using FMODUnity;

public class FMOD3DSoundTest : MonoBehaviour
{
    public EventReference testEvent;

    void Update()
    {
      // Just attach this script to an object and then when you press T it will emit a sound in 3D
        if (Input.GetKeyDown(KeyCode.T))
        {
            FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(testEvent);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));
            instance.start();
            instance.release();
        }
    }
}