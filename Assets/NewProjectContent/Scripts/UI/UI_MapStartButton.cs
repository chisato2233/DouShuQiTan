using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class UI_MapStartButton : MonoBehaviour {
    private List<Image> images = new List<Image>();
    private Button button;
    public void Enable() {
        foreach (var i in images) {
            i.DOFade(1.0f, 0.5f).OnComplete(() => button.enabled = true);
        }
    }

    public void Disable() {
        button.enabled = false;
        foreach (var i in images) {
            i.DOFade(0.5f, 0.5f);
        }
    }

    public void Awake() {
        images.Add(GetComponent<Image>());
        images.Add(transform.parent.GetComponent<Image>());
        foreach(var i in images){
            var c =i.color;
            c.a = 0.5f;
           i.color = c;
        }

        button = GetComponent<Button>();
        button.enabled = false;
    }


}
