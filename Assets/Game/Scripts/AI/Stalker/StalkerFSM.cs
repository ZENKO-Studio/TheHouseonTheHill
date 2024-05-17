using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StalkerFSM : FSM
{
    [SerializeField] TMP_Text statText;

    static public readonly int PatrolState = Animator.StringToHash("Patrol");
    static public readonly int ChasePlayerState = Animator.StringToHash("ChasePlayer");
    static public readonly int AttackState = Animator.StringToHash("Attack");
    static public readonly int InvestigateState = Animator.StringToHash("Investigate");
    static public readonly int InvestigateSoundState = Animator.StringToHash("InvestigateSound");
    static public readonly int StunState = Animator.StringToHash("Stun");

    Dictionary<int, string> stateDic; 

    private void Start()
    {
        stateDic = new Dictionary<int, string>();

        stateDic.Add(StalkerFSM.PatrolState, "Patrol");
        stateDic.Add(StalkerFSM.ChasePlayerState, "ChasePlayer");
        stateDic.Add(StalkerFSM.AttackState, "Attack");
        stateDic.Add(StalkerFSM.InvestigateState, "Investigate");
        stateDic.Add(StalkerFSM.InvestigateSoundState, "InvestigateSound");
        stateDic.Add(StalkerFSM.StunState, "Stun");

        statText.text = stateDic[fsmAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash];
    }

    public override bool ChangeState(int _stateName)
    {

        statText.text = stateDic[_stateName];

        return base.ChangeState(_stateName);
    }
}
