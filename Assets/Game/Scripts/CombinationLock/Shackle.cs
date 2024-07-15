using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shackle : MonoBehaviour
{

    [Header("Animation Settings")] [SerializeField] private float yMovement;
    [SerializeField] private float yMovementDuration;
    [SerializeField] private float rotationAngle;
    [SerializeField] private float rotationDuration;
    private LTDescr _ltDescr;


    private void Start()
    {
        _ltDescr = LeanTween.rotateAroundLocal(gameObject, Vector3.up, rotationAngle, rotationDuration);
    }

    public void Open()
    {

        LeanTween.moveLocalY(gameObject, yMovement, yMovementDuration).setEase(LeanTweenType.easeOutBack).setOnComplete(
            () => _ltDescr
                .setEase(LeanTweenType.easeOutBack));


    }


}
