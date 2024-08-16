using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;


public class GotWood : MonoBehaviour
{

    public GameObject fireWood;
    public GameObject collider;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotWoods() {

        if (fireWood != null && Lua.ReferenceEquals(fireWood,1)) {

            Destroy(collider);

        }
    
    
    
    }
}
