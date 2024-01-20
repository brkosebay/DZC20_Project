using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>() ?? gameObject.AddComponent<CanvasGroup>();
        canvas = FindObjectOfType<Canvas>(); // Ideally, pass this reference more directly
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; // Optional: reduce opacity when dragging
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, eventData.position, canvas.worldCamera, out localPoint);

        // Adjust for the pivot point
        Vector2 pivotOffset = rectTransform.pivot - new Vector2(0.5f, 0.5f);
        Vector2 size = rectTransform.rect.size;
        pivotOffset = new Vector2(pivotOffset.x * size.x, pivotOffset.y * size.y);

        rectTransform.anchoredPosition = localPoint - pivotOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f; // Restore full opacity
    }
}
