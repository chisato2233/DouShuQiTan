using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEditor;
using UnityEngine;

public class UI_StoryControl : MonoBehaviour {
    public GameObject BackGround;
    public float MoveTime = 1.0f;
    
    TweenerCore<Color,Color,ColorOptions> ColorTween;
    public void ShowStory() {
        ColorTween = transform.Find("黑幕").GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
    }


    public void EndStory() { 
        ColorTween.Kill();
        BackGround.transform.position = new Vector2(transform.position.x, -10);
        BackGround.GetComponent<ScrollController>().UpdateLines();
        transform.Find("黑幕").GetComponent<SpriteRenderer>().DOFade(1, 0.5f).OnComplete(() => {
            BackGround.GetComponent<ScrollController>().ShowMap();
            gameObject.SetActive(false);
        });
    }

    void Start() {
        
    }



}
