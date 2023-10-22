using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance => m_instance;
    private static EnemyManager m_instance;

    [SerializeField] private Enemy m_enemy;

    private Animator m_animator;

    private void Awake()
    {
        if (m_instance)
        {
            Destroy(gameObject);
            return;
        }

        m_instance = this;
    }

    private void Start()
    {
        m_animator = GetComponentInChildren<Animator>();
    }

    public void WakeUp()
    {
        m_animator.SetBool("isSleeping", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") == false || m_enemy.IsSleeping == true || m_enemy.IsChassing == true) return;

        m_enemy.Sleep();

        m_animator.SetBool("isSleeping", true);
    }
}
