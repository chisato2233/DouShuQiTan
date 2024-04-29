using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DouShuQiTan;
using UnityEngine;

public class UI_Msg_BattleStart : MonoBehaviour {
    public float MoveTime = 0.5f;
    public float IdelTime = 1.5f;
    public void Move() {
        StartCoroutine(Move_Impl());
        
    }

    IEnumerator Move_Impl() {
        var trans = GetComponent<RectTransform>();
        var initPos = trans.anchoredPosition.x;
        trans.DOAnchorPosX(0,MoveTime);
        yield return new WaitForSeconds(MoveTime + IdelTime);
        trans.DOAnchorPosX(-initPos, MoveTime).OnComplete(()=>gameObject.SetActive(false));
        
    }
}
