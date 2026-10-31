using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ParentalGate : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdSeconds = 3f;
    public UnityEvent onConfirmed;

    private bool holding;
    private float timer;
    private Image fill;

    public void SetFillImage(Image image)
    {
        fill = image;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        holding = true;
        timer = 0f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        holding = false;
        if (fill != null)
            fill.fillAmount = 0f;
    }

    private void Update()
    {
        if (!holding)
            return;
        timer += Time.unscaledDeltaTime;
        if (fill != null)
            fill.fillAmount = Mathf.Clamp01(timer / holdSeconds);
        if (timer >= holdSeconds)
        {
            holding = false;
            if (onConfirmed != null)
                onConfirmed.Invoke();
        }
    }
}
