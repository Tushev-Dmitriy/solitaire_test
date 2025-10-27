using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public bool IsReceiver = false;

    [SerializeField] private float ladderOffsetY;

    public void OnDrop(PointerEventData eventData)
    {
        var card = eventData.pointerDrag?.GetComponent<Card>();
        if (card == null) return;

        card.transform.SetParent(transform, false);

        if (IsReceiver)
        {
            card.transform.localPosition = new Vector3(0, 135, 0);
        }
        else
        {
            int childCount = transform.childCount;
            card.transform.localPosition = new Vector3(0, ladderOffsetY * (childCount - 1), 0);
        }
    }
}
