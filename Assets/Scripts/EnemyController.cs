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

    protected GameObject attackTarget;
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

    bool DetectPlayer()
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

        var colliders = Physics.OverlapSphere(transform.position, viewRadius); // var代表所有类型

        foreach (var target in colliders)
        {
            if (target.CompareTag("ship"))
            {
                attackTarget = target.gameObject;
                return true;
            }
        }

        attackTarget = null;
        return false;
    }

    void ChasePlayer()
    {
        transform.LookAt(attackTarget.transform);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: health--
        Destroy(gameObject);
    }
}
