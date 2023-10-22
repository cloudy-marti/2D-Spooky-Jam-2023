using UnityEngine;

public class WinManager : MonoBehaviour
{
    [SerializeField] WinScreen m_win_screen;
    [SerializeField] Character m_character;

    int m_number_of_placed_grabbable;
    static WinManager active_win_manager = null;
    private int m_number_of_grabbables = -1;

    public static WinManager GetWinManager()
    {
        return active_win_manager;
    }

    public void Start()
    {
        m_number_of_placed_grabbable = 0;
        active_win_manager = this;
        m_number_of_grabbables = FindObjectsByType<Grabbable>(FindObjectsSortMode.InstanceID).Length;
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
            m_character.GameOver();
            m_win_screen.LaunchWin();
        }
    }
}
