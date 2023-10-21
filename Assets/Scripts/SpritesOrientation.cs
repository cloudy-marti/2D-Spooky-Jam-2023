using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesOrientation : MonoBehaviour
{

    [SerializeField]
    List<SpriteRenderer> m_renderers;

    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rigidbody.velocity.x > 0)
            foreach (SpriteRenderer renderer in m_renderers)
                renderer.flipX = true;
        if (m_rigidbody.velocity.x < 0)
            foreach (SpriteRenderer renderer in m_renderers)
                renderer.flipX = false;
    }
}
