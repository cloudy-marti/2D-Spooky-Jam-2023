using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float m_vertical_acceleration = 50f;

    [SerializeField]
    float m_downward_acceleration = 20f;

    [SerializeField]
    float m_jump_acceleration = 500f;

    Rigidbody m_rigidbody;
    bool m_pending_jump = false;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        m_pending_jump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        float horizontal_input = Input.GetAxis("Horizontal");
        float vertical_input = Input.GetAxis("Vertical");
        float down_input = Mathf.Clamp(vertical_input, -1, 0);
        Vector3 force = new Vector3(horizontal_input * m_vertical_acceleration, down_input * m_downward_acceleration, 0);
        m_rigidbody.AddForce(force);


        if (m_pending_jump)
        {
            m_rigidbody.AddForce(new Vector3(0, 1, 0) * m_jump_acceleration);
            m_pending_jump = false;
        }
    }
}
