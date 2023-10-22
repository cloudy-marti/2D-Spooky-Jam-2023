using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] int m_number_of_grabbables = 1;
    [SerializeField] WinScreen m_win_screen;

    int m_number_of_placed_grabbable;
    static WinManager active_win_manager = null;

    public static WinManager GetWinManager()
    {
        return active_win_manager;
    }

    public void Start()
    {
        m_number_of_placed_grabbable = 0;
        active_win_manager = this;
    }

    public void OnGrabbablePlaced()
    {
        m_number_of_placed_grabbable++;
        CheckWinConditions();
    }

    private void CheckWinConditions()
    {
        if(m_number_of_placed_grabbable >= m_number_of_grabbables)
        {
            m_win_screen.gameObject.SetActive(true);
            m_win_screen.LaunchWin();
        }
    }
}
