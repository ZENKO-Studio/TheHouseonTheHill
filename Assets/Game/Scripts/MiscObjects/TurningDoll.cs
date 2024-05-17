using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will need an Player Object with Tag Player
/// </summary>

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class TurningDoll : MonoBehaviour
{
    [Tooltip("How far the player should be")]
    [SerializeField] float distanceThreshould;

    [Tooltip("How much the angle should be")]
    [SerializeField] float angleThreshould;

    [Tooltip("How fast the doll should turn")]
    [SerializeField] float rotationSpeed;

    Transform playerTransform = null;

    private void Start()
    {
        //Will 100% get it because it is required comp
        SphereCollider sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = distanceThreshould;
        sphereCollider.isTrigger = true;

        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if(Vector3.Angle(playerTransform.forward, (transform.position - playerTransform.position)) < angleThreshould)
        {
            Vector3 direction = (playerTransform.position - transform.position);

            direction.y = 0.0f;
            float angle = Vector3.Angle(transform.forward, direction);
            if (Mathf.Abs(angle) > 5f)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation,
                                                     Quaternion.LookRotation(direction),
                                                     Time.deltaTime * rotationSpeed);
            }
            else
            {
                transform.LookAt(transform.position + direction);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            playerTransform = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            playerTransform = null;
    }

}
