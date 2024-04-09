using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP = 50;
    public float Defend = 0;
    public float MaxHp = 50;
    public GameObject HPText;
    public GameObject DefendText;
    public GameObject HPImage;
    private Vector3 HpStartPos;
    private Vector3 HpStart;
    public float HalfLenth;
    public GameObject Xieli;
    public GameObject LeiRuo;
    public GameObject Dun;

    //buff
    public int IsXieli = 0;
    public int IsLeiRuo = 0;

    private void Awake()
    {
        HP = GameData.HP;
        HpStartPos = HPImage.transform.position;
        HpStart = HPImage.transform.localScale;
    }
    void Start()
    {
        
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
        float newPosX1 = HP* HalfLenth / MaxHp ;
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
        if (IsLeiRuo > 0)
        {
            LeiRuo.SetActive(true);
        }
        else
        {
            LeiRuo.SetActive(false);
        }

    }
}
