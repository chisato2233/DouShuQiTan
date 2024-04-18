using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_咒印Toogle : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler {
    [SerializeField]
    private int Index = 0;
    
    private Image[] image;
    public ChouJiang 抽奖System;
    public bool enable = false;

    public void Awake() {
        image = GetComponents<Image>();
        foreach (var i in image) {
            var c = i.color;
            c.a = 0.5f;
            i.color = c;
        }
    }
    public void OnPointerEnter(PointerEventData eventData) {
        if (!enable) return;
        foreach (var i in image) {
            i.DOFade(1.0f, 0.3f);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!enable) return;
        foreach (var i in image) {
            i.DOFade(0.5f, 0.3f);
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!enable) return;
        抽奖System.PlayerChoose = Index;
        
    }
}
