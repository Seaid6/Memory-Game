using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 initialPosition;

    // --- السطر الجديد هنا ---
    public int itemID;
    // -----------------------

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData) { }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // الصورة هترجع مكانها تلقائياً إلا لو سكريبت الخانة (Slot) ثبتها
        rectTransform.anchoredPosition = initialPosition;
    }

    // دالة هناديها من سكريبت الخانة عشان نثبت الصورة في مكانها الجديد لو الصح
    public void LockPosition(Vector2 newPos)
    {
        initialPosition = newPos;
        rectTransform.anchoredPosition = newPos;
    }
}