using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
public class EnemyController : MonoBehaviour
{
    public float viewRadius;
    public LayerMask playerLayer;
    public float chaseSpeed;
    public float patrolSpeed;
    public float patrolRange;
    public bool isPatrol;

    private Transform player;
    private bool isChasing;
    private Vector2 patrolTarget;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("ship").transform;
        isChasing = false;
        SetNewPatrolTarget();
    }

    void Update()
    {
        DetectPlayer();
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            if (isPatrol)
            {
                Patrol();
            }
        }
    }

    void DetectPlayer()
    {
        Collider2D[] detectedPlayer = Physics2D.OverlapCircleAll(transform.position, viewRadius, playerLayer);

        if (detectedPlayer.Length > 0)
        {
            isChasing = true;
        }
        else
        {
            isChasing = false;
        }
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)direction * chaseSpeed * Time.deltaTime;
    }

    void Patrol()
    {
        if (Vector2.Distance(transform.position, patrolTarget) < 0.5f)
        {
            SetNewPatrolTarget();
        }
        else
        {
            Vector2 direction = (patrolTarget - (Vector2)transform.position).normalized;
            transform.position += (Vector3)direction * patrolSpeed * Time.deltaTime;
        }
    }

    void SetNewPatrolTarget()
    {
        patrolTarget = (Vector2)transform.position + Random.insideUnitCircle * patrolRange;
    }
}
