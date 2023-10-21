using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableReceptacle : MonoBehaviour
{
    [SerializeField]
    GameObject m_receptacle;

    public GameObject get_receptacle()
    { return m_receptacle; }
}
