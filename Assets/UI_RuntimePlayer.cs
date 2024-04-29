using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DouShuQiTan;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UI_RuntimePlayer : MonoBehaviour {

    public float duration = 1.0f; // 浮动一个周期的持续时间
    public float displacement = 30.0f; // 浮动的距离

    private RuntimePlayer Player;
    private RectTransform RectTransform;


    void Awake() {
        Player = GetComponent<RuntimePlayer>();
        RectTransform = GetComponent<RectTransform>();
        StartIdleAnime();
    }




    void StartIdleAnime() {
        RectTransform.DOAnchorPosY(RectTransform.anchoredPosition.y + displacement, duration)
            .SetEase(Ease.InOutSine) // 设置缓动类型为正弦进出，使动画更平滑
            .SetLoops(-1, LoopType.Yoyo) // 设置循环类型为Yoyo，使动画往复进行，-1表示无限循环
            .SetRelative(true); // 设置为相对移动
    }
    
}
