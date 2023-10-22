using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField]
    float m_sprite_duration = 0.5f;

    [SerializeField]
    List<Sprite> m_body_sprites;

    SpriteRenderer m_sprite_renderer;
    float m_time_counter = 0;
    int m_body_sprite_id = 0;

    // Start is called before the first frame update
    void Start()
    {
        Assert.IsTrue(m_body_sprites.Count > 0);
        m_sprite_renderer = GetComponent<SpriteRenderer>();
        m_sprite_renderer.sprite = m_body_sprites[m_body_sprite_id];
    }

    // Update is called once per frame
    void Update()
    {
        m_time_counter += Time.deltaTime;
        if (m_time_counter > m_sprite_duration )
        {
            m_body_sprite_id = (m_body_sprite_id + 1) % m_body_sprites.Count;
            m_time_counter -= m_sprite_duration;
            m_sprite_renderer.sprite = m_body_sprites[m_body_sprite_id];
            //Debug.Log("switch");
        }
    }
}
