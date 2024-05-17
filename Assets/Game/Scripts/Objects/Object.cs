using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent (typeof(Rigidbody))]
public class Object : MonoBehaviour
{
    [SerializeField] int PickableRange = 3;

    protected void Start()
    {
        gameObject.tag = "Object";
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = PickableRange;
        GetComponent <Rigidbody>().isKinematic = true;
    }

    public virtual void PickUp()
    {
        //Override and Do the necessary to store in Inventory
    }
}
