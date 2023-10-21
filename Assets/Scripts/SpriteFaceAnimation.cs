using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public enum FaceState
{
    chill,
    fear,
    angry
}

public class FaceSpriteAnimation : MonoBehaviour
{
    [SerializeField]
    Sprite m_sprite_chill;

    [SerializeField]
    Sprite m_sprite_fear;

    [SerializeField]
    Sprite m_sprite_angry;

    SpriteRenderer m_sprite_renderer;
    FaceState m_state;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite_renderer = GetComponent<SpriteRenderer>();
        m_state = FaceState.chill;
    }

    // Update is called once per frame
    void Update()
    {
        Sprite s = m_sprite_chill;
        switch (m_state)
        {
            case FaceState.chill:
                s = m_sprite_chill;
                break;
            case FaceState.fear:
                s = m_sprite_fear;
                break;
            case FaceState.angry:
                s = m_sprite_angry;
                break;
        }

        if (s != m_sprite_renderer.sprite)
            m_sprite_renderer.sprite = s;
    }

    public void SetState(FaceState state)
    {
        m_state= state;
    }
}
