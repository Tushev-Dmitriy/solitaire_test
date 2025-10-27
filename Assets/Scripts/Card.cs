using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _originalParent;
    private Canvas _canvas;

    private List<Transform> _draggedCards = new();

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalParent = transform.parent;
        _draggedCards.Clear();

        int myIndex = transform.GetSiblingIndex();
        for (int i = myIndex; i < _originalParent.childCount; i++)
        {
            _draggedCards.Add(_originalParent.GetChild(i));
        }

        foreach (var card in _draggedCards)
        {
            card.SetParent(_canvas.transform);
            var cg = card.GetComponent<CanvasGroup>();
            if (cg != null) cg.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        foreach (var card in _draggedCards)
        {
            var rt = card.GetComponent<RectTransform>();
            rt.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var dropZone = eventData.pointerEnter ? eventData.pointerEnter.GetComponentInParent<DropZone>() : null;

        if (dropZone == null)
        {
            ReturnToOriginalParent();
        }
        else
        {
            if (dropZone.IsReceiver && _draggedCards.Count > 1)
            {
                ReturnToOriginalParent();
                return;
            }

            foreach (var card in _draggedCards)
            {
                card.SetParent(dropZone.transform, false);
                var cg = card.GetComponent<CanvasGroup>();
                if (cg != null) cg.blocksRaycasts = true;
            }
        }
    }

    private void ReturnToOriginalParent()
    {
        foreach (var card in _draggedCards)
        {
            card.SetParent(_originalParent, false);
            var cg = card.GetComponent<CanvasGroup>();
            if (cg != null) cg.blocksRaycasts = true;
        }
    }
}
