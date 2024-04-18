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
    private Vector3 StandardScale = new Vector2(0.318f,0.335f);
    private Vector2 StandardPositionOffset = new Vector2(-0.04f,0.328f);
    private Vector2[] showPos = new Vector2[] {
        new Vector2(3.5f, -0.88f),
        new Vector2(-2.92f, -1.08f)
    };
    private Vector3 ShowScale = new Vector3(0.362f, 0.393f, 0.083f);
    public bool IsForgetRest=false;
    public bool IsForgetEvent=false;
    public bool IsUpGrade=false;


    private void FixPosition(Transform card,Transform slot) {
        card.transform.localScale = StandardScale;
        card.transform.position = slot.position + new Vector3(StandardPositionOffset.x,StandardPositionOffset.y,0);
    }

    private void Awake() {
    
    cards.Clear();
        for(int i=0;i<GameData.HoldCard.Count && GameData.HoldCard.Count<=16;i++)
        {

            var card = Resources.Load<GameObject>("Test/" + ((CardType)GameData.HoldCard[i]).ToString());
            if (card != null)
            {
                card=Instantiate(card, transform.GetChild(1));
                if (card.GetComponent<CardModel>() != null)
                    Destroy(card.GetComponent<CardModel>());
                FixPosition(card.transform,transform.GetChild(1).GetChild(i));
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
            int random = Random.Range(0, cards.Count);
            int num = Random.Range(0, cards.Count);
            for (int i = 0; i < 2; i++)
            {
                if (i == 1)
                {
                    while (random == num)
                        num = Random.Range(0, cards.Count);
                    random = num;
                }
                GameData.HoldCard[random] = Random.Range(0, 29);
                GameData.HasZhenXing[random] = false;
                GameData.IsUpGrade[random] = false;
                //被换掉的卡牌
                var ChangedCard = cards[random];
                DG.Tweening.Sequence sequence = DOTween.Sequence();

                ChangedCard.SetActive(true);
                ChangedCard.transform.position = showPos[i];
                sequence.Append(ChangedCard.transform.DOScale(ShowScale, 1f));
                sequence.Append(ChangedCard.transform.DOScale(0, 0.5f));

                //新卡
                var Newcard = Resources.Load<GameObject>("Test/" + ((CardType)GameData.HoldCard[random]).ToString());
                if (Newcard != null)
                {
                    Newcard = Instantiate(Newcard, transform.GetChild(1));
                    if (Newcard.GetComponent<CardModel>() != null)
                        Destroy(Newcard.GetComponent<CardModel>());

                    Newcard.transform.localScale = Vector3.zero;
                    Newcard.transform.position = ChangedCard.transform.position;
                    sequence.Append(Newcard.transform.DOScale(ShowScale, 0.5f));
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
            FixPosition(cards[i].transform, transform.GetChild(1).GetChild(i));
        }

        GetComponent<PackageScroll>().enabled = true;
        gameObject.SetActive(false);
    }


}
