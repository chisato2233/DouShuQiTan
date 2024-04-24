using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MapScroll : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IScrollHandler {
    [Header("-----")]
    [SerializeField] private float scrollSpeed = 500f;
    [SerializeField] private float dragSpeed = 100f;
    [SerializeField] private float MinY = -10f;
    [SerializeField] private float MaxY = 10f;
    [SerializeField] private float EazyOffset = 100f;

    [Header("-----")]
    [SerializeField] private GameObject Sider;
    [SerializeField] private float ShowMapTime = 1.0f;

    [Header("-----")]
    private Vector2 dragStartPosition;
    private RectTransform rectTransform;
    private bool isDragging;

    public bool Enable = true;
    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }



    public void OnBeginDrag(PointerEventData eventData) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out dragStartPosition);
    }

    public void OnDrag(PointerEventData eventData) {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localCursor)) {
            Vector2 positionDelta = localCursor - dragStartPosition;
            positionDelta.x = 0;

            Vector2 newPosition = rectTransform.anchoredPosition + positionDelta * dragSpeed;
            newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
            rectTransform.anchoredPosition = newPosition;
        }
    }


    public void OnEndDrag(PointerEventData eventData) {
       
    }



    public void OnScroll(PointerEventData eventData) {
        Vector2 scrollDelta = eventData.scrollDelta * scrollSpeed;
        Vector2 newPosition = rectTransform.anchoredPosition - new Vector2(0, scrollDelta.y);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);
        rectTransform.anchoredPosition = newPosition;
    }

    public void UpdateLines() {

    }
    public void ShowMap() {
        var endPosition = transform.position;
        endPosition.y = 10;

        transform.DOMove(endPosition, ShowMapTime).OnUpdate(UpdateLines)
            .OnComplete(() => {
                Enable = true;
                StartCoroutine(Sider.GetComponent<TuLi>().AutoShow());
            });
    }

}
