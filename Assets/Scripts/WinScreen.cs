using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    RectTransform m_ghost_transform;

    [SerializeField]
    Image background_image;

    [SerializeField]
    float duration = 3;

    [SerializeField]
    float delay_to_menu = 5;

    Vector2 m_initial_ghost_pos;
    Color m_final_background_color;
    Color m_initial_background_color;
    Image[] m_images;

    void Start()
    {
        m_initial_ghost_pos = m_ghost_transform.anchoredPosition;
        m_final_background_color = background_image.color;
        m_initial_background_color = new Color(background_image.color.r, background_image.color.g, background_image.color.b, 0);
        m_images = GetComponentsInChildren<Image>();
        background_image.color = m_initial_background_color;
        gameObject.SetActive(false);
    }

    public void LaunchWin()
    {
        GeneralAudioSource.Instance.PlayWinMusic();
        StartCoroutine(AnimateWin());
    }

    private IEnumerator AnimateWin()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            m_ghost_transform.anchoredPosition = Vector2.Lerp(m_initial_ghost_pos + new Vector2(300, 0), m_initial_ghost_pos, t);
            foreach (Image image in m_images) // TODO: optimize if necessary
            {
                Color init_color = new Color(image.color.r, image.color.g, image.color.b, 0);
                Color final_color = new Color(image.color.r, image.color.g, image.color.b, 1);
                image.color = Color.Lerp(init_color, final_color, t);
            }
            background_image.color = Color.Lerp(m_initial_background_color, m_final_background_color, t);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the end of the frame
        }

        // Ensure the final position is exactly at the target

        m_ghost_transform.anchoredPosition = m_initial_ghost_pos;
        foreach (Image image in m_images) // TODO: optimize if necessary
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, 1);
        }
        background_image.color = m_final_background_color;


        elapsedTime = 0f;
        while (elapsedTime < delay_to_menu)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the end of the frame
        }
        SceneManager.LoadScene(0);
        yield break;
    }
}
