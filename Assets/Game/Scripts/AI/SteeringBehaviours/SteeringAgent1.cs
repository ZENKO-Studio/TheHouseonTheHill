using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    public enum SummingMethod
    {
        WeightedAverage,
        Prioritized,
    };
    public SummingMethod summingMethod = SummingMethod.WeightedAverage;

    public float mass = 1.0f;
    public float maxSpeed = 1.0f;
    public float maxForce = 10.0f;
    public bool reachedGoal = false;

    public Vector3 velocity = Vector3.zero;

    public List<SteeringBehaviourBase> steeringBehaviours = new List<SteeringBehaviourBase>();

    public float angularDampeningTime = 5.0f;
    public float deadZone = 10.0f;

    private void Start()
    {
        steeringBehaviours.AddRange(GetComponentsInChildren<SteeringBehaviourBase>());
        foreach (SteeringBehaviourBase behaviour in steeringBehaviours)
        {
            behaviour.steeringAgent = this;
        }
    }

    private void Update()
    {
        Vector3 steeringForce = CalculateSteeringForce();
        steeringForce.y = 0.0f;

        Vector3 acceleration = steeringForce / mass;
        velocity = velocity + (acceleration * Time.deltaTime);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.magnitude > 0.0f)
        {
            Vector3 vector3 = new Vector3(velocity.x, 0.0f, velocity.z);
            float angle = Vector3.Angle(transform.forward, vector3);
            if (Mathf.Abs(angle) <= deadZone)
            {
                transform.LookAt(transform.position + vector3);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(vector3),
                                                      Time.deltaTime * angularDampeningTime);
            }
        }
    }

    private Vector3 CalculateSteeringForce()
    {
        Vector3 totalForce = Vector3.zero;

        foreach (SteeringBehaviourBase behaviour in steeringBehaviours)
        {
            if (behaviour.enabled)
            {
                switch (summingMethod)
                {
                    case SummingMethod.WeightedAverage:
                        totalForce = totalForce + (behaviour.CalculateForce() * behaviour.weight);
                        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
                        break;

                    case SummingMethod.Prioritized:
                        Vector3 steeringForce = (behaviour.CalculateForce() * behaviour.weight);
                        if (!AccumulateForce(ref totalForce, steeringForce))
                        {
                            return totalForce;
                        }
                        break;
                }

            }
        }

        return totalForce;
    }

    private bool AccumulateForce(ref Vector3 runningTotalForce, Vector3 forceToAdd)
    {
        float magnitudeSoFar = runningTotalForce.magnitude;
        float magnitudeRemaining = maxForce - magnitudeSoFar;

        if (magnitudeRemaining <= 0.0f)
        {
            return false;
        }

        float magnitudeToAdd = forceToAdd.magnitude;
        if (magnitudeToAdd < magnitudeRemaining)
        {
            runningTotalForce = runningTotalForce + forceToAdd;
        }
        else
        {
            runningTotalForce = runningTotalForce + (forceToAdd * magnitudeRemaining);
            return false;
        }

        return true;
    }
}
