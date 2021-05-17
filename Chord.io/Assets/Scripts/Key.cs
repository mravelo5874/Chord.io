using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Key : MonoBehaviour, 
    IPointerClickHandler,
    IDragHandler, 
    IPointerEnterHandler, 
    IPointerExitHandler
{
    public Note note;
    private bool isPressed;

    [SerializeField] private Image hoverOverlay;
    [SerializeField] private Image keyImage;

    private const float hoverAlpha = 25f / 255f;
    private float hoverTime = 0.1f;
    private float pressedTime = 0.1f;

    private Color hoverColor;
    private Color nonHoverColor;

    [SerializeField] private Color pressedColor;
    private Color nonPressedColor;

    private AudioSource audioSource;
    private AudioClip audioClip;

    void Awake()
    {
        // set hover colors
        nonHoverColor = new Color(0f, 0f, 0f, 0f);
        hoverColor = new Color(0f, 0f, 0f, hoverAlpha);
        hoverOverlay.color = nonHoverColor;
        // set pressed colors
        nonPressedColor = new Color(1f, 1f, 1f, 1f);
        
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0; //force 2D sound
        audioSource.Stop(); //avoids audiosource from starting to play automatically
        audioSource.loop = true;

        audioClip = AudioManager.instance.CreateSineClip(NoteData.instance.GetNoteFrequency(note));
        audioSource.clip = audioClip;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isPressed = !isPressed;
        StartCoroutine(PressKeyRoutine(isPressed));

        if (isPressed)
        {
            audioSource.Play();
        }
        else 
        {
            audioSource.Stop();
        }
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        
    }
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(HoverOvelayRoutine(true));
    }   
 
    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(HoverOvelayRoutine(false));
    }

    private IEnumerator HoverOvelayRoutine(bool hover)
    {
        float timer = 0f;
        Color start;
        Color end;

        if (hover)
        {
            start = nonHoverColor;
            end = hoverColor;
        }
        else 
        {
            start = hoverColor;
            end = nonHoverColor;
        }

        while (true)
        {
            timer += Time.deltaTime;
            if (timer > hoverTime)
            {
                hoverOverlay.color = end;
                break;
            }

            Color color = Color.Lerp(start, end, timer / hoverTime);
            hoverOverlay.color = color;
            yield return null;
        }
    }

    private IEnumerator PressKeyRoutine(bool pressed)
    {
        float timer = 0f;
        Color start;
        Color end;

        if (pressed)
        {
            start = nonPressedColor;
            end = pressedColor;
        }
        else 
        {
            start = pressedColor;
            end = nonPressedColor;
        }

        while (true)
        {
            timer += Time.deltaTime;
            if (timer > pressedTime)
            {
                hoverOverlay.color = end;
                break;
            }

            Color color = Color.Lerp(start, end, timer / pressedTime);
            keyImage.color = color;
            yield return null;
        }
    }
}
