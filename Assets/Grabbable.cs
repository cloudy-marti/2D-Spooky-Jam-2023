using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Grabbable : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onPlacedCallback;
    [SerializeField] private UnityEvent m_onPickedCallback;
    [SerializeField] private UnityEvent m_onDroppedCallback;
    [SerializeField] private GameObject m_receptacle;
    [SerializeField] private AudioClip m_onDropSound;
    [SerializeField] private AudioClip m_onPlacedSound;
    [SerializeField] private AudioClip m_onPickupSound;
    AudioSource m_audioSource;

    public void Start()
    {
        ParticleSystem particles = m_receptacle.GetComponent<ParticleSystem>();
        Sprite sprite = GetComponentInChildren<SpriteRenderer>().sprite;
        if (sprite != null)
        { 
            particles.textureSheetAnimation.AddSprite(sprite);
        }
        m_audioSource = GetComponent<AudioSource>();
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
        m_audioSource.PlayOneShot(m_onPlacedSound);
    }

    public void OnObjectPicked()
    {
        m_onPickedCallback.Invoke();
        m_audioSource.PlayOneShot(m_onPickupSound);
    }

    public void OnObjectDropped()
    {
        m_onDroppedCallback.Invoke();
        m_audioSource.PlayOneShot(m_onDropSound);
    }
}
