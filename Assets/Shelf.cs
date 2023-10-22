using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    [SerializeField] List<GameObject> m_toDisable;
    [SerializeField] List<GameObject> m_toEnable;


    public void OnPlacedBook()
    {
        foreach (GameObject go in m_toDisable)
        {
            go.SetActive(false);
        }
        foreach (GameObject go in m_toEnable)
        {
            go.SetActive(true);
        }
    }
}
