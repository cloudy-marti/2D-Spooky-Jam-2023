using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler: MonoBehaviour
{
    [SerializeField]
    GameObject m_door_opened;

    [SerializeField]
    GameObject m_door_closed;

    public void Open()
    {
        m_door_closed.SetActive(false);
        m_door_opened.SetActive(true);
    }

    public void Close()
    {
        m_door_closed.SetActive(true);
        m_door_opened.SetActive(false);
    }
}
