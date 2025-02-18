using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public float attackRange = 2f;
    public float attackCooldown = 1.5f;
    public int attackDamage = 10;
    public float detectionRange = 15f;

    private NavMeshAgent m_Agent;
    private float m_AttackTimer;
    private Transform m_Player;
    private Animator m_Animator;
    private Health m_Health;
    private bool m_IsDead;

    private void Start()
    {
        m_Health = GetComponent<Health>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponentInChildren<Animator>();
        m_Agent.speed = moveSpeed;
        m_Player = GameObject.FindGameObjectWithTag("Player").transform;

        m_Health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        m_IsDead = true;
       m_Animator.Rebind();
       GetComponent<Collider>().isTrigger = true;
       m_Agent.isStopped = true;
       m_Agent.updatePosition = false;
       m_Agent.updateRotation = false;
       m_Agent.velocity = Vector3.zero;
       m_Animator.SetTrigger("die");
       m_Agent.enabled = false;
    }

    private void Update()
    {
        if (m_Player == null || m_IsDead) return;
        
        m_AttackTimer -= Time.deltaTime;
        
        if(m_AttackTimer > 0) return;
        
        float distanceToPlayer = Vector3.Distance(transform.position, m_Player.position);

        if (distanceToPlayer <= detectionRange)
        {
            m_Agent.SetDestination(m_Player.position);
            m_Agent.isStopped = false;
            m_Animator.SetBool("walk", true);


            if (!(distanceToPlayer <= attackRange)) return;
            m_Agent.isStopped = true;
            m_Animator.SetTrigger("attack");
            AttackPlayer();
            m_AttackTimer = attackCooldown;
        }
        else
        {
            m_Agent.isStopped = true;
            m_Animator.SetBool("walk", false);
        }
    }

    private void AttackPlayer()
    {
        Health health = m_Player.GetComponent<Health>();
       if (health != null)
       {
            health.TakeDamage(attackDamage);
       }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
