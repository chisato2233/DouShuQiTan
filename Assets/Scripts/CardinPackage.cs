using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CardinPackage : MonoBehaviour, IPointerClickHandler
{
    public int num;
    public CardPackage package;
    bool IsDestroy = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (package.IsForgetEvent || package.IsForgetRest)
        {
            AudioManager.Instance.PlaySfx("icon");
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(0, 0.7f));
            sequence.InsertCallback(0.5f, () =>
            {
                Destr();
            });
            //sequence.OnKill(Destr);
        }
        else if (package.IsUpGrade)
        {
            AudioManager.Instance.PlaySfx("icon");
            package.UpGraded(num);
        }
    }
    public void Destr()
    {
        package.Forgetted(num);

    }
}
