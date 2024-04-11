using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CardPackage : MonoBehaviour
{
    public List<GameObject> cards;
    public Rest rest;
    public Events events;
    public float ScrollSpeed;
    public float DragSpeed;
    Vector2 startPosition;
    Vector2 EndPosition;

    public bool IsForgetRest=false;
    public bool IsForgetEvent=false;
    public bool IsUpGrade=false;
    private void Awake()
    {
        cards.Clear();
        for(int i=0;i<GameData.HoldCard.Count && GameData.HoldCard.Count<=16;i++)
        {

            var card = Resources.Load<GameObject>("Test/" + ((CardType)GameData.HoldCard[i]).ToString());
            if (card != null)
            {
                card=Instantiate(card, transform.GetChild(1));
                if (card.GetComponent<CardModel>() != null)
                    Destroy(card.GetComponent<CardModel>());
                card.transform.localScale = card.transform.localScale * 1.1f;
                card.transform.position = transform.GetChild(1).GetChild(i).position;
                if (card.GetComponent<CardinPackage>() == null)
                {
                    card.AddComponent<CardinPackage>();
                    card.GetComponent<CardinPackage>().package = this;
                    card.GetComponent<CardinPackage>().num = i;

                }

                cards.Add(card);
            }
        }
    }

    public void Forgetted(int num)
    {
        var ForgettedCard = cards[num];
        cards.RemoveAt(num);
        Destroy(ForgettedCard);
        GameData.HoldCard.RemoveAt(num);
        GameData.IsUpGrade.RemoveAt(num);
        GameData.HasZhenXing.RemoveAt(num);
        GameData.ZhenXing.RemoveAt(num);
        for(int i=num;i<cards.Count;i++)
        {
            cards[i].transform.position = transform.GetChild(1).GetChild(i).position;
            cards[i].GetComponent<CardinPackage>().num = num;
        }
        if ( IsForgetRest)
        {
             IsForgetRest = false;
             rest.GetComponent<Animator>().Play("HeiPing");
            rest.node.save();
        }
        else if(IsForgetEvent)
        {
            IsForgetEvent = false;
            events.GetComponent<Animator>().Play("eventDisappear");
            events.node.save();
        }
        gameObject.SetActive(false);

    }
    public void UpGraded(int num)
    {
        IsUpGrade = false;
        GameData.IsUpGrade[num] = true;
        events.GetComponent<Animator>().Play("eventDisappear");
        events.node.save();
        gameObject.SetActive(false);
    }
    public void Changed()
    {
        if(GameData.HoldCard.Count>=2)
        {
            GetComponent<PackageScroll>().enabled = false;
            transform.GetChild(1).position = Vector3.zero;
            transform.GetChild(0).gameObject.SetActive(false);
            for(int i=0;i<transform.GetChild(1).childCount;i++)
            {
                transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
            }
                int random = Random.Range(0, GameData.HoldCard.Count);
                int num = Random.Range(0, GameData.HoldCard.Count);
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    while (random == num)
                        num = Random.Range(0, GameData.HoldCard.Count);
                    random = num;
                }
                GameData.HoldCard[random] = Random.Range(0, 29);
                GameData.HasZhenXing[random] = false;
                GameData.IsUpGrade[random] = false;
                //被换掉的卡牌
                var ChangedCard = cards[random];
                DG.Tweening.Sequence sequence = DOTween.Sequence();
                Vector3 startScale = ChangedCard.transform.localScale;
                ChangedCard.SetActive(true);
                if(i==0)
                    ChangedCard.transform.position = transform.GetChild(1).GetChild(1).position;
                else
                    ChangedCard.transform.position = transform.GetChild(1).GetChild(2).position;
                sequence.Append(ChangedCard.transform.DOScale(startScale, 1f));
                sequence.Append(ChangedCard.transform.DOScale(0, 0.5f));

                //新卡
                var Newcard = Resources.Load<GameObject>("Test/" + ((CardType)GameData.HoldCard[random]).ToString());
                if (Newcard != null)
                {
                    Newcard = Instantiate(Newcard, transform.GetChild(1));
                    if (Newcard.GetComponent<CardModel>() != null)
                        Destroy(Newcard.GetComponent<CardModel>());
                    Vector3 endscale = Newcard.transform.localScale * 1.1f;
                    Newcard.transform.localScale = Vector3.zero;
                    Newcard.transform.position = ChangedCard.transform.position;
                    sequence.Append(Newcard.transform.DOScale(endscale, 0.5f));
                    sequence.Append(Newcard.transform.DOScale(endscale, 1f));
                    if (Newcard.GetComponent<CardinPackage>() == null)
                    {
                        Newcard.AddComponent<CardinPackage>();
                        Newcard.GetComponent<CardinPackage>().package = this;
                        Newcard.GetComponent<CardinPackage>().num = random;
                    }

                    cards[random] = Newcard;
                }
                //Destroy(ChangedCard);
                sequence.InsertCallback(1.5f, () =>
                {
                    Destroy(ChangedCard);
                });
                if(i==1)
                    sequence.OnKill(ChangeEnd);
            }
            //events.GetComponent<Animator>().Play("eventDisappear");
            //events.node.save();
            //gameObject.SetActive(false);
        }

    }
    void ChangeEnd()
    {
        events.GetComponent<Animator>().Play("eventDisappear");
        events.node.save();
            transform.GetChild(0).gameObject.SetActive(true);
        for(int i=0;i<transform.GetChild(1).childCount;i++)
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(true);
        }
        for(int i=0;i<cards.Count;i++)
        {
            cards[i].transform.position= transform.GetChild(1).GetChild(i).position;
        }

        GetComponent<PackageScroll>().enabled = true;
        gameObject.SetActive(false);
    }


}
