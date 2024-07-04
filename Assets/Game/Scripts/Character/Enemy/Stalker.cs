using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof(StalkerFSM))]
public class Stalker : EnemyBase, IHear
{
    public Transform dest1;
    public Transform dest2;

    StalkerFSM fsm;
    public NavMeshAgent stalkerAgent;

    public bool bPlayerSensed = false;
    public bool bSoundHeard = false;

    [SerializeField]
    public List<Vector3> soundPoint = new List<Vector3>();

    [SerializeField]
    public List<Transform> patrolPoints = new List<Transform>();

    public float chaseSpeed = 2;

    [SerializeField] int currIndex = 0;

    protected override void Start()
    {
        stalkerAgent = GetComponent<NavMeshAgent>();
        stalkerAgent.stoppingDistance = attackRange;
        stalkerAgent.speed = moveSpeed;
    }

    #region Attacking Player
    //Checks continuosly
    void CheckForPlayer()
    {
        if (!bPlayerSensed) return;

        if (fsm.currentState == StalkerFSM.PatrolState || fsm.currentState == StalkerFSM.InvestigateState)
            fsm.ChangeState(StalkerFSM.ChasePlayerState);
    }

    public override void Attack()
    {
        Debug.Log($"{gameObject.name} Attacking with damage of {damageToDeal * damageMultiplier}");

        if(playerTransform != null) 
        {
            //#TODO: Check if stalker is in FOV of player and adjust the damage multiplier

            NellController playerRef = playerTransform.GetComponent<NellController>();
            if (playerRef != null)
            {
                playerRef.TakeDamage(damageToDeal * damageMultiplier);
            }
        }
        //Do all the visuals 
    }
    #endregion

    #region Patroling Related Code
    public Transform GetNextWaypoint()
    {
        if(currIndex == patrolPoints.Count)
        {
            currIndex = 0;
            RandomizePatrolPoints();
        }

        return patrolPoints[currIndex++];
    }

    void RandomizePatrolPoints()
    {
        for (int i = 0; i < patrolPoints.Count-1; i++)
        {
            Transform temp = patrolPoints[i];
            int randomIndex = Random.Range(i + 1, patrolPoints.Count - 1);
            patrolPoints[i] = patrolPoints[randomIndex];
            patrolPoints[randomIndex] = temp;
        }
    }
    #endregion

    #region Sound related Stuff
    public void RespondToSound(Sound sound)
    {
        bSoundHeard = true;
        soundPoint.Add(sound.pos);
        Debug.Log($"Stalker heard sound at {sound.pos}");
    }

    public void SoundInvestigated()
    {
        if (soundPoint.Count > 0)
            soundPoint.RemoveAt(0);

        if (soundPoint.Count == 0)
            bSoundHeard = false;
    }

    public Vector3 GetSoundPoint()
    {
        return soundPoint[0];
    }
    #endregion

    #region Getting Stunned
    public void GetStunned()
    {
        
        fsm.ChangeState(StalkerFSM.StunState);
        
    }
    #endregion
}
