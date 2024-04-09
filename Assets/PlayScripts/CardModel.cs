using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class CardModel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform ExamPos;

    public CardType cardType;
    public GameObject HeiMu;
    public GameObject Liang;
    public int num;
    public float value = 0;
    private GameStart m_GameStart;
    private Vector3 startPosition;
    //private Vector3 beginPosition;
    private Transform startParent;
    public List<int[]> ZhenXing = new List<int[]>();
    public bool HasZhenXing = false;
    private Camera mainCamera = null;
    public int QiShu;
    public int ZhenX;
    public int ZhenY;
    public float ExamIntervalX;
    public float ExamIntervalY;
    //private bool IsUp = false;
    private bool HasInit = false;
    private bool IsUpSort = false;
    public bool IsUpGrade = false;
    public bool IsWinCard = false;
    //private GameObject[] allChild;
    //牌面预设
    private GameObject m_BeadRed;
    private GameObject m_BeadBlue;
    private GameObject m_BeadYellow;
    private GameObject m_BeadGreen;
    private GameObject m_BeadBlack;
    private GameObject m_EmptyQi;
    public GameObject valueText;

    //private Transform cardStart;

    private void Awake()
    {
        m_BeadRed = Resources.Load<GameObject>("Test/BeadImageRed");
        m_BeadBlue = Resources.Load<GameObject>("Test/BeadImageBlue");
        m_BeadYellow = Resources.Load<GameObject>("Test/BeadImageYellow");
        m_BeadGreen = Resources.Load<GameObject>("Test/BeadImageGreen");
        m_BeadBlack = Resources.Load<GameObject>("Test/BeadImageBlack");
        m_EmptyQi = Resources.Load<GameObject>("Test/EmptyQiImage");
        //随机阵型
        //ZhenXing = new List<int[]>();
        //if(QiShu == 4)
        //{
        //    ZhenX = 3;
        //    ZhenY = 3;
        //}
        //if(QiShu == 5)
        //{
        //    ZhenX = 4;
        //    ZhenY = 3;
        //}
        if (!HasZhenXing)
        {
            if(cardType == CardType.XingKong1||cardType == CardType.XingKong2||cardType == CardType.GuXiu)
            {
                int[] pos1 = new int[2];
                pos1[0] = 1; pos1[1] = 0;ZhenXing.Add(pos1);
                int[] pos2 = new int[2];
                pos2[0] = 0; pos2[1] = 1;ZhenXing.Add(pos2);
                int[] pos3 = new int[2];
                pos3[0] = 1; pos3[1] = 1;ZhenXing.Add(pos3);
                int[] pos4 = new int[2];
                pos4[0] = 1; pos4[1] = 2;ZhenXing.Add(pos4);
                int[] pos5 = new int[2];
                pos5[0] = 2; pos5[1] = 1;ZhenXing.Add(pos5);
                HasZhenXing = true;
            }
            while (ZhenXing.Count < QiShu)
            {
                int x = UnityEngine.Random.Range(0, ZhenX);
                int y = UnityEngine.Random.Range(0, ZhenY);
                int[] pos = new int[2];
                pos[0] = x;
                pos[1] = y;
                //if (!ZhenXing.Contains(pos))
                //{
                //    ZhenXing.Add(pos);
                //}
                bool include = false;
                for (int i = 0; i < ZhenXing.Count; i++)
                {
                    if (ZhenXing[i][0] == pos[0] && ZhenXing[i][1] == pos[1])
                    {
                        include = true;
                    }
                }
                if (!include)
                {
                    ZhenXing.Add(pos);
                }
            }
            HasZhenXing = true;
        }
    }

    public void UpdateValue()
    {
        value = 0;
        if(cardType == CardType.XingKong1 || cardType == CardType.XingKong2 || cardType == CardType.GuXiu)
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    bool success = true;
                    float onceValue = 0;
                    int HuoNum = 0;
                    for(int m = 0; m < ZhenXing.Count; m++)
                    {
                        if (ZhenXing[m][0] == 1 && ZhenXing[m][1] == 1)
                        {
                            if (m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]] != null)
                            {
                                if(m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<QiCao>().IsUse == false)
                                {
                                    success = false;
                                    onceValue = 0;
                                }
                                else
                                {
                                    onceValue += m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().grade;
                                    if(cardType == CardType.XingKong1)
                                    {
                                        onceValue *= 1.25f;
                                    }
                                    else if(cardType == CardType.XingKong2||cardType == CardType.GuXiu)
                                    {
                                        onceValue *= 1.125f;
                                    }
                                    if (m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().attribute[0] == 3)
                                    {
                                        //onceValue *= (1 + 0.25f + m_GameStart.HuoUp * 0.025f);
                                        HuoNum++;
                                    }
                                }
                            }
                            else
                            {
                                success = false;
                                onceValue = 0;
                            }
                        }
                        else
                        {
                            if(m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]] != null)
                            {
                                if(m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<QiCao>().IsUse)
                                {
                                    success = false;
                                    onceValue = 0;
                                }
                            }
                        }
                    }
                    if(success)
                    {
                        if(cardType == CardType.XingKong1||cardType == CardType.XingKong2)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * onceValue;
                        }
                        else
                        {
                            value += onceValue;
                        }
                        m_GameStart.usingBeads[i + 1, j + 1].GetComponent<Bead>().InZhenXing = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    bool success = true;
                    float onceValue = 0;
                    int HuoNum = 0;
                    List<int> luliCard = new List<int>();
                    for (int m = 0; m < ZhenXing.Count; m++)
                    {
                        if (m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]] != null)
                        {
                            if (m_GameStart.grids[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<QiCao>().IsUse == false)
                            {
                                success = false;
                                onceValue = 0;
                            }
                            else
                            {
                                onceValue += m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().grade;
                                //火1
                                if(!luliCard.Contains(m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().attribute[0]))
                                {
                                    luliCard.Add(m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().attribute[0]);
                                }
                                if (m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().attribute[0] == 3)
                                {
                                    HuoNum++;
                                }
                                //m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().InZhenXing = true;
                            }
                        }
                        else
                        {
                            success = false;
                            onceValue = 0;
                        }
                    }
                    if (success)
                    {
                        if (cardType == CardType.Attact||cardType == CardType.Juji1||cardType == CardType.Juji2||cardType == CardType.YiShi)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * onceValue;
                        }
                        else if(cardType == CardType.YiLing||cardType == CardType.ZhenShe)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * onceValue * 0.75f;
                        }
                        else if(cardType == CardType.LiaoGu1)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * onceValue * 2f;
                        }
                        else if(cardType == CardType.LiaoGu2||cardType == CardType.NiLv)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * onceValue * 1.5f;
                        }
                        else if(cardType == CardType.LuLi1)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * (1 + luliCard.Count * 0.3f) * onceValue * 0.75f;
                        }
                        else if(cardType == CardType.LuLi2)
                        {
                            value += (1 + (0.25f + m_GameStart.HuoUp * 0.025f) * HuoNum) * (1 + luliCard.Count * 0.3f) * onceValue * 0.5f;
                        }
                        else if (cardType == CardType.JiaoXie)
                        {
                            value += onceValue * 0.75f;
                        }
                        else
                        {
                            value += onceValue;
                        }
                        for (int m = 0; m < ZhenXing.Count; m++)
                        {
                            m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().InZhenXing = true;
                            //火1
                            //if (m_GameStart.usingBeads[i + ZhenXing[m][0], j + ZhenXing[m][1]].GetComponent<Bead>().attribute[0] == 3)
                            //{
                            //    HuoNum++;
                            //}
                        }
                    }
                }
            }
        }
        //value = (1 + 0.25f * HuoNum) * value;
    }
    public void OnInit(GameStart game, int num)
    {
        m_GameStart = game;
        this.num = num;
        //this.cardStart = cardStart;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win) return;

        if (m_GameStart.DragCard!=num&&m_GameStart.DragCard != -1) return;
        m_GameStart.DragCard = num;

        if (mainCamera == null)
        {
            mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        }
        // 记录拖拽开始时的位置和父对象
        startPosition = new Vector3(num * m_GameStart.interval, 0, 0) + m_GameStart.CardStart.position;
        startParent = transform.parent;
        transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);

        // 将Tile设置为层级最高，确保其位于其他Tile上方
        //transform.SetAsLastSibling();
        if (!IsUpSort)
        {
            GetComponent<SpriteRenderer>().sortingOrder += 30;
            foreach (Transform child in this.transform)
            {
                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().sortingOrder += 30;
                }
                else if (child.GetComponent<SortingGroup>() != null)
                {
                    child.GetComponent<SortingGroup>().sortingOrder += 30;
                }
                else
                {
                    foreach (Transform child1 in child)
                    {
                        if (child1.GetComponent<SpriteRenderer>() != null)
                        {
                            child1.GetComponent<SpriteRenderer>().sortingOrder += 30;
                        }

                    }
                }
            }
            IsUpSort = true;
        }
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win) return;

        if (m_GameStart.DragCard != num && m_GameStart.DragCard != -1) return;
        transform.position = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win) return;

        if (m_GameStart.DragCard != num && m_GameStart.DragCard != -1) return;
        
        GetComponent<BoxCollider2D>().enabled = true;
        CardModel cardModel = null;
        Vector3 endPosition = Vector3.zero;
        int targetNum = 0;
        // 检查是否有与之交换位置的目标Tile
        GameObject targetObject = eventData.pointerCurrentRaycast.gameObject;

        while (targetObject != null && targetObject.GetComponent<CardModel>() == null)
        {
            if (targetObject.transform.parent != null)
            {
                targetObject = targetObject.transform.parent.gameObject;
            }
            else
            {
                break;
            }
        }

        if (targetObject != null && targetObject.GetComponent<CardModel>() != null)
        {
            startPosition = new Vector3(num * m_GameStart.interval, 0, 0) + m_GameStart.CardStart.position;
            cardModel = targetObject.GetComponent<CardModel>();
            endPosition = new Vector3(cardModel.num * m_GameStart.interval, 0, 0) + m_GameStart.CardStart.position;
            targetNum = cardModel.num;
            GetComponent<BoxCollider2D>().enabled = false;
            targetObject.GetComponent<BoxCollider2D>().enabled = false;
            cardModel.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            DG.Tweening.Sequence sequence1 = DOTween.Sequence();
            DG.Tweening.Sequence sequence2 = DOTween.Sequence();
            sequence1.Append(this.gameObject.transform.DOMove(endPosition, 0.3f).SetEase(Ease.OutExpo));
            sequence2.Append(cardModel.gameObject.transform.DOMove(startPosition, 0.3f).SetEase(Ease.OutExpo));
            sequence1.InsertCallback(0.3f, () =>
            {
                m_GameStart.DragCard = -1;
                cardModel.num = this.num;
                this.num = targetNum;
                GetComponent<BoxCollider2D>().enabled = true;
                cardModel.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                if (IsUpSort)
                {
                    GetComponent<SpriteRenderer>().sortingOrder -= 30;
                    foreach (Transform child in this.transform)
                    {
                        if (child.GetComponent<SpriteRenderer>() != null)
                        {
                            child.GetComponent<SpriteRenderer>().sortingOrder -= 30;
                        }
                        else if (child.GetComponent<SortingGroup>() != null)
                        {
                            child.GetComponent<SortingGroup>().sortingOrder -= 30;
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.GetComponent<SpriteRenderer>() != null)
                                {
                                    child1.GetComponent<SpriteRenderer>().sortingOrder -= 30;
                                }

                            }
                        }

                    }
                    IsUpSort = false;
                }
            });

        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
            DG.Tweening.Sequence sequence = DOTween.Sequence();
            sequence.Append(this.gameObject.transform.DOMove(startPosition, 0.3f).SetEase(Ease.OutExpo));
            transform.SetParent(startParent);
            sequence.InsertCallback(0.3f, () =>
            {
                GetComponent<BoxCollider2D>().enabled = true;
                m_GameStart.DragCard = -1;
                if (IsUpSort)
                {
                    GetComponent<SpriteRenderer>().sortingOrder -= 30;
                    foreach (Transform child in this.transform)
                    {
                        if (child.GetComponent<SpriteRenderer>() != null)
                        {
                            child.GetComponent<SpriteRenderer>().sortingOrder -= 30;
                        }
                        else if (child.GetComponent<SortingGroup>() != null)
                        {
                            child.GetComponent<SortingGroup>().sortingOrder -= 30;
                        }
                        else
                        {
                            foreach (Transform child1 in child)
                            {
                                if (child1.GetComponent<SpriteRenderer>() != null)
                                {
                                    child1.GetComponent<SpriteRenderer>().sortingOrder -= 30;
                                }
                            }
                        }
                    }
                    IsUpSort=false;
                }
            });
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win)
        {
            m_GameStart.NotUseCardModel.Add(this);
            //Debug.Log(1);
            m_GameStart.winChooseCard = true;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win || m_GameStart.gameTurn == GameTurn.playerChoose) return;
        if (m_GameStart.DragCard != num && m_GameStart.DragCard != -1) return;
        //if (!IsUp)
        //{
        //    beginPosition = transform.position;
        //    IsUp = true;
        //}
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(this.gameObject.transform.DOMove(new Vector3(num * m_GameStart.interval, 0.8f, 0) + m_GameStart.CardStart.position, 0.3f).SetEase(Ease.OutExpo));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_GameStart.gameTurn == GameTurn.win||m_GameStart.gameTurn == GameTurn.playerChoose) return;
        if (m_GameStart.DragCard != num && m_GameStart.DragCard != -1) return;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(this.gameObject.transform.DOMoveY(m_GameStart.CardStart.position.y, 0.3f).SetEase(Ease.OutExpo));
        //sequence.OnKill(NotUp);
    }

    //private void NotUp()
    //{
    //    IsUp = false;
    //}
    // Start is called before the first frame update
    void Start()
    {
        //if (!HasZhenXing)
        //{
        //    ZhenXingInit();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (HasZhenXing&&!HasInit)
        {
            HasInit = true;
            ZhenXingInit();
        }

        valueText.GetComponent<TextMeshPro>().text = value.ToString("F0");
        if (value > 0)
        {
            HeiMu.SetActive(false);
            Liang.SetActive(true);
        }
        else
        {
            HeiMu.SetActive(true);
            Liang.SetActive(false);
        }
    }

    private void ZhenXingInit()
    {
        float ExamX = ExamPos.position.x;
        float ExamY = ExamPos.position.y;
        
        for(int m = 0; m < ZhenX; m++)
        {
            for(int  n = 0; n < ZhenY; n++)
            {
                Vector3 pos = new Vector3(ExamX, ExamY, 0);
                GameObject EmptyQi = Instantiate(m_EmptyQi, ExamPos);
                EmptyQi.transform.position = pos;
                for (int i = 0; i < QiShu; i++)
                {
                    if (ZhenXing[i][0] == m && ZhenXing[i][1] == n)
                    {
                        if(cardType == CardType.Attact||cardType == CardType.Juji1||cardType ==CardType.Juji2||
                            cardType == CardType.YiShi||cardType == CardType.YiLing||cardType == CardType.NingShi1||
                            cardType == CardType.NingShi2||cardType == CardType.ZhenShe||cardType == CardType.LiaoGu1||
                            cardType == CardType.LiaoGu2||cardType == CardType.LuLi1||cardType == CardType.LuLi2||
                            cardType == CardType.NiLv)
                        {
                            GameObject qi = Instantiate(m_BeadRed, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        if((cardType == CardType.XingKong1||cardType == CardType.XingKong2)&&m==1&&n==1)
                        {
                            GameObject qi = Instantiate(m_BeadRed, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        else if(cardType == CardType.GuXiu && m == 1 && n == 1)
                        {
                            GameObject qi = Instantiate(m_BeadGreen, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        else if(cardType == CardType.XingKong1 || cardType == CardType.XingKong2||cardType == CardType.GuXiu)
                        {
                            GameObject qi = Instantiate(m_BeadBlack, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        if (cardType == CardType.HuiSu||cardType == CardType.FanZhao1||cardType == CardType.FanZhao2)
                        {
                            GameObject qi = Instantiate(m_BeadBlue, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        if (cardType == CardType.Special||cardType == CardType.FengTu||cardType==CardType.KongFeng||
                            cardType==CardType.QinHuo||cardType==CardType.YuShui||cardType == CardType.QuDao1||
                            cardType == CardType.QuDao2)
                        {
                            GameObject qi = Instantiate(m_BeadYellow, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                        if (cardType == CardType.Defend||cardType == CardType.FanFu||cardType == CardType.GuXiu||
                            cardType == CardType.JiaoXie)
                        {
                            GameObject qi = Instantiate(m_BeadGreen, ExamPos);
                            qi.transform.position = pos;
                            if (IsWinCard)
                            {
                                qi.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }
                        }
                    }
                }
                ExamX += ExamIntervalX;
            }
            ExamY -= ExamIntervalY;
            ExamX = ExamPos.position.x;
        }
        HasZhenXing = true;
    }
}
