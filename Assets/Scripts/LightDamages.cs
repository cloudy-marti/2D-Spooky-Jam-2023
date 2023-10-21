using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class LightDamages : MonoBehaviour
{
    [SerializeField]
    float m_damages = 10f;
    SphereCollider m_sphere_collider;

    private void Start()
    {
        m_sphere_collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 direction = other.transform.position - transform.position;
            RaycastHit hit_info;
            bool hit = Physics.Raycast(transform.position, direction, out hit_info, m_sphere_collider.radius);
            if (hit && hit_info.collider.gameObject.CompareTag("Player"))
            {
                Character character = other.gameObject.GetComponent<Character>(); // TODO: optimize if necessary
                character.TakeDamage(m_damages);
            }
        }
    }
}
