using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using UnityEngine.AI;

public class StalkerBaseState : FSMBaseState<StalkerFSM>
{
    protected Stalker stalkerRef;
    protected NavMeshAgent agent;
    protected Transform stalkerTransform;
    protected Animator yBotAnimator;

    protected Vector3 lastPlayerPos;

    public override void Init(GameObject _owner, FSM _fsm)
    {
        base.Init(_owner, _fsm);

        stalkerRef = owner.GetComponent<Stalker>();
        Debug.Assert(stalkerRef != null, $"{owner.name} requires a Stalker component");

        agent = owner.GetComponent<NavMeshAgent>();
        Debug.Assert(agent != null, $"{owner.name} requires a NavMeshAgent component");

        stalkerTransform = owner.transform;

        yBotAnimator = owner.GetComponent<Animator>();
        Debug.Assert(yBotAnimator != null, $"{owner.name} requires a Animator component");
    }
}
