using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TextScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText;
    public float transition = 0f;
    public float transitionSpeed = 5f;
    public bool isHoverColour = false;
    public Color hoverColour;
    public Color normalColour;

    private void Start()
    {
        buttonText.color = normalColour;
    }

    private void Update()
    {
        float target = isHoverColour ? 1f : 0f;

        transition = Mathf.MoveTowards(transition, target, Time.deltaTime * transitionSpeed);

        buttonText.color = Color.Lerp(normalColour, hoverColour, transition);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHoverColour = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHoverColour = false;
    }
}
