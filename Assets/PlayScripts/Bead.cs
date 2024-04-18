using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Bead : MonoBehaviour, 
                    IPointerDownHandler, 
                    IPointerClickHandler, 
                    IPointerEnterHandler, 
                    IPointerExitHandler
{
    //一个珠子的特质就是属性、等级、大小
    //attribute[0]为显示出来的属性，[1]为隐藏属性，0无1土2水3火4风
    public int[] attribute;
    public int grade;//显示等级
    public int grade0;//实际等级
    public int extraGrade;//额外等级
    public int onceUpGrade;//每次升级等级
    public int extraRandom;//额外进化概率
    public int plusGrade;
    public int[] ShuXingGaiLv;//进化属性概率0无1土2水3火4风
    public int useNum;
    public GameStart m_game;
    public GameObject m_grid;
    //正常大小就是在盘里的大小
    //public enum scale { big,small,common};
    //public scale beadscale;
    public Vector3 commonScale;
    //public Vector3 bigScale;
    //public Vector3 smallScale;

    private Camera cam = null;
    public bool IsUse;//是否正在放置
    public bool InZhenXing = false;
    public bool onTheTop = false;
    public bool EnableMove = true; //是否可以移动

    //属性图
    public GameObject TuDi, ShuiDi, HuoDi, FengDi, TuDing, ShuiDing, HuoDing, FengDing;
    //等级显示
    public GameObject gradeText;
    //距离升级次数
    public GameObject NeedUseText;
    private float tm;
    //public int InGridNum = -1;
    List<int> attri;
    // Start is called before the first frame update


    private Vector3 startPosition;
    private Transform startParent;
    public void OnInitGrid(GameObject grid)
    {
        m_grid = grid;
    }
    private void Awake()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        attri = new List<int> { 0, 1, 2 ,3, 4};
        //attribute = new int[2];
        commonScale = gameObject.transform.localScale;
        //bigScale = commonScale * 1.2f;//大珠子是正常珠子的几倍
        //smallScale = 0.9f * commonScale;//小珠子是正常珠子的几倍

    }
    void Start()
    {
        //attribute = new int[2];
        //attribute[0] = 0;
        //attribute[1] = 0;

        //grade = 1;
        //无属性显示图片（颜色
        //gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        //plusGrade = extraGrade;
        //beadscale = scale.common;
        IsUse = false;
        tm = 0;
}

// Update is called once per frame
    void Update() {
        if (!m_game.showNeedUse)
        {
            tm = 0;
            gradeText.SetActive(true);
            NeedUseText.SetActive(false);
        }
        else
        {
            gradeText.SetActive(false);
            NeedUseText.SetActive(true);
            tm += Time.deltaTime;
            if (tm > 3f)
            {
                m_game.showNeedUse = false;
                tm = 0;
            }
        }
        //var Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        if (IsUse)
        {
            //beadscale = scale.common;
            gameObject.transform.localScale = commonScale;
            transform.position = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
            if (!onTheTop)
            {
                foreach (Transform child in this.GetComponentsInChildren<Transform>())
                {
                    if (child.GetComponent<SpriteRenderer>() != null)
                    {
                        child.GetComponent<SpriteRenderer>().sortingOrder += 100;
                    }
                    else if (child.GetComponent<SortingGroup>() != null)
                    {
                        child.GetComponent<SortingGroup>().sortingOrder += 100;
                    }
                }
                onTheTop = true;
            }
        }
        ShuXing();
        grade = grade0 + plusGrade;
        gradeText.GetComponent<TextMeshPro>().text = null;
        gradeText.GetComponent<TextMeshPro>().text += MyFunc.GetText(grade);

        int needtxt = grade0 - useNum;
        if (needtxt > 3 - useNum)
        {
            needtxt = 3 - useNum;
        }
        NeedUseText.GetComponent<TextMeshPro>().text = needtxt.ToString();

    }
    //消耗完回到珠串里切换属性和显示面，升级随机赋属性
    public void Frezz() {
        foreach (var sr in GetComponentsInChildren<SpriteRenderer>()) {
            sr.DOFade(0.4f, 0.5f);
        }
        GetComponent<SpriteRenderer>().DOFade(0.4f, 0.5f);
        EnableMove = false;
    }
    public void UpGrade()
    {
        if (grade0 == 2)
        {
            if (UnityEngine.Random.Range(0, 100) <= 10 + extraRandom*2 && attribute[0] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (UnityEngine.Random.Range(0, 100) <= 10 + extraRandom * 2 && attribute[0] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(0, 5)];
            }
            if (UnityEngine.Random.Range(0, 100) <= 10 + extraRandom * 2 && attribute[1] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (UnityEngine.Random.Range(0, 100) <= 10 + extraRandom * 2 && attribute[1] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(0, 5)];
            }
        }
        if (grade0 == 3)
        {
            if (UnityEngine.Random.Range(0, 100) <= 55 + extraRandom * 2 && attribute[0] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if(UnityEngine.Random.Range(0, 100) <= 55 + extraRandom * 2 && attribute[0] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(0, 5)];
            }
            if (UnityEngine.Random.Range(0, 100) <= 55 + extraRandom * 2 && attribute[1] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (UnityEngine.Random.Range(0, 100) <= 55 + extraRandom * 2 && attribute[1] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(0, 5)];
            }
        }
        if (grade0 == 4)
        {
            if (UnityEngine.Random.Range(0, 100) <= 85 + extraRandom * 2 && attribute[0] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (UnityEngine.Random.Range(0, 100) <= 85 + extraRandom * 2 && attribute[0] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(0, 5)];
            }
            if (UnityEngine.Random.Range(0, 100) <= 85 + extraRandom * 2 && attribute[1] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (UnityEngine.Random.Range(0, 100) <= 85 + extraRandom * 2 && attribute[1] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(0, 5)];
            }
        }
        if(grade0 >= 5)
        {
            if (attribute[0] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (attribute[0] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(0, i);
                //attribute[0] = attri[UnityEngine.Random.Range(0, 5)];
            }
            if (attribute[1] == 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                while (i < ShuXingGaiLv[0])
                {
                    i = UnityEngine.Random.Range(0, 100);
                }
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(1, 5)];
            }
            else if (attribute[1] != 0)
            {
                int i = UnityEngine.Random.Range(0, 100);
                GetShuXing(1, i);
                //attribute[1] = attri[UnityEngine.Random.Range(0, 5)];
            }
        }
    }

    private void GetShuXing(int a, int i)
    {
        //int i = UnityEngine.Random.Range(0, 100);
        if (i < ShuXingGaiLv[0]) attribute[a] = 0;
        else if (i < ShuXingGaiLv[0] + ShuXingGaiLv[1]) attribute[a] = 1;
        else if (i < ShuXingGaiLv[0] + ShuXingGaiLv[1] + ShuXingGaiLv[2]) attribute[a] = 2;
        else if (i < ShuXingGaiLv[0] + ShuXingGaiLv[1] + ShuXingGaiLv[2] + ShuXingGaiLv[3]) attribute[a] = 3;
        else if (i < ShuXingGaiLv[0] + ShuXingGaiLv[1] + ShuXingGaiLv[2] + ShuXingGaiLv[3] + ShuXingGaiLv[4]) attribute[a] = 4;
    }
    public void returnString()
    {
        useNum++;
        if(useNum == grade0||useNum>=3)
        {
            grade0++;
            plusGrade += onceUpGrade;
            useNum = 0;
            UpGrade();
        }
        SwitchShuXing();

        //ShuXing();
        //属性用不同图片表示（暂且用颜色）
        //if (attribute[0] == 0)
        //    gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        //else if (attribute[0] == 1)
        //    gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        //else if (attribute[0] == 2)
        //    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (m_game.gameTurn == GameTurn.win) return;
        if (!EnableMove) return;
        if(m_game.chooseType != ChooseType.No) {
            switch(m_game.chooseType)
            {
                case ChooseType.FengTu:
                    if (attribute[0] == 0)
                    {
                        attribute[0] = 1;
                    }
                    else if (attribute[1] == 0)
                    {
                        attribute[1] = 1;
                    }
                    else
                    {
                        attribute[0] = 1;
                    }
                    break;
                case ChooseType.YuShui:
                    if (attribute[0] == 0)
                    {
                        attribute[0] = 2;
                    }
                    else if (attribute[1] == 0)
                    {
                        attribute[1] = 2;
                    }
                    else
                    {
                        attribute[0] = 2;
                    }
                    break;
                case ChooseType.QinHuo:
                    if (attribute[0] == 0)
                    {
                        attribute[0] = 3;
                    }
                    else if (attribute[1] == 0)
                    {
                        attribute[1] = 3;
                    }
                    else
                    {
                        attribute[0] = 3;
                    }
                    break;
                case ChooseType.KongFeng:
                    if (attribute[0] == 0)
                    {
                        attribute[0] = 4;
                    }
                    else if (attribute[1] == 0)
                    {
                        attribute[1] = 4;
                    }
                    else
                    {
                        attribute[0] = 4;
                    }
                    break;

            }
            m_game.needChooseNum--;
        }
        else if (IsUse == false&&m_game.IsPlacingBead == null)
        {
            this.transform.GetChild(3).gameObject.SetActive(false);
            AudioManager.Instance.PlaySfx("HoldBead");
            m_game.IsPlacingBead = this.gameObject;
            IsUse = true;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            if (m_grid != null)
            {
                int ab = m_grid.GetComponent<QiCao>().abscissa;
                int or = m_grid.GetComponent<QiCao>().ordinate;
                m_game.usingBeads[ab, or] = null;
                m_grid.GetComponent<QiCao>().IsUse = false;
                m_grid = null;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }

    public void SwitchShuXing()
    {
        int a = attribute[0];
        attribute[0] = attribute[1];
        attribute[1] = a;
    }
    private void ShuXing()
    {
        if (attribute[0] == 0)
        {
            TuDing.SetActive(false);
            ShuiDing.SetActive(false);
            HuoDing.SetActive(false);
            FengDing.SetActive(false);
        }
        else if (attribute[0] == 1)
        {
            TuDing.SetActive(true);
            ShuiDing.SetActive(false);
            HuoDing.SetActive(false);
            FengDing.SetActive(false);
        }
        else if (attribute[0] == 2)
        {
            TuDing.SetActive(false);
            ShuiDing.SetActive(true);
            HuoDing.SetActive(false);
            FengDing.SetActive(false);
        }
        else if (attribute[0] == 3)
        {
            TuDing.SetActive(false);
            ShuiDing.SetActive(false);
            HuoDing.SetActive(true);
            FengDing.SetActive(false);
        }
        else if (attribute[0] == 4)
        {
            TuDing.SetActive(false);
            ShuiDing.SetActive(false);
            HuoDing.SetActive(false);
            FengDing.SetActive(true);
        }
        if (attribute[1] == 0)
        {
            TuDi.SetActive(false);
            ShuiDi.SetActive(false);
            HuoDi.SetActive(false);
            FengDi.SetActive(false);
        }
        if (attribute[1] == 1)
        {
            TuDi.SetActive(true);
            ShuiDi.SetActive(false);
            HuoDi.SetActive(false);
            FengDi.SetActive(false);
        }
        if (attribute[1] == 2)
        {
            TuDi.SetActive(false);
            ShuiDi.SetActive(true);
            HuoDi.SetActive(false);
            FengDi.SetActive(false);
        }
        if (attribute[1] == 3)
        {
            TuDi.SetActive(false);
            ShuiDi.SetActive(false);
            HuoDi.SetActive(true);
            FengDi.SetActive(false);
        }
        if (attribute[1] == 4)
        {
            TuDi.SetActive(false);
            ShuiDi.SetActive(false);
            HuoDi.SetActive(false);
            FengDi.SetActive(true);
        }
    }


}
