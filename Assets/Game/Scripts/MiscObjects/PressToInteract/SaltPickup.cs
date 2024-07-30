using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltPickup : InteractableObject
{

    [Tooltip("How much salt should this pickup add")]
    [SerializeField] int quanitity = 1;

    public override void Interact()
    {
        GameManager.Instance.playerRef.saltChargeHandler.AddSalt(quanitity);

        Destroy(gameObject);
    }
}
