using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuClickEvents : MonoBehaviour
{
    public void PlayOnClick()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitOnClick()
    {
        Application.Quit();
    }
}
