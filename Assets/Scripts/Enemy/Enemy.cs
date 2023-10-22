using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float m_damagePerSecond = 20f;
    [SerializeField] private float m_searchingDuration = 5f;
    [SerializeField] private Transform m_startingTransform;
    [SerializeField] private float m_stoppingDistance = 2f;

    public bool IsSleeping => m_isSleeping;
    public bool IsChassing => m_isChassing;

    private NavMeshAgent m_navMeshAgent;
    private const string m_playerTag = "Player";
    private Animator m_animator;
    private SpriteRenderer m_spriteRenderer;
    private Light m_spotLight;
    private Collider m_visionCollider;
    private Coroutine m_currentSearchingCoroutine;
    private bool m_isSleeping = false;
    private bool m_isChassing = false;

    private void Start()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_animator = GetComponentInChildren<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_spotLight = GetComponentInChildren<Light>();
        m_visionCollider = GetComponentInChildren<Collider>();
    }

    IEnumerator StartSearching()
    {
        yield return new WaitUntil(() => m_navMeshAgent.remainingDistance <= m_stoppingDistance && m_navMeshAgent.hasPath);
        m_navMeshAgent.isStopped = true;
        yield return new WaitForSeconds(m_searchingDuration);
        m_navMeshAgent.isStopped = false;

        m_navMeshAgent.SetDestination(m_startingTransform.position);
        m_currentSearchingCoroutine = null;
        m_animator.SetBool("isWalking", true);
    }

    private void Update()
    {
        if (m_navMeshAgent.velocity.x > 0 && m_spriteRenderer.flipX == false)
        {
            m_spriteRenderer.flipX = true;
        }
        else if (m_navMeshAgent.velocity.x < 0 && m_spriteRenderer.flipX == true)
        { 
            m_spriteRenderer.flipX = false;
        }
    }

    public void Sleep()
    {
        m_spotLight.enabled = false;
        m_visionCollider.enabled = false;
        m_spriteRenderer.enabled = false;
        m_isSleeping = true;
    }

    public void WakeUp()
    {
        m_spotLight.enabled = true;
        m_visionCollider.enabled = true;
        m_spriteRenderer.enabled = true;
        m_isSleeping = false;
    }

    public void MoveToPosition(Transform _transform)
    {
        if (m_currentSearchingCoroutine != null) return;

        m_navMeshAgent.SetDestination(_transform.position);
        m_currentSearchingCoroutine = StartCoroutine(StartSearching());
        m_animator.SetBool("isWalking", true);
        m_isChassing = true;
        if (m_isSleeping)
        {
            WakeUp();
            EnemyManager.Instance.WakeUp();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        if (Physics.Raycast(other.transform.position, transform.position) == true)
        { 
            m_navMeshAgent.isStopped = false;
            return; 
        }
        other.GetComponent<Character>().TakeDamage(m_damagePerSecond * Time.deltaTime);
        m_navMeshAgent.isStopped = true;
        m_animator.SetBool("isWalking", false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_playerTag) == false) return;
        m_navMeshAgent.isStopped = false;
        m_animator.SetBool("isWalking", true);
        m_isChassing = false;
    }
}
