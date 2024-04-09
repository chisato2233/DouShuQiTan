using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    public float Defend = 0;
    public float MaxHp;
    public GameObject HPImage;
    private Vector3 HpStartPos;
    private Vector3 HpStart;
    public float HalfLenth;
    public int type;//·ç0-2ÔÆ3-5¾«Ó¢6BOSS7
    public float attactBasic;
    public float defendBasic;
    public float AddValue;
    public float act1value;
    public float act2value;
    public GameObject HPText;
    public GameObject DefendText;
    public GameObject Xieli;
    public GameObject LeiRuo;
    public GameObject Dun;
    public GameObject enemyImage;

    //buff
    public int IsXieli = 0;
    public int IsLeiRuo = 0;

    private void Awake()
    {
        HpStartPos = HPImage.transform.position;
        HpStart = HPImage.transform.localScale;
        attactBasic = 3;
        defendBasic = 3;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(type == 7)
        {
            AddValue = 3;
            HP = 487;
            MaxHp = 487;
        }
        else if(type == 6)
        {
            AddValue = 2;
            HP = 199;
            MaxHp = 199;
        }
        else if(type == 0)
        {
            AddValue = 1;
            HP = 54;
            MaxHp = 54;
        }
        else if(type == 1)
        {
            AddValue = 1;
            HP = 97;
            MaxHp = 97;
        }
        else if(type == 2)
        {
            AddValue = 1;
            HP = 123;
            MaxHp = 123;
        }
        else if(type == 3)
        {
            AddValue = 1;
            HP = 60;
            MaxHp = 60;
        }
        else if(type == 4)
        {
            AddValue = 1;
            HP = 88;
            MaxHp = 88;
        }
        else if(type == 5)
        {
            AddValue = 1;
            HP = 133;
            MaxHp = 133;
        }
        else
        {
            AddValue = 1;
            HP = 55;
            MaxHp = 55;
        }
        if(GameData.NanDu == 2&&type!=7&&type!=6)
        {
            MaxHp += 40;
            HP += 40;
        }
        float startY = enemyImage.transform.localPosition.y;
        enemyImage.transform.localPosition -= new Vector3(0, 0.5f, 0);
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(enemyImage.transform.DOLocalMoveY(startY + 0.5f, 1.5f)/*.SetEase(Ease.OutExpo)*/);
        sequence.Append(enemyImage.transform.DOLocalMoveY(startY - 0.5f, 1.5f)/*.SetEase(Ease.OutExpo)*/);
        sequence.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            HP = 0;
        }
        HPText.GetComponent<TextMeshPro>().text = HP.ToString("F0") + "/" + MaxHp.ToString("F0");
        if (Defend <= 0)
        {
            DefendText.GetComponent<TextMeshPro>().text = null;
            Dun.SetActive(false);
        }
        else
        {
            DefendText.GetComponent<TextMeshPro>().text =/* "·ÀÓù:" + */Defend.ToString("F0");
            Dun.SetActive(true);
        }
        float newX = HP * HpStart.x / MaxHp;
        HPImage.transform.localScale = new Vector3(newX, HpStart.y, HpStart.z);
        float newPosX1 = HP * HalfLenth / MaxHp;
        float newPosX = HalfLenth - newPosX1;
        HPImage.transform.position = new Vector3(HpStartPos.x - newPosX, HpStartPos.y, 0);
        if (IsXieli>0)
        {
            Xieli.SetActive(true);
        }
        else
        {
            Xieli.SetActive(false);
        }
        if (IsLeiRuo>0)
        {
            LeiRuo.SetActive(true);
        }
        else
        {
            LeiRuo.SetActive(false);
        }
    }
}
