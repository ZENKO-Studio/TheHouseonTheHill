using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATECLOCFAN : MonoBehaviour
{
        public float SpeedX,SpeedY,SpeedZ;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate (SpeedX,SpeedY,SpeedZ);
    }
}
