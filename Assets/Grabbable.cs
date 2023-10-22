using UnityEngine;
using UnityEngine.Events;

public class Grabbable : MonoBehaviour
{
    [SerializeField] UnityEvent m_onPlacedCallback;
    [SerializeField] UnityEvent m_onPickedCallback;
    [SerializeField] UnityEvent m_onDroppedCallback;
    [SerializeField] GameObject m_receptacle;

    public void Start()
    {
        ParticleSystem particles = m_receptacle.GetComponent<ParticleSystem>();
        Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        if (sprite != null)
            particles.textureSheetAnimation.AddSprite(sprite);
    }

    public GameObject GetReceptacle()
    { 
        return m_receptacle;
    }

    public void OnObjectPlaced()
    {
        m_onPlacedCallback.Invoke();
        WinManager.GetWinManager().OnGrabbablePlaced();
        foreach (Collider collider in GetComponents<Collider>())
        {
            collider.enabled = false;
        }
    }

    public void OnObjectPicked()
    {
        m_onPickedCallback.Invoke();
    }

    public void OnObjectDropped()
    {
        m_onDroppedCallback.Invoke();
    }
}
