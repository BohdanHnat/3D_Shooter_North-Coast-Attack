using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private string targetTag;
    [SerializeField] private float sightRange, attackRange;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private HealthComponent enemyHealth;
    [SerializeField] private GameObject corpsePrefab;
    public Transform target { get; private set; }

    private bool alreadyAttacked;

    private bool targetInSightRange, targetInAttackRange;

    public event Action onAttackingTarget;
    public event Action onDie;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        enemyHealth.OnEnemyDie += OnDie;
    }
    private void Update()
    {
        targetInSightRange = Physics.CheckSphere(transform.position, sightRange, targetMask);

        targetInAttackRange = Physics.CheckSphere(transform.position, attackRange, targetMask);

        if (!targetInSightRange && !targetInAttackRange) 
        {
            if (agent.enabled) agent.SetDestination(transform.position);

            animator.SetFloat("DirectionX", 0);
            animator.SetFloat("DirectionZ", 0);
        } 
        else if (targetInSightRange && !targetInAttackRange) ChaseTarget();
        else if (targetInAttackRange && targetInSightRange) AttackTarget();   
    }
    private void ChaseTarget()
    {
        animator.SetBool("IsShooting", false);
        weaponAnimator.SetBool("IsShooting", false);

        if (agent.enabled) agent.SetDestination(target.position);

        animator.SetFloat("DirectionX", 0);
        animator.SetFloat("DirectionZ", 1);
    }
    private void AttackTarget()
    {
        if (agent.enabled) agent.SetDestination(transform.position);

        animator.SetBool("IsShooting", true);
        weaponAnimator.SetBool("IsShooting", true);

        transform.LookAt(target);

        if (!alreadyAttacked)
        {
            onAttackingTarget?.Invoke();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void OnDie()
    {
        onDie?.Invoke();

        Context.Instance.AudioSystem.PlaySFX(new AudioData("face_hit_finisher_73", volume: 0.4f));
        
        agent.enabled = false;
        animator.SetBool("IsDead", true);

        StartCoroutine(DeathRoutine());       
    }
    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        Instantiate(corpsePrefab, new Vector3(transform.position.x, transform.position.y - 0.9f, transform.position.z), transform.rotation, null);
        
        Destroy(gameObject);
    }
}
