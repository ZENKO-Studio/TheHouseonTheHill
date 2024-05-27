using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFacing : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
