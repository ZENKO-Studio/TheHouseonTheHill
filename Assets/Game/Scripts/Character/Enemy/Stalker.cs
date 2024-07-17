using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;

[RequireComponent (typeof(StalkerFSM))]
public class Stalker : EnemyBase, IHear
{
    public Transform dest1;
    public Transform dest2;

    StalkerFSM fsm;
    public NavMeshAgent stalkerAgent;

    public Animator stalkerAnimator;

    public bool bPlayerSensed = false;
    public bool bSoundHeard = false;

    [SerializeField]
    public List<Vector3> soundPoint = new List<Vector3>();

    [SerializeField]
    public List<Transform> patrolPoints = new List<Transform>();

    public float chaseSpeed = 2;

    [SerializeField] int currIndex = 0;

    public Material stalkerMaterial;

    public Color emissionColor = Color.yellow;
    public float glowIntensity = 1.0f;

    protected override void Start()
    {
        stalkerAgent = GetComponent<NavMeshAgent>();
        stalkerAgent.stoppingDistance = attackRange;
        stalkerAgent.speed = moveSpeed;
        fsm = GetComponent<StalkerFSM>();
        stalkerAnimator = GetComponent<Animator>();
        
        stalkerMaterial = GetComponentInChildren<Renderer>().material;  

    }

    private void Update()
    {
        stalkerAnimator.SetFloat("MoveSpeed", stalkerAgent.velocity.magnitude);
    }

    #region Attacking Player
    //Checks continuosly
    void CheckForPlayer()
    {
        if (!bPlayerSensed) return;

        if (fsm.currentState == StalkerFSM.PatrolState || fsm.currentState == StalkerFSM.InvestigateState)
            fsm.ChangeState(StalkerFSM.ChasePlayerState);
    }

    public bool CanAttackPlayer()
    {
        return Vector3.Distance(transform.position, playerTransform.position) <= attackRange;
    }

    //Tobe called from animations
    private void OnAttack(AnimationEvent animationEvent)
    {
        Attack();
    }

    public override void Attack()
    {
        Debug.Log($"{gameObject.name} Attacking with damage of {damageToDeal * damageMultiplier}");

        if(playerTransform != null && CanAttackPlayer()) 
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
        if(fsm == null)
        {
            Debug.Log("FSM Null");
            return;
        }

        fsm.ChangeState(StalkerFSM.StunState);
        
    }
    #endregion
}
