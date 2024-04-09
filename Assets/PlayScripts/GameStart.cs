using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;



public enum CardType {
    Attact,
    Defend,
    Special,
    FengTu,
    KongFeng,
    QinHuo,
    YuShui,
    //WangFu,
    HuiSu,
    Juji1,
    Juji2,
    FanFu,
    XingKong1,
    XingKong2,
    GuXiu,
    YiShi,
    YiLing,
    NingShi1,
    NingShi2,
    JiaoXie,
    ZhenShe,
    QuDao1,
    QuDao2,
    FanZhao1,
    FanZhao2,
    LiaoGu1,
    LiaoGu2,
    LuLi1,
    LuLi2,
    NiLv,
}

public enum BeadType {
    Tu,
    Shui,
    Huo,
    Feng,
}
public enum ChooseType {
    No,
    FengTu,
    KongFeng,
    QinHuo,
    YuShui,
}
public enum GameTurn {
    prepare,
    playerTurn,
    enemyEffect,
    useCards,
    playerChoose,
    enemyTurn,
    fail,
    win,
}

//public struct BeadStruct
//{
//   public int[] attribute;
//   public int grade;
//}
public class GameStart : MonoBehaviour {

    [SerializeField] public Transform CardStart;
    [SerializeField] private Transform QiPanStart;
    public Transform NotUseQi;
    public Transform UsingQi;
    [SerializeField] private Transform NotUseGrid;
    [SerializeField] private Transform CardLeavePos;
    [SerializeField] private Transform WinCardPos;

    public EasyTransition.TransitionManager transitionManager;
    public string sceneName;

    public GameTurn gameTurn;
    public ChooseType chooseType = ChooseType.No;
    private float timer = 0;
    public Camera cam;
    public GameObject IsPlacingBead=null;
    public GameObject player;
    public GameObject playerImage;
    public GameObject enemy;
    public GameObject enemyImage;
    public GameObject enemyTou;
    public GameObject enemyActionImage1;
    public GameObject enemyActionImage2;
    private GameObject HeiMuWin;
    public GameObject winChooseText;
    private GameObject HeiMuFail;
    private GameObject ShuYuanQiangHua;
    public GameObject skillImage;
    public GameObject endTurnImage;
    public GameObject background;
    public GameObject skip;
    public GameObject TiaoZi;
    //动画
    public GameObject attackAnimation;
    public GameObject enemyAttackAnimation;
    public GameObject ChanRuoAnimation;
    public GameObject enemyChanRuoAnimation;
    public GameObject XieLiAnimation;
    public GameObject enemyXieLiAnimation;
    public GameObject defendAnimation;
    public GameObject enemyDefendAnimation;

    private CardType enemyActionType;
    private CardModel card1 = null, card2 = null, card3 = null, card4 = null;
    private int IsHuiSu = -1;
    private GameObject wincard1 = null, wincard2 = null, wincard3 = null;
    private GameObject winQiang1 = null, winQiang2 = null, winQiang3 = null;
    public int HuoUp = 0, ShuiUp = 0, TuUp = 0, FengUp = 0;
    private bool HasStart = false, HasEnd = false;
    private bool act1 = false, act2 = false, act3 = false, act4 = false;
    private bool Eact1 = false, Eact2 = false;
    private bool Eeffect = false;
    private bool IsPozhen = false;
    private bool winPrepare = false, winPrepare1 = false, failPrepare = false;
    public bool winChooseCard = false;
    public bool showNeedUse = false;
    private bool record = false;
    private bool record1 = false;
    private bool clearBuff = false;
    private bool Feng1Record = false;
    private bool Tu2Record = false;
    private bool Feng1 = false;
    private bool Shui1 = false;
    private bool Tu2 = false;
    public int SkillNeedNum = 32;
    public int SkillNowNum = 0;
    public int Feng1Num = 0;
    public int Tu2Num = 0;
    //private bool enemyHasAct = false;
    private bool clearDefend = false;
    //private bool recordCard = false;
    public int DragCard = -1;
    public float stopTime = 0;
    private float nowTime = 0;
    public int needChooseNum = 0;
    public int maxInitNum = 0;
    public int enemyAction1 = 0;//敌人行动条
    public int enemyAction2 = 0;
    public int effect1 = -1, effect2 = -1,effect3 = -1, effect4 = -1;//敌人特殊效果
    public int HuiHeShu = 1;
    public int PoZhenX = 0, PoZhenY = 0;
    public int PoZhenType = 0;

    //测试数据
    public float AllAttact = 0;
    public float OnceAttact = 0;

    public int useCardNum = 4;
    private int abscissa = 6;
    private int ordinate = 6;
    public int usingBeadNum = 0;
    public int inGridBeadNum = 0;
    //棋盘上的棋子坐标
    public GameObject[,] usingBeads;
    //格子坐标
    public GameObject[,] grids;
    //10个棋槽
    public Transform[] CanUseGrids = new Transform[10];
    //所有棋子属性
    //public List<BeadStruct> AllBeads = new List<BeadStruct>();
    public List<Bead> BeadsInGrids = new List<Bead>();
    public List<Bead> NotUseBead = new List<Bead>();
    public float interval;
    public float QiIntervalX;
    public float QiIntervalY;
    //卡牌预设
    private GameObject m_CardAttactAsset;
    private GameObject m_CardDefendAsset;
    private GameObject m_CardSpecialAsstet;
    private GameObject m_CardFengTu;
    private GameObject m_CardKongFeng;
    private GameObject m_CardQinHuo;
    private GameObject m_CardYuShui;
    private GameObject m_CardHuiSu;
    private GameObject m_CardJuJi1;
    private GameObject m_CardJuJi2;
    private GameObject m_CardFanFu;
    private GameObject m_CardXingKong1;
    private GameObject m_CardXingKong2;
    private GameObject m_CardGuXiu;
    private GameObject m_CardYiShi;
    private GameObject m_CardYiLing;
    private GameObject m_CardNingShi1;
    private GameObject m_CardNingShi2;
    private GameObject m_CardJiaoXie;
    private GameObject m_CardZhenShe;
    private GameObject m_CardQuDao1;
    private GameObject m_CardQuDao2;
    private GameObject m_CardFanZhao1;
    private GameObject m_CardFanZhao2;
    private GameObject m_CardLiaoGu1;
    private GameObject m_CardLiaoGu2;
    private GameObject m_CardLuLi1;
    private GameObject m_CardLuLi2;
    private GameObject m_CardNiLv;
    //枢元预设
    private GameObject m_bead;
    private GameObject m_QiCao;
    //敌人预设
    private Sprite Feng1Asset;
    private Sprite Feng2Asset;
    private Sprite Feng3Asset;
    private Sprite Feng1Tou;
    private Sprite Feng2Tou;
    private Sprite Feng3Tou;
    private Sprite Yun1Asset;
    private Sprite Yun2Asset;
    private Sprite Yun3Asset;
    private Sprite Yun4Asset;
    private Sprite Yun1Tou;
    private Sprite Yun2Tou;
    private Sprite Yun3Tou;
    private Sprite Yun4Tou;
    private Sprite JiMinAsset;
    private Sprite JiMinTou;
    private Sprite Fu1Asset;
    private Sprite Fu2Asset;
    private Sprite Fu1Tou;
    private Sprite Fu2Tou;
    //背景图片
    private Sprite FengBack;
    private Sprite YunBack;
    private Sprite JiBack;
    private Sprite FuBack;

    private List<GameObject> ChoosedCard = new List<GameObject>();
    public List<GameObject> NotUseCard = new List<GameObject>();
    public List<CardModel> NotUseCardModel = new List<CardModel>();
    private List<CardModel> NowCard = new List<CardModel>();
    public List<CardModel> AllCard = new List<CardModel>();

    private List<int> BigPos;
    private List<int> SmallPos;

    private bool IsPrepared = false;
    //文字
    public GameObject StartText;
    public GameObject TurnText;
    public GameObject enemyActText;
    public GameObject HuiHeText;
    public GameObject AllAttactText;
    public GameObject OnceAttactText;
    // Start is called before the first frame update

    private void Awake() {
        player.GetComponent<Player>().HP = GameData.HP;
        player.GetComponent<Player>().MaxHp = GameData.MaxHp;
        //transitionManager = GameObject.Find("TransitionManager").GetComponent<EasyTransition.TransitionManager>();
        usingBeads = new GameObject[abscissa+5, ordinate+5];
        grids = new GameObject[abscissa + 5, ordinate + 5];//上下左右额外多出两行用于判定，此后枢元坐标均需要各+2
        for(int i = 0; i < 10; i++)
        {
            CanUseGrids[i] = NotUseGrid.GetChild(i).transform;
        }

        //卡牌资源
        LoadCardResource();

        //枢元资源
        m_bead = Resources.Load<GameObject>("Test/Bead");
        m_QiCao = Resources.Load<GameObject>("Test/EmptyQi");

        HeiMuWin = Resources.Load<GameObject>("Test/HeiMuWin");
        HeiMuFail = Resources.Load<GameObject>("Test/HeiMuFail");
        ShuYuanQiangHua = Resources.Load<GameObject>("Test/QiangHua");
        //敌人图像
        LoadEnemyResource();

        //背景图片
        FengBack = Resources.Load<Sprite>("Art/Feng");
        YunBack = Resources.Load<Sprite>("Art/Yun");
        JiBack = Resources.Load<Sprite>("Art/Ji");
        FuBack = Resources.Load<Sprite>("Art/Fu");

        FixEnemyConfig();

        //BigPos = new List<int>();
        //SmallPos = new List<int>();
        //BigPos.Add(0);
        //BigPos.Add(2);
        //BigPos.Add(4);
        //BigPos.Add(5);
        //BigPos.Add(7);
        //BigPos.Add(9);
        //SmallPos.Add(1);
        //SmallPos.Add(3);
        //SmallPos.Add(6);
        //SmallPos.Add(8);
        
        //test
        ChoosedCard.Add(m_CardAttactAsset);
        ChoosedCard.Add(m_CardAttactAsset);
        //ChoosedCard.Add(m_CardAttactAsset);
        //ChoosedCard.Add(m_CardAttactAsset);
        //ChoosedCard.Add(m_CardAttactAsset);
        //ChoosedCard.Add(m_CardDefendAsset);
        ChoosedCard.Add(m_CardDefendAsset);
        ChoosedCard.Add(m_CardDefendAsset);
        ChoosedCard.Add(m_CardSpecialAsstet);
        ChoosedCard.Add(m_CardSpecialAsstet);

        {
            if (GameData.IsOne)
           
            
        }

    }

    void InitializeGameData() {
        if (GameData.IsOne) CreateInitialCardsAndBeads();
        else LoadGameData();

        CreateAllTypeCard();
    }

    void LoadGameData() {
        //Debug.Log(GameData.HoldCard.Count);
        //读取咒术
        for (int i = 0; i < GameData.HoldCard.Count; i++) {
            CardModel card = new() {
                cardType = (CardType)GameData.HoldCard[i],
                IsUpGrade = GameData.IsUpGrade[i],
                HasZhenXing = GameData.HasZhenXing[i],
                ZhenXing = new List<int[]>()
            };
            if (card.HasZhenXing) {
                //card.ZhenXing = GameData.ZhenXing[i];
                for (int j = 0; j < GameData.ZhenXing[i].Count; j++) {
                    card.ZhenXing.Add(GameData.ZhenXing[i][j]);
                }
            }
            NotUseCardModel.Add(card);
        }
        //读取枢元
        NotUseBead = new List<Bead>();
        for (int i = 0; i < GameData.ExtraGrade.Count; i++) {
            Bead bead = new Bead {
                attribute = new int[2]
            };
            bead.attribute[0] = 0;
            bead.attribute[1] = 0;
            bead.grade0 = 1;
            bead.useNum = 0;
            bead.extraGrade = GameData.ExtraGrade[i];
            bead.onceUpGrade = GameData.OnceUpGrade[i];
            bead.extraRandom = GameData.ExtraRandom[i];
            bead.plusGrade = bead.extraGrade;
            bead.ShuXingGaiLv = new int[5];
            for (int j = 0; j < 5; j++) {
                bead.ShuXingGaiLv[j] = GameData.ShuXingGaiLv[i][j];
            }
            NotUseBead.Add(bead);
        }
        //读取属性强化
        HuoUp = GameData.HuoUp;
        ShuiUp = GameData.ShuiUp;
        TuUp = GameData.TuUp;
        FengUp = GameData.FengUp;
    }


    void CreateInitialCardsAndBeads() {
        //咒术
        GameData.IsOne = false;
        CardModel cardmod1 = new() {
            cardType = CardType.Attact,
            ExamIntervalX = 0.38f
        };
        NotUseCardModel.Add(cardmod1);
        NotUseCardModel.Add(cardmod1);
        CardModel cardmod2 = new() {
            cardType = CardType.Defend,
            ExamIntervalX = 0.38f
        };
        NotUseCardModel.Add(cardmod2);
        NotUseCardModel.Add(cardmod2);
        CardModel cardmod3 = new() {
            cardType = CardType.Special,
            ExamIntervalX = 0.38f
        };
        NotUseCardModel.Add(cardmod3);
        CardModel cardmod4 = new();
        int i = UnityEngine.Random.Range(0, 4);
        {
            if (i == 0) {
                cardmod4.cardType = CardType.FengTu;
            }
            else if (i == 1) {
                cardmod4.cardType = CardType.KongFeng;
            }
            else if (i == 2) {
                cardmod4.cardType = CardType.QinHuo;
            }
            else if (i == 3) {
                cardmod4.cardType = CardType.YuShui;
            }
        }
        NotUseCardModel.Add(cardmod4);
        //枢元
        for (int j = 0; j < 16; j++) {
            Bead bead = new() {
                attribute = new int[2]
            };
            bead.attribute[0] = 0;
            bead.attribute[1] = 0;
            bead.grade0 = 1;
            bead.useNum = 0;
            bead.extraGrade = 0;
            bead.onceUpGrade = 0;
            bead.extraRandom = 0;
            bead.plusGrade = 0;
            bead.ShuXingGaiLv = new int[5];
            int[] gailv = new int[5];
            gailv[0] = 20; gailv[1] = 20; gailv[2] = 20; gailv[3] = 20; gailv[4] = 20;
            bead.ShuXingGaiLv = gailv;
            NotUseBead.Add(bead);
        }
        //属性强化
        HuoUp = 0; ShuiUp = 0; TuUp = 0; FengUp = 0;
    }


    void CreateAllTypeCard(){
        for(int i = 0; i< 29; i++) {
            CardModel cardmod = new() {
                cardType = (CardType)i
            };
            AllCard.Add(cardmod);
            if (i == 1 || i == 11 || i == 14 || i == 19) {
                AllCard.Add(cardmod);
            }
        }
    }


    private void LoadCardResource() {
        m_CardAttactAsset = Resources.Load<GameObject>("Test/Attact");
        m_CardDefendAsset = Resources.Load<GameObject>("Test/Defend");
        m_CardSpecialAsstet = Resources.Load<GameObject>("Test/Special");
        m_CardFengTu = Resources.Load<GameObject>("Test/FengTu");
        m_CardKongFeng = Resources.Load<GameObject>("Test/KongFeng");
        m_CardQinHuo = Resources.Load<GameObject>("Test/QinHuo");
        m_CardYuShui = Resources.Load<GameObject>("Test/YuShui");
        m_CardHuiSu = Resources.Load<GameObject>("Test/HuiSu");
        m_CardJuJi1 = Resources.Load<GameObject>("Test/JuJi1");
        m_CardJuJi2 = Resources.Load<GameObject>("Test/JuJi2");
        m_CardFanFu = Resources.Load<GameObject>("Test/FanFu");
        m_CardXingKong1 = Resources.Load<GameObject>("Test/XingKong1");
        m_CardXingKong2 = Resources.Load<GameObject>("Test/XingKong2");
        m_CardGuXiu = Resources.Load<GameObject>("Test/GuXiu");
        m_CardYiShi = Resources.Load<GameObject>("Test/YiShi");
        m_CardYiLing = Resources.Load<GameObject>("Test/YiLing");
        m_CardNingShi1 = Resources.Load<GameObject>("Test/NingShi1");
        m_CardNingShi2 = Resources.Load<GameObject>("Test/NingShi2");
        m_CardJiaoXie = Resources.Load<GameObject>("Test/JiaoXie");
        m_CardZhenShe = Resources.Load<GameObject>("Test/ZhenShe");
        m_CardQuDao1 = Resources.Load<GameObject>("Test/QuDao1");
        m_CardQuDao2 = Resources.Load<GameObject>("Test/QuDao2");
        m_CardFanZhao1 = Resources.Load<GameObject>("Test/FanZhao1");
        m_CardFanZhao2 = Resources.Load<GameObject>("Test/FanZhao2");
        m_CardLiaoGu1 = Resources.Load<GameObject>("Test/LiaoGu1");
        m_CardLiaoGu2 = Resources.Load<GameObject>("Test/LiaoGu2");
        m_CardLuLi1 = Resources.Load<GameObject>("Test/LuLi1");
        m_CardLuLi2 = Resources.Load<GameObject>("Test/LuLi2");
        m_CardNiLv = Resources.Load<GameObject>("Test/NiLv");
    }

    private void LoadEnemyResource() {
        Feng1Asset = Resources.Load<Sprite>("Art/Enemy/风士兵1");
        Feng1Tou = Resources.Load<Sprite>("Art/Enemy/风士兵1头");
        Feng2Asset = Resources.Load<Sprite>("Art/Enemy/风士兵2");
        Feng2Tou = Resources.Load<Sprite>("Art/Enemy/风士兵2头");
        Feng3Asset = Resources.Load<Sprite>("Art/Enemy/风士兵3");
        Feng3Tou = Resources.Load<Sprite>("Art/Enemy/风士兵3头");
        Yun1Asset = Resources.Load<Sprite>("Art/Enemy/云士兵1");
        Yun1Tou = Resources.Load<Sprite>("Art/Enemy/云士兵1头");
        Yun2Asset = Resources.Load<Sprite>("Art/Enemy/云士兵2");
        Yun2Tou = Resources.Load<Sprite>("Art/Enemy/云士兵2头");
        Yun3Asset = Resources.Load<Sprite>("Art/Enemy/云士兵3");
        Yun3Tou = Resources.Load<Sprite>("Art/Enemy/云士兵3头");
        Yun4Asset = Resources.Load<Sprite>("Art/Enemy/云龟");
        Yun4Tou = Resources.Load<Sprite>("Art/Enemy/云龟头像");
        JiMinAsset = Resources.Load<Sprite>("Art/Enemy/饥民");
        JiMinTou = Resources.Load<Sprite>("Art/Enemy/饥民头");
        Fu1Asset = Resources.Load<Sprite>("Art/Enemy/boss一阶段");
        Fu1Tou = Resources.Load<Sprite>("Art/Enemy/boss一阶段头");
        Fu2Asset = Resources.Load<Sprite>("Art/Enemy/boss二阶段");
        Fu2Tou = Resources.Load<Sprite>("Art/Enemy/boss二阶段头");
    }

    private void FixEnemyConfig() {
        for (int i = 0; i < 8; i++) {
            if (GameData.Enemy[i] == 1) {
                enemy.GetComponent<Enemy>().type = i;
                switch (i) {
                    case 0:
                        background.GetComponent<SpriteRenderer>().sprite = FengBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Feng1Asset;
                        enemyImage.transform.localPosition = new Vector3(-2.09f, -1.19f, 0);
                        enemyImage.transform.localScale = new Vector3(1f, 1f, 1f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Feng1Tou;
                        break;
                    case 1:
                        background.GetComponent<SpriteRenderer>().sprite = FengBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Feng2Asset;
                        enemyImage.transform.localPosition = new Vector3(-2.66f, -3.08f, 0);
                        enemyImage.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Feng2Tou;
                        break;
                    case 2:
                        background.GetComponent<SpriteRenderer>().sprite = FengBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Feng3Asset;
                        enemyImage.transform.localPosition = new Vector3(-1.82f, -1.06f, 0);
                        enemyImage.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Feng3Tou;
                        break;
                    case 3:
                        background.GetComponent<SpriteRenderer>().sprite = YunBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Yun1Asset;
                        enemyImage.transform.localPosition = new Vector3(-0.77f, -2.45f, 0);
                        enemyImage.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Yun1Tou;
                        break;
                    case 4:
                        background.GetComponent<SpriteRenderer>().sprite = YunBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Yun2Asset;
                        enemyImage.transform.localPosition = new Vector3(-6.98f, -2.03f, 0);
                        enemyImage.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Yun2Tou;
                        break;
                    case 5:
                        background.GetComponent<SpriteRenderer>().sprite = YunBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Yun3Asset;
                        enemyImage.transform.localPosition = new Vector3(-3.56f, -3.01f, 0);
                        enemyImage.transform.localScale = new Vector3(0.84f, 0.84f, 0.84f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Yun3Tou;
                        break;
                    case 6:
                        background.GetComponent<SpriteRenderer>().sprite = JiBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = JiMinAsset;
                        enemyImage.transform.localPosition = new Vector3(-3.56f, -1.68f, 0);
                        enemyImage.transform.localScale = new Vector3(1f, 1f, 1f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = JiMinTou;
                        break;
                    case 7:
                        background.GetComponent<SpriteRenderer>().sprite = FuBack;
                        enemyImage.GetComponent<SpriteRenderer>().sprite = Fu1Asset;
                        enemyImage.transform.localPosition = new Vector3(-6.42f, -4.12f, 0);
                        enemyImage.transform.localScale = new Vector3(1f, 1f, 1f);
                        enemyTou.GetComponent<SpriteRenderer>().sprite = Fu1Tou;
                        break;
                }
            }
        }
    }

    private void InitBead()
    {
        for(int i = 0; i < NotUseQi.childCount; i++)
        {
            //NotUseQi.GetChild(i).transform.position = CanUseGrids[i].transform.position;
            NotUseQi.GetChild(i).transform.DOMove(CanUseGrids[i].transform.position, 0.3f).SetEase(Ease.OutExpo);
            //if (BigPos.Contains(i))
            //{
            //    NotUseQi.GetChild(i).transform.localScale = NotUseQi.GetChild(i).GetComponent<Bead>().bigScale;
            //}
            //else if (SmallPos.Contains(i))
            //{
            //    NotUseQi.GetChild(i).transform.localScale = NotUseQi.GetChild(i).GetComponent<Bead>().smallScale;
            //}

        }
        int initNum = 0;
        if (NotUseQi.childCount >= 10)
            return;
        else if (NotUseQi.childCount <= 10-maxInitNum)
        {
            initNum = maxInitNum;
        }
        else
        {
            initNum = 10 - NotUseQi.childCount;
        }
        if(initNum >= NotUseBead.Count)
        {
            initNum = NotUseBead.Count;
        }
        for(int i = 0; i < initNum; i++)
        {
            int NowPos = NotUseQi.childCount;
            Bead bead = NotUseBead[0];
            int[] gailv = new int[5];
            for(int j = 0;j<5;j++)
            {
                gailv[j] = bead.ShuXingGaiLv[j];
            }
            //bool canRemove = false;
            var newBead = InitEachBead(bead.attribute[0], bead.attribute[1], bead.grade0, bead.useNum,
                bead.extraGrade, bead.onceUpGrade, bead.plusGrade, bead.extraRandom, gailv);
            //var newBead = InitEachBead(0, 0, 1);
            //newBead.transform.position = CanUseGrids[NowPos].transform.position;
            newBead.transform.position = new Vector3(0, 5f, 0);
            newBead.transform.DOMove(CanUseGrids[NowPos].transform.position, 0.3f).SetEase(Ease.OutExpo);
            NotUseBead.RemoveAt(0);
            //if (BigPos.Contains(NowPos))
            //{
            //    newBead.transform.localScale = newBead.GetComponent<Bead>().bigScale;
            //}
            //else if(SmallPos.Contains(NowPos))
            //{
            //    newBead.transform.localScale = newBead.GetComponent<Bead>().smallScale;
            //}
            //bead.InGridNum = NowPos;
            //newBead.GetComponent<Bead>().InGridNum = NowPos;
            //BeadsInGrids.Add(bead);
            //inGridBeadNum++;
            //if (bead != null)
            //{
            //    //canRemove = true;
            //    NotUseBead.RemoveAt(0);
            //}

            //bool remove = false;
            //for(int j = 0;j<NotUseBead.Count;j++)
            //{
            //    if (!remove && NotUseBead[j].attribute == bead.attribute && NotUseBead[j].grade == bead.grade)
            //    {
            //        NotUseBead.RemoveAt(j);
            //        remove = true;
            //    }
            //}
        }
    }

    private GameObject InitEachBead(int att0, int att1, int grade0, int useNum, int extraGrade,
        int onceUpGrade, int plusGrade, int extraRandom, int[] ShuXingGaiLv)
    {
        GameObject bead = Instantiate(m_bead, NotUseQi);
        Bead beadmodel = bead.GetComponent<Bead>();
        beadmodel.attribute[0] = att0;
        beadmodel.attribute[1] = att1;
        beadmodel.m_game = this;
        beadmodel.grade0 = grade0; 
        beadmodel.useNum = useNum;
        beadmodel.extraGrade = extraGrade;
        beadmodel.onceUpGrade = onceUpGrade;
        beadmodel.plusGrade = plusGrade;
        beadmodel.extraRandom = extraRandom;
        beadmodel.ShuXingGaiLv = new int[5];
        for(int i = 0; i < 5; i++)
        {
            beadmodel.ShuXingGaiLv[i] = ShuXingGaiLv[i];
        }
        return bead;
    }

    private void ClearBead()
    {
        for(int i = usingBeadNum-1; i >=0; i--)
        {
            //Debug.Log(UsingQi.childCount);
            //Debug.Log(i);
            var b = UsingQi.GetChild(i);
            if(b.GetComponent<Bead>().InZhenXing == true)
            {
                //Bead bead = new Bead();
                //bead.attribute[0] = b.GetComponent<Bead>().attribute[0];
                //bead.attribute[1] = b.GetComponent<Bead>().attribute[1];
                //bead.grade = b.GetComponent<Bead>().grade;
                NotUseBead.Add(b.GetComponent<Bead>());
                NotUseBead[NotUseBead.Count - 1].InZhenXing = false;
                NotUseBead[NotUseBead.Count - 1].returnString();
                SkillNowNum++;
                b.GetComponent<Bead>().m_grid.GetComponent<QiCao>().IsUse = false;
                int ab = b.GetComponent<Bead>().m_grid.GetComponent<QiCao>().abscissa;
                int or = b.GetComponent<Bead>().m_grid.GetComponent<QiCao>().ordinate;
                usingBeads[ab, or] = null;
                b.SetParent(null);
                Destroy(b.gameObject);
                usingBeadNum--;
            }
        }
    }
    public void UseSkill()
    {
        if(gameTurn == GameTurn.playerTurn&&SkillNowNum>=SkillNeedNum)
        {
            AudioManager.Instance.PlaySfx("useAbility");
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grids[i,j]!= null)
                    {
                        if (grids[i, j].GetComponent<QiCao>().IsUse)
                        {
                            if (usingBeads[i, j] != null)
                            {
                                usingBeads[i, j].GetComponent<Bead>().SwitchShuXing();
                            }

                        }
                    }
                }
            }
            SkillNowNum = 0;
        }
    }
    void Start()
    {
        //NotUseCard = ChoosedCard;
        //CreateCard();
        //InitBead();
        QiPanInit();
        timer = 0;
        playerImage.transform.localPosition = new Vector3(-20f, 0, 0);
        playerImage.transform.DOLocalMove(Vector3.zero, 0.8f).SetEase(Ease.OutExpo);
        Vector3 enemyStart = enemyImage.transform.localPosition;
        enemyImage.transform.localPosition += new Vector3(20f, 0, 0);
        enemyImage.transform.DOLocalMove(enemyStart, 0.8f).SetEase(Ease.OutExpo);
        //gameTurn = GameTurn.prepare;
    }

    // Update is called once per frame
    void Update() {
        if (enemy.GetComponent<Enemy>().type == 7 && enemy.GetComponent<Enemy>().HP <= 240f)
        {
            enemyImage.GetComponent<SpriteRenderer>().sprite = Fu2Asset;
            enemyTou.GetComponent<SpriteRenderer>().sprite = Fu2Tou;
        }
        if (NotUseCardModel.Count == GameData.HoldCard.Count&&!HasStart)
        {
            HasStart = true;
            gameTurn = GameTurn.prepare;
        }
        timer += Time.deltaTime;
        usingBeadNum = 0;
        HandlePoZhen();
        //if (SkillNowNum >= SkillNeedNum)
        //{
        //    SkillNowNum = SkillNeedNum;
        //    skillImage.transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else
        //{
        //    skillImage.transform.GetChild(0).gameObject.SetActive(false);
        //}
        //if(gameTurn == GameTurn.playerTurn)
        //{
        //    endTurnImage.transform.GetChild(0).gameObject.SetActive(true);
        //}
        //else
        //{
        //    endTurnImage.transform.GetChild(0).gameObject.SetActive(false);
        //}
        AllAttactText.GetComponent<TextMeshPro>().text = "总伤害：" + AllAttact.ToString("F1");
        OnceAttactText.GetComponent<TextMeshPro>().text = "本回合伤害：" + OnceAttact.ToString("F1");
        HuiHeText.GetComponent<TextMeshPro>().text = "回合数：" + HuiHeShu.ToString("F0");
        
        if (player.GetComponent<Player>().HP <= 0&&gameTurn!=GameTurn.fail)
        {
            gameTurn = GameTurn.fail;
            record = false;
            //PlayerPrefs.SetInt("StopExplo", 0);
            //sceneName = "start";
            //transitionManager.LoadScene(sceneName, "Fade", 0f);
        }
        if (enemy.GetComponent<Enemy>().HP <= 0&&gameTurn!=GameTurn.win && gameTurn != GameTurn.useCards && gameTurn != GameTurn.playerChoose)
        {
            gameTurn = GameTurn.win;
            record = false;
            //sceneName = "map";
            //transitionManager.LoadScene(sceneName, "Fade", 0f);
        }
        for ( int i = 0; i < 8; i++)
        {
            for(int j = 0;j< 8; j++)
            {
                if (grids[i,j] != null)
                {
                    if (grids[i, j].GetComponent<QiCao>().IsUse)
                    {
                        usingBeadNum++;
                    }
                }
            }
        }

        HandleGameTurn();

        if (winChooseCard&&!HasEnd)
        {
            HasEnd = true;
            GameData.IsWin = true;
            //咒术存档
            GameData.HoldCard.Clear();
            GameData.IsUpGrade.Clear();
            GameData.HasZhenXing.Clear();
            GameData.ZhenXing.Clear();
            for(int i = 0; i < NotUseCardModel.Count; i++)
            {
                CardModel card = NotUseCardModel[i];
                GameData.HoldCard.Add((int)card.cardType);
                GameData.IsUpGrade.Add(card.IsUpGrade);
                GameData.HasZhenXing.Add(card.HasZhenXing);
                //GameData.ZhenXing.Add(card.ZhenXing);
                List<int[]> cardrow = new List<int[]>();
                if (card.HasZhenXing)
                {
                    for(int j = 0; j < card.ZhenXing.Count; j++)
                    {
                        cardrow.Add(card.ZhenXing[j]);
                    }
                }
                GameData.ZhenXing.Add(cardrow);
            }
            //玩家存档
            GameData.HP = player.GetComponent<Player>().HP;
            GameData.MaxHp = player.GetComponent<Player>().MaxHp;
            //枢元存档
            GameData.ExtraGrade.Clear();
            GameData.OnceUpGrade.Clear();
            GameData.ExtraRandom.Clear();
            GameData.ShuXingGaiLv.Clear();
            for(int i = 0; i < 16; i++)
            {
                GameData.ExtraGrade.Add(NotUseBead[i].extraGrade);
                GameData.OnceUpGrade.Add(NotUseBead[i].onceUpGrade);
                GameData.ExtraRandom.Add(NotUseBead[i].extraRandom);
                int[] gailv = new int[5];
                for(int j = 0; j < 5; j++)
                {
                    gailv[j] = NotUseBead[i].ShuXingGaiLv[j];
                }
                GameData.ShuXingGaiLv.Add(gailv);
            }
            //属性强化存档
            GameData.HuoUp = HuoUp;
            GameData.ShuiUp = ShuiUp;
            GameData.TuUp = TuUp;
            GameData.FengUp = FengUp;
        }
        if (HasEnd&&GameData.HoldCard.Count==NotUseCardModel.Count)
        {
            if (Camera.main.GetComponent<JieSuan>() == null)
                Camera.main.AddComponent<JieSuan>();
        }

    }


    //PoZhen(???)Logic=============================================================
    private void HandlePoZhen() {
        if (IsPozhen) {
            int x = PoZhenX;
            int y = PoZhenY;
            if (PoZhenType == 0) {
                for (int i = x; i < x + 3; i++) {
                    for (int j = y; j < y + 3; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(true);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }

            }
            else if (PoZhenType == 1) {
                for (int i = x; i < x + 2; i++) {
                    for (int j = y; j < y + 4; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(true);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
            }
            else if (PoZhenType == 2) {
                for (int i = x; i < x + 4; i++) {
                    for (int j = y; j < y + 2; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(true);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
        else {
            int x = PoZhenX;
            int y = PoZhenY;
            if (PoZhenType == 0) {
                for (int i = x; i < x + 3; i++) {
                    for (int j = y; j < y + 3; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }

            }
            else if (PoZhenType == 1) {
                for (int i = x; i < x + 2; i++) {
                    for (int j = y; j < y + 4; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }
            }
            else if (PoZhenType == 2) {
                for (int i = x; i < x + 4; i++) {
                    for (int j = y; j < y + 2; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                    //usingBeads[i, j].GetComponent<Bead>().grade0--;
                                }
                                usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                            }
                            grids[i, j].transform.GetChild(1).gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }


    //GameTurnLogic=======================================================
    private void HandleGameTurn() {
        switch (gameTurn) {
            case GameTurn.prepare:
                OnGameTurnPrepared();break;

            case GameTurn.playerTurn:
                OnGameTurnPlayerTurn();break;

            case GameTurn.enemyEffect:
                OnGameTurnEnemyEffect();break;

            case GameTurn.useCards:
                OnGameTurnUseCards();break;

            case GameTurn.playerChoose:
                OnGameTurnPlayerChoose();break;

            case GameTurn.enemyTurn:
                OnGameTurnEnemyTurn();break;

            case GameTurn.fail:
                OnGameTurnFail();break;

            case GameTurn.win:
                OnGameTurnWin();break;
        }
    }
    private void OnGameTurnPrepared() {
        if (!IsPrepared) {
            IsPrepared = true;
            stopTime = 0;
            player.GetComponent<Player>().Defend = 0;
            //enemy.GetComponent<Enemy>().Defend = 0;
            if (enemy.GetComponent<Enemy>().IsXieli > 0) {
                enemy.GetComponent<Enemy>().IsXieli--;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0) {
                enemy.GetComponent<Enemy>().IsLeiRuo--;
            }
            record1 = false;
            clearBuff = false;
            Feng1Record = false;
            Tu2Record = false;
            Feng1 = false;
            Shui1 = false;
            Tu2 = false;
            //enemyHasAct = false;
            act1 = false; act2 = false; act3 = false; act4 = false;
            Eact1 = false; Eact2 = false;
            Eeffect = false;
            clearDefend = false;
            TurnText.GetComponent<TextMeshPro>().text = "准备阶段";
            cam.GetComponent<Physics2DRaycaster>().enabled = false;
            CreateCard();
            ClearBead();
            InitBead();
            maxInitNum = 8;
            //敌人行动
            for (int i = 0; i < 4; i++) {
                enemyActionImage1.transform.GetChild(i).gameObject.SetActive(false);
                enemyActionImage2.transform.GetChild(i).gameObject.SetActive(false);
            }
            enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
            enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
            effect1 = -1; effect2 = -1;
            enemyAction1 = UnityEngine.Random.Range(0, 5);//0攻击1防御2特殊效果3破阵//2:1:1:1
            enemyAction2 = UnityEngine.Random.Range(0, 5);
            while (enemyAction2 == enemyAction1 || (enemyAction1 == 0 && enemyAction2 == 4) || (enemyAction1 == 4 && enemyAction2 == 0))//防止重复
            {
                enemyAction2 = UnityEngine.Random.Range(0, 5);
            }
            switch (enemyAction1) {
                case 0:
                    enemyActionImage1.transform.GetChild(0).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act1value = enemy.GetComponent<Enemy>().attactBasic * HuiHeShu;
                    if (enemy.GetComponent<Enemy>().IsXieli > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 0.8f;
                    }
                    if (player.GetComponent<Player>().IsLeiRuo > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 1.2f;
                    }
                    enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act1value.ToString("F0");
                    break;
                case 4:
                    enemyActionImage1.transform.GetChild(0).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act1value = enemy.GetComponent<Enemy>().attactBasic * HuiHeShu;
                    if (enemy.GetComponent<Enemy>().IsXieli > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 0.8f;
                    }
                    if (player.GetComponent<Player>().IsLeiRuo > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 1.2f;
                    }
                    enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act1value.ToString("F0");
                    break;
                case 1:
                    enemyActionImage1.transform.GetChild(1).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act1value = enemy.GetComponent<Enemy>().defendBasic * HuiHeShu;
                    enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act1value.ToString("F0");
                    break;
                case 2:
                    enemyActionImage1.transform.GetChild(2).gameObject.SetActive(true);
                    effect1 = UnityEngine.Random.Range(0, 4);
                    int i = UnityEngine.Random.Range(0, 100);
                    if (i < 50) {
                        effect2 = UnityEngine.Random.Range(0, 4);
                        while (effect2 == effect1) {
                            effect2 = UnityEngine.Random.Range(0, 4);
                        }
                    }
                    enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
                    break;
                case 3:
                    IsPozhen = true;
                    PoZhenType = UnityEngine.Random.Range(0, 3);
                    if (PoZhenType == 0) {
                        PoZhenX = UnityEngine.Random.Range(2, 6);
                        PoZhenY = UnityEngine.Random.Range(2, 6);
                    }
                    else if (PoZhenType == 1) {
                        PoZhenX = UnityEngine.Random.Range(2, 7);
                        PoZhenY = UnityEngine.Random.Range(2, 5);
                    }
                    else if (PoZhenType == 2) {
                        PoZhenX = UnityEngine.Random.Range(2, 5);
                        PoZhenY = UnityEngine.Random.Range(2, 7);
                    }
                    enemyActionImage1.transform.GetChild(3).gameObject.SetActive(true);
                    enemyActionImage1.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
                    break;

            }
            switch (enemyAction2) {
                case 0:
                    enemyActionImage2.transform.GetChild(0).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act2value = enemy.GetComponent<Enemy>().attactBasic * HuiHeShu;
                    if (enemy.GetComponent<Enemy>().IsXieli > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 0.8f;
                    }
                    if (player.GetComponent<Player>().IsLeiRuo > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 1.2f;
                    }
                    enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act2value.ToString("F0");
                    break;
                case 4:
                    enemyActionImage2.transform.GetChild(0).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act2value = enemy.GetComponent<Enemy>().attactBasic * HuiHeShu;
                    if (enemy.GetComponent<Enemy>().IsXieli > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 0.8f;
                    }
                    if (player.GetComponent<Player>().IsLeiRuo > 0) {
                        enemy.GetComponent<Enemy>().act1value *= 1.2f;
                    }
                    enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act2value.ToString("F0");
                    break;
                case 1:
                    enemyActionImage2.transform.GetChild(1).gameObject.SetActive(true);
                    enemy.GetComponent<Enemy>().act2value = enemy.GetComponent<Enemy>().defendBasic * HuiHeShu;
                    enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = enemy.GetComponent<Enemy>().act2value.ToString("F0");
                    break;
                case 2:
                    enemyActionImage2.transform.GetChild(2).gameObject.SetActive(true);
                    effect3 = UnityEngine.Random.Range(0, 4);
                    int i = UnityEngine.Random.Range(0, 100);
                    if (i < 50) {
                        effect4 = UnityEngine.Random.Range(0, 4);
                        while (effect4 == effect1) {
                            effect4 = UnityEngine.Random.Range(0, 4);
                        }
                    }
                    enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
                    break;
                case 3:
                    IsPozhen = true;
                    PoZhenType = UnityEngine.Random.Range(0, 3);
                    if (PoZhenType == 0) {
                        PoZhenX = UnityEngine.Random.Range(2, 6);
                        PoZhenY = UnityEngine.Random.Range(2, 6);
                    }
                    else if (PoZhenType == 1) {
                        PoZhenX = UnityEngine.Random.Range(2, 7);
                        PoZhenY = UnityEngine.Random.Range(2, 5);
                    }
                    else if (PoZhenType == 2) {
                        PoZhenX = UnityEngine.Random.Range(2, 5);
                        PoZhenY = UnityEngine.Random.Range(2, 7);
                    }
                    enemyActionImage2.transform.GetChild(3).gameObject.SetActive(true);
                    enemyActionImage2.transform.GetChild(4).GetComponent<TextMeshPro>().text = null;
                    break;

            }
            //int index = UnityEngine.Random.Range(0, 100);
            //if (index < 50)
            //{
            //    enemyActionType = CardType.Attact;
            //    enemyActText.GetComponent<TextMeshPro>().text = "即将攻击";
            //}
            //else if (index < 75)
            //{
            //    enemyActionType = CardType.Defend;
            //    enemyActText.GetComponent<TextMeshPro>().text = "即将防御";
            //}
            //else
            //{
            //    enemyActionType = CardType.Special;
            //    enemyActText.GetComponent<TextMeshPro>().text = "特殊效果";
            //}
        }
        if(IsPrepared) {
            if (!record) {
                nowTime = timer;
                record = true;
            }
            float tm = timer - nowTime;
            if (tm >= 0.5f) {
                gameTurn = GameTurn.playerTurn;
                StartText.SetActive(false);
                record = false;
            }
        }
    }
    private void OnGameTurnPlayerTurn() {
        TurnText.GetComponent<TextMeshPro>().text = "玩家";
        cam.GetComponent<Physics2DRaycaster>().enabled = true;
        //if (Input.GetMouseButtonDown(0))
        //{
        for (int i = 0; i < NowCard.Count; i++) {
            NowCard[i].UpdateValue();
        }
        //}
        //if (IsPlacingBead != null && Input.GetMouseButtonDown(0))
        //{
        //    Vector3 mouse = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);

        //    int x = (int)Mathf.Round((mouse.x - QiPanStart.position.x) / QiInterval);
        //    int y = (int)Mathf.Round((mouse.y - QiPanStart.position.y) / QiInterval);
        //    if (grids[x, y] != null && grids[x + 2, y + 2].GetComponent<QiCao>().IsUse == false)
        //    {
        //        IsPlacingBead.transform.position = grids[x + 2, y + 2].transform.position;
        //        grids[x + 2, y + 2].GetComponent<QiCao>().IsUse = true;
        //        IsPlacingBead = null;
        //    }

        //}
    }

    private void OnGameTurnEnemyEffect() {
        if (!record) {
            nowTime = timer;
            record = true;
        }
        float tm = stopTime + timer - nowTime;
        if (tm >= 0.5f && !Eeffect) {
            Eeffect = true;
            if (enemyAction1 == 3 || enemyAction2 == 3) {
                int x = PoZhenX;
                int y = PoZhenY;
                if (PoZhenType == 0) {
                    for (int i = x; i < x + 3; i++) {
                        for (int j = y; j < y + 3; j++) {
                            if (grids[i, j] != null) {
                                if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                    if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                        usingBeads[i, j].GetComponent<Bead>().grade0--;
                                    }
                                    //usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                                }
                                //grids[i,j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else if (PoZhenType == 1) {
                    for (int i = x; i < x + 2; i++) {
                        for (int j = y; j < y + 4; j++) {
                            if (grids[i, j] != null) {
                                if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                    if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                        usingBeads[i, j].GetComponent<Bead>().grade0--;
                                    }
                                    //usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                                }
                                //grids[i,j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                    }
                }
                else if (PoZhenType == 2) {
                    for (int i = x; i < x + 4; i++) {
                        for (int j = y; j < y + 2; j++) {
                            if (grids[i, j] != null) {
                                if (grids[i, j].GetComponent<QiCao>().IsUse) {
                                    if (usingBeads[i, j].GetComponent<Bead>().grade0 > 1) {
                                        usingBeads[i, j].GetComponent<Bead>().grade0--;
                                    }
                                    //usingBeads[i, j].transform.GetChild(3).gameObject.SetActive(false);
                                }
                                //grids[i,j].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                    }
                }
                IsPozhen = false;
            }
            record = false;
            gameTurn = GameTurn.useCards;
        }
    }
    private void OnGameTurnUseCards() {
        //OnceAttact = 0;
        TurnText.GetComponent<TextMeshPro>().text = "玩家结算";
        cam.GetComponent<Physics2DRaycaster>().enabled = false;
        for (int i = 0; i < NowCard.Count; i++) {
            NowCard[i].UpdateValue();
        }
        //float tm = 0;
        //tm += Time.deltaTime;
        if (!record) {
            nowTime = timer;
            record = true;
        }
        float tm = stopTime + timer - nowTime;
        if (!record1) {
            //for(int i = 0; i < NowCard.Count; i++)
            //{
            //    if (NowCard[i].cardType == CardType.WangFu && NowCard[i].value>0 && NowCard[i].num == 0)
            //    {
            //        card1.cardType = card4.cardType;
            //    }
            //}

            for (int i = 0; i < NowCard.Count; i++) {
                if (NowCard[i].cardType != CardType.HuiSu && NowCard[i].num == 0) card1 = NowCard[i];
                else if (NowCard[i].cardType == CardType.HuiSu && NowCard[i].num == 0 && card4 != null) {
                    card1.cardType = card4.cardType;
                    IsHuiSu = 0;
                }
                //if (NowCard[i].cardType != CardType.WangFu && NowCard[i].num == 1) card2 = NowCard[i];
                //if (NowCard[i].cardType != CardType.WangFu && NowCard[i].num == 2) card3 = NowCard[i];
                //if (NowCard[i].cardType != CardType.WangFu && NowCard[i].num == 3) card4 = NowCard[i];
            }
            for (int i = 0; i < NowCard.Count; i++) {
                if (NowCard[i].cardType != CardType.HuiSu && NowCard[i].num == 1) card2 = NowCard[i];
                else if (NowCard[i].cardType == CardType.HuiSu && NowCard[i].num == 1 && card1 != null) {
                    card2.cardType = card1.cardType;
                    IsHuiSu = 1;
                }
            }
            for (int i = 0; i < NowCard.Count; i++) {
                if (NowCard[i].cardType != CardType.HuiSu && NowCard[i].num == 2) card3 = NowCard[i];
                else if (NowCard[i].cardType == CardType.HuiSu && NowCard[i].num == 2 && card2 != null) {
                    card3.cardType = card2.cardType;
                    IsHuiSu = 2;
                }
            }
            for (int i = 0; i < NowCard.Count; i++) {
                if (NowCard[i].cardType != CardType.HuiSu && NowCard[i].num == 3) card4 = NowCard[i];
                else if (NowCard[i].cardType == CardType.HuiSu && NowCard[i].num == 3 && card3 != null) {
                    card4.cardType = card3.cardType;
                    IsHuiSu = 3;
                }
            }
            record1 = true;
        }
        if (tm >= 0.5f && !act1) {
            CardAct(card1, 0, tm);
            act1 = true;
        }
        if (tm >= 1f && !act2) {
            CardLeave(0);
            CardAct(card2, 1, tm);
            act2 = true;
        }
        if (tm >= 1.5f && !act3) {
            CardLeave(1);
            CardAct(card3, 2, tm);
            act3 = true;
        }
        if (tm >= 2f && !act4) {
            CardLeave(2);
            CardAct(card4, 3, tm);
            act4 = true;
        }
        if (tm >= 2.5f) {
            CardLeave(3);
            //水1//土2//风1
            int TuNum = 0;
            int TuValue = 0;
            int FengOnceNum = 0;
            int FengUpNum = 0;
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    if (grids[i, j] != null) {
                        if (grids[i, j].GetComponent<QiCao>().IsUse == true) {
                            if (usingBeads[i, j].GetComponent<Bead>().attribute[0] == 1) {
                                TuNum++;
                                TuValue += usingBeads[i, j].GetComponent<Bead>().grade;
                            }
                            if (usingBeads[i, j].GetComponent<Bead>().attribute[0] == 4) {
                                FengOnceNum++;
                                if (FengOnceNum == 2) {
                                    FengUpNum++;
                                    FengOnceNum = 0;
                                }
                            }
                        }
                    }
                }
            }
            if (Feng1Record == false) {
                Feng1Num = FengUpNum;
                Feng1Record = true;
            }
            if (!Tu2Record) {
                Tu2Record = true;
                Tu2Num = TuNum;
            }
            if (!Tu2) {
                Tu2 = true;
                if (Tu2Num >= 3) {
                    player.GetComponent<Player>().HP += TuValue * (1 + 0.25f * TuUp);
                    AudioManager.Instance.PlaySfx("Cure");
                    if (player.GetComponent<Player>().HP >= player.GetComponent<Player>().MaxHp) {
                        player.GetComponent<Player>().HP = player.GetComponent<Player>().MaxHp;
                    }
                }
            }
            if (!Shui1) {
                Shui1 = true;
                for (int i = 0; i < 8; i++) {
                    for (int j = 0; j < 8; j++) {
                        if (grids[i, j] != null) {
                            if (grids[i, j].GetComponent<QiCao>().IsUse == true) {
                                if (usingBeads[i, j].GetComponent<Bead>().attribute[0] == 2) {
                                    float actualValue = usingBeads[i, j].GetComponent<Bead>().grade * (2.5f + 0.25f * ShuiUp);
                                    if (player.GetComponent<Player>().IsXieli > 0) {
                                        actualValue *= 0.8f;
                                    }
                                    if (enemy.GetComponent<Enemy>().IsLeiRuo > 0) {
                                        actualValue *= 1.2f;
                                    }
                                    AllAttact += actualValue;
                                    OnceAttact += actualValue;
                                    attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
                                    if (actualValue <= enemy.GetComponent<Enemy>().Defend) {
                                        enemy.GetComponent<Enemy>().Defend -= actualValue;
                                        AudioManager.Instance.PlaySfx("guard");
                                    }
                                    else {
                                        enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                                        enemy.GetComponent<Enemy>().HP -= actualValue;
                                        enemy.GetComponent<Enemy>().Defend = 0;
                                        enemy.GetComponent<Animator>().SetTrigger("Hurt");
                                        AudioManager.Instance.PlaySfx("PlayerAttack");
                                    }

                                    if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;

                                }
                            }
                        }
                    }
                }


            }
            if (!Feng1) {
                Feng1 = true;
                if (Feng1Num > 0) {
                    for (int i = 0; i < 8; i++) {
                        for (int j = 0; j < 8; j++) {
                            if (grids[i, j] != null) {
                                if (grids[i, j].GetComponent<QiCao>().IsUse == true) {
                                    usingBeads[i, j].GetComponent<Bead>().grade0 += Feng1Num;
                                    //usingBeads[i, j].GetComponent<Bead>().UpGrade();
                                    //if(usingBeads[i, j].GetComponent<Bead>().attribute[0] != 4)
                                    //{
                                    //    usingBeads[i, j].GetComponent<Bead>().attribute[0] = 0;
                                    //}
                                    //if (usingBeads[i, j].GetComponent<Bead>().attribute[1] != 4)
                                    //{
                                    //    usingBeads[i, j].GetComponent<Bead>().attribute[1] = 0;
                                    //}

                                }
                            }
                        }
                    }
                    if (FengUp > 0) {
                        List<int> suiji = new List<int>();
                        if (FengUp > UsingQi.childCount) {
                            while (suiji.Count < UsingQi.childCount) {
                                int i = UnityEngine.Random.Range(0, UsingQi.childCount);
                                if (!suiji.Contains(i)) {
                                    suiji.Add(i);
                                }
                            }
                        }
                        else {
                            while (suiji.Count < FengUp) {
                                int i = UnityEngine.Random.Range(0, UsingQi.childCount);
                                if (!suiji.Contains(i)) {
                                    suiji.Add(i);
                                }
                            }
                        }
                        for (int i = 0; i < suiji.Count; i++) {
                            UsingQi.GetChild(suiji[i]).GetComponent<Bead>().grade0 += Feng1Num;
                        }
                    }
                }
            }
        }
        if (tm >= 3f) {
            if (!clearBuff) {
                clearBuff = true;
                if (player.GetComponent<Player>().IsXieli > 0) {
                    player.GetComponent<Player>().IsXieli--;

                }
                if (player.GetComponent<Player>().IsLeiRuo > 0) {
                    player.GetComponent<Player>().IsLeiRuo--;
                }
            }
            for (int i = 0; i < NowCard.Count; i++) {
                //Destroy(NowCard[i].gameObject);
                NowCard[i].gameObject.transform.position = new Vector3(0, -100, 0);
                NowCard[i].gameObject.transform.SetParent(null);
            }
            NowCard.Clear();
            gameTurn = GameTurn.enemyTurn;
            record = false;
        }

    }

    private void OnGameTurnPlayerChoose() {
        cam.GetComponent<Physics2DRaycaster>().enabled = true;
        if (needChooseNum == 0) {
            gameTurn = GameTurn.useCards;
            chooseType = ChooseType.No;
        }
    }
    private void OnGameTurnFail() {
        GameObject fail = null;
        if (!record) {
            nowTime = timer;
            record = true;
        }
        float tm = timer - nowTime;
        if (!failPrepare) {
            failPrepare = true;
            AudioManager.Instance.PlayMusic("lose");
            fail = Instantiate(HeiMuFail);
            fail.transform.position = Vector3.zero;
            NotUseCardModel.Add(card1);
            NotUseCardModel.Add(card2);
            NotUseCardModel.Add(card3);
            NotUseCardModel.Add(card4);
        }
        if (tm > 2f) {
            GameData.IsWin = false;
            if (Camera.main.GetComponent<JieSuan>() == null)
                Camera.main.AddComponent<JieSuan>();
            //PlayerPrefs.SetInt("StopExplo", 0);
            //sceneName = "start";
            //transitionManager.LoadScene(sceneName, "Fade", 0f);
        }
    }
    private void OnGameTurnWin() {
        GameObject win = null;
        cam.GetComponent<Physics2DRaycaster>().enabled = true;
        if (!record) {
            nowTime = timer;
            record = true;
        }
        if (!winPrepare1) {
            winPrepare1 = true;
            AudioManager.Instance.PlaySfx("TanChuReward");
            win = Instantiate(HeiMuWin);
            win.transform.position = Vector3.zero;
            //棋盘上枢元放回
            for (int i = 0; i < UsingQi.childCount; i++) {
                Bead bead = UsingQi.GetChild(i).GetComponent<Bead>();
                NotUseBead.Add(bead);
            }
            for (int i = 0; i < NotUseQi.childCount; i++) {
                Bead bead = NotUseQi.GetChild(i).GetComponent<Bead>();
                NotUseBead.Add(bead);
            }
            //咒术
            int c1, c2, c3;
            c1 = UnityEngine.Random.Range(0, 33);
            c2 = UnityEngine.Random.Range(0, 33);
            c3 = UnityEngine.Random.Range(0, 33);
            while (c2 == c1) {
                c2 = UnityEngine.Random.Range(0, 33);
            }
            while (c3 == c1 || c3 == c2) {
                c3 = UnityEngine.Random.Range(0, 33);
            }
            wincard1 = GetRandomCard(AllCard, c1);
            wincard2 = GetRandomCard(AllCard, c2);
            wincard3 = GetRandomCard(AllCard, c3);
            wincard1.GetComponent<CardModel>().OnInit(this, 0);
            wincard2.GetComponent<CardModel>().OnInit(this, 0);
            wincard3.GetComponent<CardModel>().OnInit(this, 0);
            wincard1.GetComponent<CardModel>().IsWinCard = true;
            wincard2.GetComponent<CardModel>().IsWinCard = true;
            wincard3.GetComponent<CardModel>().IsWinCard = true;
            wincard1.SetActive(false);
            wincard2.SetActive(false);
            wincard3.SetActive(false);
            //枢元强化
            int d1, d2, d3;
            d1 = UnityEngine.Random.Range(0, 17);
            d2 = UnityEngine.Random.Range(0, 17);
            d3 = UnityEngine.Random.Range(0, 17);
            while (d2 == d1) {
                d2 = UnityEngine.Random.Range(0, 17);
            }
            while (d3 == d1 || d3 == d2) {
                d3 = UnityEngine.Random.Range(0, 17);
            }
            winQiang1 = Instantiate(ShuYuanQiangHua);
            winQiang2 = Instantiate(ShuYuanQiangHua);
            winQiang3 = Instantiate(ShuYuanQiangHua);
            winQiang1.transform.position = new Vector3(0, 2.2f, 0);
            winQiang2.transform.position = new Vector3(0, 0f, 0);
            winQiang3.transform.position = new Vector3(0, -2.2f, 0);
            winQiang1.GetComponent<winQiangHua>().Num = d1;
            winQiang2.GetComponent<winQiangHua>().Num = d2;
            winQiang3.GetComponent<winQiangHua>().Num = d3;
            winQiang1.GetComponent<winQiangHua>().OnInit(this);
            winQiang2.GetComponent<winQiangHua>().OnInit(this);
            winQiang3.GetComponent<winQiangHua>().OnInit(this);
            winQiang1.SetActive(false);
            winQiang2.SetActive(false);
            winQiang3.SetActive(false);
            if (!act1) NotUseCardModel.Add(card1);
            if (!act2) NotUseCardModel.Add(card2);
            if (!act3) NotUseCardModel.Add(card3);
            if (!act4) NotUseCardModel.Add(card4);
            //NotUseCardModel.Add(card1);
            //NotUseCardModel.Add(card2);
            //NotUseCardModel.Add(card3);
            //NotUseCardModel.Add(card4);
        }
        float tm = timer - nowTime;
        if (!winPrepare && tm > 1.5f) {
            //Debug.Log(1);
            winPrepare = true;
            int e = enemy.GetComponent<Enemy>().type;
            if (e == 6) {
                e = UnityEngine.Random.Range(0, 6);
            }
            if (e < 3) {
                //咒术
                wincard1.SetActive(true);
                wincard2.SetActive(true);
                wincard3.SetActive(true);
                wincard1.GetComponent<SpriteRenderer>().sortingOrder += 100;
                foreach (Transform child in wincard1.transform) {
                    if (child.GetComponent<SpriteRenderer>() != null) {
                        child.GetComponent<SpriteRenderer>().sortingOrder += 100;
                    }
                    else if (child.GetComponent<SortingGroup>() != null) {
                        child.GetComponent<SortingGroup>().sortingOrder += 100;
                    }
                    else {
                        foreach (Transform child1 in child) {
                            if (child1.GetComponent<SpriteRenderer>() != null) {
                                child1.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }

                        }
                    }
                }
                wincard2.GetComponent<SpriteRenderer>().sortingOrder += 100;
                foreach (Transform child in wincard2.transform) {
                    if (child.GetComponent<SpriteRenderer>() != null) {
                        child.GetComponent<SpriteRenderer>().sortingOrder += 100;
                    }
                    else if (child.GetComponent<SortingGroup>() != null) {
                        child.GetComponent<SortingGroup>().sortingOrder += 100;
                    }
                    else {
                        foreach (Transform child1 in child) {
                            if (child1.GetComponent<SpriteRenderer>() != null) {
                                child1.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }

                        }
                    }
                }
                wincard3.GetComponent<SpriteRenderer>().sortingOrder += 100;
                foreach (Transform child in wincard3.transform) {
                    if (child.GetComponent<SpriteRenderer>() != null) {
                        child.GetComponent<SpriteRenderer>().sortingOrder += 100;
                    }
                    else if (child.GetComponent<SortingGroup>() != null) {
                        child.GetComponent<SortingGroup>().sortingOrder += 100;
                    }
                    else {
                        foreach (Transform child1 in child) {
                            if (child1.GetComponent<SpriteRenderer>() != null) {
                                child1.GetComponent<SpriteRenderer>().sortingOrder += 100;
                            }

                        }
                    }
                }
                wincard1.transform.position = WinCardPos.position;
                wincard2.transform.position = WinCardPos.position + new Vector3(interval * 1.5f, 0, 0);
                wincard3.transform.position = WinCardPos.position + new Vector3(interval * 3f, 0, 0);
                var color1 = wincard1.GetComponent<SpriteRenderer>().color;
                var color2 = wincard2.GetComponent<SpriteRenderer>().color;
                var color3 = wincard3.GetComponent<SpriteRenderer>().color;
                wincard1.GetComponent<SpriteRenderer>().color = new Color(color1.r, color1.g, color1.b, 0);
                wincard2.GetComponent<SpriteRenderer>().color = new Color(color2.r, color2.g, color2.b, 0);
                wincard3.GetComponent<SpriteRenderer>().color = new Color(color3.r, color3.g, color3.b, 0);
                wincard1.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                wincard2.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                wincard3.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                //win.transform.GetChild(1).gameObject.SetActive(true);
                //win.transform.GetChild(1).GetComponent<TextMeshPro>().DOFade(1, 0.5f);
                winChooseText.GetComponent<TextMeshPro>().text = "选择一张新的咒术";
                winChooseText.GetComponent<TextMeshPro>().DOFade(1, 0.5f);
            }
            else if (e < 6) {
                //枢元
                winQiang1.SetActive(true);
                winQiang2.SetActive(true);
                winQiang3.SetActive(true);
                //var color = winQiang1.transform.GetChild(0).GetComponent<TextMeshPro>().color;
                for (int i = 1; i < 8; i++) {
                    winQiang1.transform.GetChild(i).GetComponent<TextMeshPro>().DOFade(1, 0.5f);
                    winQiang2.transform.GetChild(i).GetComponent<TextMeshPro>().DOFade(1, 0.5f);
                    winQiang3.transform.GetChild(i).GetComponent<TextMeshPro>().DOFade(1, 0.5f);
                    winQiang1.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                    winQiang2.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                    winQiang3.transform.GetChild(i).GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
                }
                winChooseText.GetComponent<TextMeshPro>().text = "选择一种强化效果";
                winChooseText.GetComponent<TextMeshPro>().DOFade(1, 0.5f);
            }
            skip.SetActive(true);
            skip.transform.GetChild(0).GetComponent<TextMeshPro>().DOFade(1, 0.5f);
            skip.transform.GetChild(1).GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
        }
    }
    private void OnGameTurnEnemyTurn() {
        //bool clearDefend = false;
        if (!clearDefend) {
            enemy.GetComponent<Enemy>().Defend = 0;
            clearDefend = true;
        }
        TurnText.GetComponent<TextMeshPro>().text = "敌方";
        cam.GetComponent<Physics2DRaycaster>().enabled = false;

        //int index = UnityEngine.Random.Range(0, 100);
        //DG.Tweening.Sequence sequence = DOTween.Sequence();
        //sequence.InsertCallback(0.5f, () =>
        if (!record) {
            nowTime = timer;
            record = true;
        }
        float tm = timer - nowTime;
        if (tm >= 0.5f && !Eact1) {
            Eact1 = true;
            switch (enemyAction1) {
                case 0:
                case 4:
                    float actualValue = enemy.GetComponent<Enemy>().act1value;
                    enemyAttackAnimation.GetComponent<Animator>().SetTrigger("enemyAttack");
                    //if (enemy.GetComponent<Enemy>().IsXieli > 0) actualValue *= 0.8f;
                    //if (player.GetComponent<Player>().IsLeiRuo > 0) actualValue *= 1.2f;
                    if (actualValue <= player.GetComponent<Player>().Defend) {
                        player.GetComponent<Player>().Defend -= actualValue;
                        AudioManager.Instance.PlaySfx("guard");
                    }
                    else {
                        player.GetComponent<Player>().HP += player.GetComponent<Player>().Defend;
                        player.GetComponent<Player>().HP -= actualValue;
                        player.GetComponent<Player>().Defend = 0;
                        player.GetComponent<Animator>().SetTrigger("Hurt");
                        AudioManager.Instance.PlaySfx("EnemyAttack");
                    }
                    break;
                case 1:
                    enemyDefendAnimation.GetComponent<Animator>().SetTrigger("defend");
                    enemy.GetComponent<Enemy>().Defend += enemy.GetComponent<Enemy>().act1value;
                    break;
                case 2:
                    switch (effect1) {
                        case 0: enemy.GetComponent<Enemy>().attactBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 1: enemy.GetComponent<Enemy>().defendBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 2:
                            player.GetComponent<Player>().IsXieli = 2;
                            AudioManager.Instance.PlaySfx("debuff");
                            XieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
                            break;
                        case 3:
                            player.GetComponent<Player>().IsLeiRuo = 2;
                            ChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
                            AudioManager.Instance.PlaySfx("debuff");
                            break;
                    }
                    switch (effect2) {
                        case -1: break;
                        case 0: enemy.GetComponent<Enemy>().attactBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 1: enemy.GetComponent<Enemy>().defendBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 2:
                            player.GetComponent<Player>().IsXieli = 2;
                            XieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
                            AudioManager.Instance.PlaySfx("debuff"); break;
                        case 3:
                            player.GetComponent<Player>().IsLeiRuo = 2;
                            ChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
                            AudioManager.Instance.PlaySfx("debuff"); break;
                    }
                    break;
                case 3:
                    break;
            }
        }
        if (tm >= 1f && !Eact2) {
            Eact2 = true;
            switch (enemyAction2) {
                case 0:
                case 4:
                    float actualValue = enemy.GetComponent<Enemy>().act2value;
                    enemyAttackAnimation.GetComponent<Animator>().SetTrigger("enemyAttack");
                    //if (enemy.GetComponent<Enemy>().IsXieli > 0) actualValue *= 0.8f;
                    //if (player.GetComponent<Player>().IsLeiRuo > 0) actualValue *= 1.2f;
                    if (actualValue <= player.GetComponent<Player>().Defend) {
                        player.GetComponent<Player>().Defend -= actualValue;
                        AudioManager.Instance.PlaySfx("guard");
                    }
                    else {
                        player.GetComponent<Player>().HP += player.GetComponent<Player>().Defend;
                        player.GetComponent<Player>().HP -= actualValue;
                        player.GetComponent<Player>().Defend = 0;
                        player.GetComponent<Animator>().SetTrigger("Hurt");
                        AudioManager.Instance.PlaySfx("EnemyAttack");
                    }
                    break;
                case 1:
                    enemyDefendAnimation.GetComponent<Animator>().SetTrigger("defend");
                    enemy.GetComponent<Enemy>().Defend += enemy.GetComponent<Enemy>().act2value;
                    break;
                case 2:
                    switch (effect3) {
                        case 0: enemy.GetComponent<Enemy>().attactBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 1: enemy.GetComponent<Enemy>().defendBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 2:
                            player.GetComponent<Player>().IsXieli = 2;
                            XieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
                            AudioManager.Instance.PlaySfx("debuff");
                            break;
                        case 3:
                            player.GetComponent<Player>().IsLeiRuo = 2;
                            ChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
                            AudioManager.Instance.PlaySfx("debuff"); break;
                    }
                    switch (effect4) {
                        case -1: break;
                        case 0: enemy.GetComponent<Enemy>().attactBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 1: enemy.GetComponent<Enemy>().defendBasic += enemy.GetComponent<Enemy>().AddValue; break;
                        case 2:
                            player.GetComponent<Player>().IsXieli = 2;
                            XieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
                            AudioManager.Instance.PlaySfx("debuff"); break;
                        case 3:
                            player.GetComponent<Player>().IsLeiRuo = 2;
                            ChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
                            AudioManager.Instance.PlaySfx("debuff"); break;
                    }
                    break;
                case 3:
                    break;
            }
        }
        //if(tm>=0.5f&&!enemyHasAct)
        //{
        //    if(enemyActionType == CardType.Attact)
        //    {
        //        int action = UnityEngine.Random.Range(5, 15);
        //        if (action <= player.GetComponent<Player>().Defend)
        //        {
        //            player.GetComponent<Player>().Defend -= action;
        //        }
        //        else
        //        {
        //            player.GetComponent<Player>().HP += player.GetComponent<Player>().Defend;
        //            player.GetComponent<Player>().HP -= action;
        //            player.GetComponent<Player>().Defend = 0;
        //        }
        //    }
        //    else if(enemyActionType == CardType.Defend)
        //    {
        //        int action = UnityEngine.Random.Range(5, 20);
        //        enemy.GetComponent<Enemy>().Defend += action;
        //    }
        //    enemyHasAct = true;
        //if (index < 66)
        //{
        //    int action = UnityEngine.Random.Range(5, 15);
        //    player.GetComponent<Player>().HP -= action;
        //}
        //else
        //{
        //    int action = UnityEngine.Random.Range(2, 6);
        //    enemy.GetComponent<Enemy>().Defend += action;
        //}

        //}
        if (tm > 1.5f) {
            IsPrepared = false;
            gameTurn = GameTurn.prepare;
            HuiHeShu++;
            record = false;
        }
    }



    public void EndPlayerTurn()
    {
        if(gameTurn == GameTurn.playerTurn)
        {
            AudioManager.Instance.PlaySfx("EndAction");
            OnceAttact = 0;
            record = false;
            gameTurn = GameTurn.enemyEffect;
            cam.GetComponent<Physics2DRaycaster>().enabled = false;
        }
    }
    

    private GameObject GetRandomCard(List<CardModel> cards, int index)
    {
        //int index = UnityEngine.Random.Range(0, cards.Count);
        //GameObject card = Instantiate(cards[index], CardStart);
        GameObject card = null;
        //if (cards[index].cardType == CardType.Attact)
        //{
        //    card = Instantiate(m_CardAttactAsset, CardStart);
        //}
        //else if(cards[index].cardType == CardType.Defend)
        //{
        //    card = Instantiate(m_CardDefendAsset, CardStart);
        //}
        //else if(cards[index].cardType == CardType.Special)
        //{
        //    card = Instantiate(m_CardSpecialAsstet, CardStart);
        //}
        switch (cards[index].cardType)
        {
            case CardType.Attact: card = Instantiate(m_CardAttactAsset, CardStart);break;
            case CardType.Defend: card = Instantiate(m_CardDefendAsset, CardStart);break;
            case CardType.Special: card = Instantiate(m_CardSpecialAsstet, CardStart);break;
            case CardType.FengTu: card = Instantiate(m_CardFengTu, CardStart);break;
            case CardType.KongFeng: card = Instantiate(m_CardKongFeng, CardStart);break;
            case CardType.QinHuo: card = Instantiate(m_CardQinHuo, CardStart);break;
            case CardType.YuShui: card = Instantiate(m_CardYuShui, CardStart);break;
            case CardType.HuiSu: card = Instantiate(m_CardHuiSu, CardStart);break;
            case CardType.Juji1: card = Instantiate(m_CardJuJi1, CardStart);break;
            case CardType.Juji2: card = Instantiate(m_CardJuJi2, CardStart);break;
            case CardType.FanFu: card = Instantiate(m_CardFanFu, CardStart);break;
            case CardType.XingKong1: card = Instantiate(m_CardXingKong1, CardStart);break;
            case CardType.XingKong2: card = Instantiate(m_CardXingKong2, CardStart);break;
            case CardType.GuXiu: card = Instantiate(m_CardGuXiu, CardStart);break;
            case CardType.YiShi: card = Instantiate(m_CardYiShi, CardStart);break;
            case CardType.YiLing: card = Instantiate(m_CardYiLing, CardStart);break;
            case CardType.NingShi1: card = Instantiate(m_CardNingShi1, CardStart);break;
            case CardType.NingShi2: card = Instantiate(m_CardNingShi2, CardStart);break;
            case CardType.JiaoXie: card = Instantiate(m_CardJiaoXie, CardStart);break;
            case CardType.ZhenShe: card = Instantiate(m_CardZhenShe, CardStart);break;
            case CardType.QuDao1: card = Instantiate(m_CardQuDao1, CardStart);break;
            case CardType.QuDao2: card = Instantiate(m_CardQuDao2, CardStart);break;
            case CardType.FanZhao1: card = Instantiate(m_CardFanZhao1, CardStart);break;
            case CardType.FanZhao2: card = Instantiate(m_CardFanZhao2, CardStart);break;
            case CardType.LiaoGu1: card = Instantiate(m_CardLiaoGu1, CardStart);break;
            case CardType.LiaoGu2: card = Instantiate(m_CardLiaoGu2, CardStart);break;
            case CardType.LuLi1: card = Instantiate(m_CardLuLi1, CardStart);break;
            case CardType.LuLi2: card = Instantiate(m_CardLuLi2, CardStart);break;
            case CardType.NiLv: card = Instantiate(m_CardNiLv, CardStart);break;
        }
        //card.name = cards[index].name;
        if (cards[index].HasZhenXing)
        {
            card.GetComponent<CardModel>().HasZhenXing = true;
            card.GetComponent<CardModel>().ZhenXing = cards[index].ZhenXing;
        }
        return card;
    }

    public void CreateCard()
    {
        AudioManager.Instance.PlaySfx("updateCard");
        for (int i=0;i<useCardNum;i++)
        {
            //bool remove = false;
            int index = UnityEngine.Random.Range(0, NotUseCardModel.Count);
            GameObject card = GetRandomCard(NotUseCardModel, index);
            if (card != null)
            {
                NotUseCardModel.RemoveAt(index);
            }
            //NotUseCardModel.RemoveAt(index);
            //for (int j=0;j<NotUseCard.Count;j++)
            //{
            //    if (!remove && card.GetComponent<CardModel>().cardType == NotUseCard[j].GetComponent<CardModel>().cardType)
            //    {
            //        NotUseCard.Remove(NotUseCard[j]);
            //        remove = true;
            //    }
            //}
            //if(NotUseCard.Contains(card))
            //{
            //    NotUseCard.Remove(card);
            //    Debug.Log("1");
            //}
            //card.transform.position = new Vector3(i * interval, 0, 0) + CardStart.position;
            card.transform.position = CardStart.position;
            card.transform.DOMove(new Vector3(i * interval, 0, 0) + CardStart.position, 0.5f).SetEase(Ease.OutExpo);
            card.GetComponent<CardModel>().OnInit(this, i);
            NowCard.Add(card.GetComponent<CardModel>());

        }
        //Debug.Log(NowCard.Count);

    }

    public void ChangeCardPos(CardModel cur, CardModel tar)
    {

    }

    private void QiPanInit()
    {
        float QiCaoX = QiPanStart.position.x;
        float QiCaoY = QiPanStart.position.y;
        TextAsset maptxt = Resources.Load("QiPan") as TextAsset;
        string[] map = maptxt.text.Split('\n');

        for (int i = 0; i < map.Length; i++)
        {
            for(int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '0')
                {
                    QiCaoX += QiIntervalX;
                }
                else if (map[i][j] == '1')
                {
                    Vector3 QiPos = new Vector3(QiCaoX, QiCaoY, 0);
                    GameObject emptyQi = Instantiate(m_QiCao, QiPanStart);
                    emptyQi.transform.position = QiPos;
                    emptyQi.GetComponent<QiCao>().OnInit(this, i+2, j+2);
                    grids[i+2,j+2] = emptyQi;
                    QiCaoX += QiIntervalX;
                }
            }
            QiCaoY -= QiIntervalY;
            QiCaoX = QiPanStart.position.x;
        }
    }

    private void CardAct(CardModel card, int n, float tm)
    {
        AudioManager.Instance.PlaySfx("InspireCard");
        if (card.cardType == CardType.Attact || card.cardType == CardType.YiShi || card.cardType == CardType.LuLi1||
            card.cardType == CardType.LuLi2 || card.cardType == CardType.NiLv)
        {
            float actualValue = card.value;
            if (player.GetComponent<Player>().IsXieli > 0)
            {
                actualValue *= 0.8f;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
            {
                actualValue *= 1.2f;
            }
            AllAttact += actualValue;
            OnceAttact += actualValue;
            if (actualValue > 0)
            {
                attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
            }
            //enemy.GetComponent<Enemy>().HP -= card4.value;
            if (actualValue <= enemy.GetComponent<Enemy>().Defend&&actualValue>0)
            {
                enemy.GetComponent<Enemy>().Defend -= actualValue;
                AudioManager.Instance.PlaySfx("guard");
            }
            else if(actualValue> enemy.GetComponent<Enemy>().Defend)
            {
                TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                enemy.GetComponent<Enemy>().HP -= actualValue;
                enemy.GetComponent<Enemy>().Defend = 0;
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                AudioManager.Instance.PlaySfx("PlayerAttack");
            }

            if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
        }
        if (card.cardType == CardType.Defend)
        {
            if (card.value > 0)
            {
                defendAnimation.GetComponent<Animator>().SetTrigger("defend");
            }
            player.GetComponent<Player>().Defend += card.value;
        }
        if (card.cardType == CardType.Special&&card.value>0)
        {
            maxInitNum++;
        }
        if (card.cardType == CardType.FengTu && card.value > 0)
        {
            stopTime += tm;
            record = false;
            needChooseNum = 1;
            chooseType = ChooseType.FengTu;
            gameTurn = GameTurn.playerChoose;
        }
        if (card.cardType == CardType.YuShui && card.value > 0)
        {
            stopTime += tm;
            record = false;
            needChooseNum = 1;
            chooseType = ChooseType.YuShui;
            gameTurn = GameTurn.playerChoose;
        }
        if (card.cardType == CardType.QinHuo && card.value > 0)
        {
            stopTime += tm;
            record = false;
            needChooseNum = 1;
            chooseType = ChooseType.QinHuo;
            gameTurn = GameTurn.playerChoose;
        }
        if (card.cardType == CardType.KongFeng && card.value > 0)
        {
            stopTime += tm;
            record = false;
            needChooseNum = 1;
            chooseType = ChooseType.KongFeng;
            gameTurn = GameTurn.playerChoose;
        }
        if((card.cardType == CardType.Juji1||card.cardType == CardType.Juji2) && card.value > 0)
        {
            float actualValue = player.GetComponent<Player>().Defend;
            if (player.GetComponent<Player>().IsXieli > 0)
            {
                actualValue *= 0.8f;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
            {
                actualValue *= 1.2f;
            }
            AllAttact += actualValue;
            OnceAttact += actualValue;
            if (actualValue > 0)
            {
                attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
            }
            //enemy.GetComponent<Enemy>().HP -= card4.value;
            if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
            {
                enemy.GetComponent<Enemy>().Defend -= actualValue;
                AudioManager.Instance.PlaySfx("guard");
            }
            else if (actualValue > enemy.GetComponent<Enemy>().Defend)
            {
                TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                enemy.GetComponent<Enemy>().HP -= actualValue;
                enemy.GetComponent<Enemy>().Defend = 0;
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                AudioManager.Instance.PlaySfx("PlayerAttack");
            }

            if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;

        }
        if(card.cardType == CardType.FanFu && card.value > 0)
        {
            defendAnimation.GetComponent<Animator>().SetTrigger("defend");
            player.GetComponent<Player>().Defend += OnceAttact;
        }
        if(card.cardType==CardType.YiLing && card.value > 0)
        {
            float actualValue = card.value;
            if (player.GetComponent<Player>().IsXieli > 0)
            {
                actualValue *= 0.8f;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
            {
                actualValue *= 1.2f;
            }
            AllAttact += actualValue;
            OnceAttact += actualValue;
            if (actualValue > 0)
            {
                attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
            }
            //enemy.GetComponent<Enemy>().HP -= card4.value;
            if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
            {
                enemy.GetComponent<Enemy>().Defend -= actualValue;
                AudioManager.Instance.PlaySfx("guard");
            }
            else if (actualValue > enemy.GetComponent<Enemy>().Defend)
            {
                TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                enemy.GetComponent<Enemy>().HP -= actualValue;
                enemy.GetComponent<Enemy>().Defend = 0;
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                AudioManager.Instance.PlaySfx("PlayerAttack");
            }

            if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
            enemyXieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
            enemy.GetComponent<Enemy>().IsXieli = 1;
        }
        if(card.cardType == CardType.NingShi1 && card.value > 0)
        {
            enemyChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
            enemy.GetComponent<Enemy>().IsLeiRuo = 2;
        }
        if(card.cardType == CardType.NingShi2 && card.value > 0)
        {
            enemyChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
            enemy.GetComponent<Enemy>().IsLeiRuo = 1;
        }
        if(card.cardType == CardType.JiaoXie && card.value > 0)
        {
            defendAnimation.GetComponent<Animator>().SetTrigger("defend");
            player.GetComponent<Player>().Defend += card.value;
            enemyXieLiAnimation.GetComponent<Animator>().SetTrigger("XieLi");
            enemy.GetComponent<Enemy>().IsXieli = 1;
        }
        if(card.cardType == CardType.ZhenShe&&card.value > 0)
        {
            enemyChanRuoAnimation.GetComponent<Animator>().SetTrigger("ChanRuo");
            enemy.GetComponent<Enemy>().IsLeiRuo = 1;
            float actualValue = card.value;
            if (player.GetComponent<Player>().IsXieli > 0)
            {
                actualValue *= 0.8f;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
            {
                actualValue *= 1.2f;
            }
            AllAttact += actualValue;
            OnceAttact += actualValue;
            if (actualValue > 0)
            {
                attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
            }
            //enemy.GetComponent<Enemy>().HP -= card4.value;
            if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
            {
                enemy.GetComponent<Enemy>().Defend -= actualValue;
                AudioManager.Instance.PlaySfx("guard");
            }
            else if (actualValue > enemy.GetComponent<Enemy>().Defend)
            {
                TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                enemy.GetComponent<Enemy>().HP -= actualValue;
                enemy.GetComponent<Enemy>().Defend = 0;
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                AudioManager.Instance.PlaySfx("PlayerAttack");
            }

            if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
        }
        if(card.cardType == CardType.QuDao1 && card.value > 0)
        {
            int UpGradeNum = UsingQi.childCount / 2;
            if( UpGradeNum > 0 )
            {
                List<int> suiji = new List<int>();
                while (suiji.Count < UpGradeNum)
                {
                    int i = UnityEngine.Random.Range(0, UsingQi.childCount);
                    if(!suiji.Contains(i))
                    {
                        suiji.Add(i);
                    }
                }
                for(int j = 0;j< suiji.Count;j++)
                {
                    UsingQi.GetChild(suiji[j]).GetComponent<Bead>().grade0 += 2;
                }
            }
        }
        if (card.cardType == CardType.QuDao2 && card.value > 0)
        {
            if(UsingQi.childCount > 0)
            {
                for (int j = 0; j < UsingQi.childCount; j++)
                {
                    UsingQi.GetChild(j).GetComponent<Bead>().grade0 += 1;
                }
            }
        }
        if(card.cardType == CardType.FanZhao1&& card.value > 0)
        {
            float actualValue = 0;
            if (player.GetComponent<Player>().HP <= 5)
            {
                for(int i = 0;i< UsingQi.childCount;i++)
                {
                    actualValue += UsingQi.GetChild(i).GetComponent<Bead>().grade * 2f;
                }
            }
            if(actualValue > 0)
            {
                if (player.GetComponent<Player>().IsXieli > 0)
                {
                    actualValue *= 0.8f;
                }
                if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
                {
                    actualValue *= 1.2f;
                }
                AllAttact += actualValue;
                OnceAttact += actualValue;
                if (actualValue > 0)
                {
                    attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
                }
                //enemy.GetComponent<Enemy>().HP -= card4.value;
                if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
                {
                    enemy.GetComponent<Enemy>().Defend -= actualValue;
                    AudioManager.Instance.PlaySfx("guard");
                }
                else if (actualValue > enemy.GetComponent<Enemy>().Defend)
                {
                    TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                    TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                    enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                    enemy.GetComponent<Enemy>().HP -= actualValue;
                    enemy.GetComponent<Enemy>().Defend = 0;
                    enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    AudioManager.Instance.PlaySfx("PlayerAttack");
                }

                if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
            }
        }
        if(card.cardType == CardType.FanZhao2&& card.value > 0)
        {
            float actualValue = 0;
            if (player.GetComponent<Player>().HP <= 12.5f)
            {
                for(int i = 0;i< UsingQi.childCount;i++)
                {
                    actualValue += UsingQi.GetChild(i).GetComponent<Bead>().grade * 1.25f;
                }
            }
            if(actualValue > 0)
            {
                if (player.GetComponent<Player>().IsXieli > 0)
                {
                    actualValue *= 0.8f;
                }
                if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
                {
                    actualValue *= 1.2f;
                }
                AllAttact += actualValue;
                OnceAttact += actualValue;
                if (actualValue > 0)
                {
                    attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
                }
                //enemy.GetComponent<Enemy>().HP -= card4.value;
                if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
                {
                    enemy.GetComponent<Enemy>().Defend -= actualValue;
                    AudioManager.Instance.PlaySfx("guard");
                }
                else if (actualValue > enemy.GetComponent<Enemy>().Defend)
                {
                    TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                    TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                    enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                    enemy.GetComponent<Enemy>().HP -= actualValue;
                    enemy.GetComponent<Enemy>().Defend = 0;
                    enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    AudioManager.Instance.PlaySfx("PlayerAttack");
                }

                if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
            }
        }
        if((card.cardType == CardType.LiaoGu1 || card.cardType == CardType.LiaoGu2) && card.value > 0)
        {
            if (player.GetComponent<Player>().HP > 5)
            {
                player.GetComponent<Animator>().SetTrigger("Hurt");
                player.GetComponent<Player>().HP -= 5;
                float actualValue = card.value;
                if (player.GetComponent<Player>().IsXieli > 0)
                {
                    actualValue *= 0.8f;
                }
                if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
                {
                    actualValue *= 1.2f;
                }
                AllAttact += actualValue;
                OnceAttact += actualValue;
                if (actualValue > 0)
                {
                    attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
                }
                //enemy.GetComponent<Enemy>().HP -= card4.value;
                if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
                {
                    enemy.GetComponent<Enemy>().Defend -= actualValue;
                    AudioManager.Instance.PlaySfx("guard");
                }
                else if (actualValue > enemy.GetComponent<Enemy>().Defend)
                {
                    TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                    TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                    enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                    enemy.GetComponent<Enemy>().HP -= actualValue;
                    enemy.GetComponent<Enemy>().Defend = 0;
                    enemy.GetComponent<Animator>().SetTrigger("Hurt");
                    AudioManager.Instance.PlaySfx("PlayerAttack");
                }

                if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
            }
        }
        if((card.cardType == CardType.XingKong1||card.cardType == CardType.XingKong2) && card.value > 0)
        {
            float actualValue = card.value;
            if (player.GetComponent<Player>().IsXieli > 0)
            {
                actualValue *= 0.8f;
            }
            if (enemy.GetComponent<Enemy>().IsLeiRuo > 0)
            {
                actualValue *= 1.2f;
            }
            AllAttact += actualValue;
            OnceAttact += actualValue;
            if (actualValue > 0)
            {
                attackAnimation.GetComponent<Animator>().SetTrigger("Attack");
            }
            //enemy.GetComponent<Enemy>().HP -= card4.value;
            if (actualValue <= enemy.GetComponent<Enemy>().Defend && actualValue > 0)
            {
                enemy.GetComponent<Enemy>().Defend -= actualValue;
                AudioManager.Instance.PlaySfx("guard");
            }
            else if (actualValue > enemy.GetComponent<Enemy>().Defend)
            {
                TiaoZi.transform.GetChild(0).GetComponent<TextMeshPro>().text = (actualValue - enemy.GetComponent<Enemy>().Defend).ToString("F0");
                TiaoZi.GetComponent<Animator>().SetTrigger("attack");
                enemy.GetComponent<Enemy>().HP += enemy.GetComponent<Enemy>().Defend;
                enemy.GetComponent<Enemy>().HP -= actualValue;
                enemy.GetComponent<Enemy>().Defend = 0;
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                AudioManager.Instance.PlaySfx("PlayerAttack");
            }

            if (enemy.GetComponent<Enemy>().HP <= 0) enemy.GetComponent<Enemy>().HP = 0;
        }
        if(card.cardType == CardType.GuXiu && card.value > 0)
        {
            defendAnimation.GetComponent<Animator>().SetTrigger("defend");
            player.GetComponent<Player>().Defend += card.value;
        }

        DG.Tweening.Sequence sequence1 = DOTween.Sequence();

        for (int i = 0; i < NowCard.Count; i++)
        {
            if (IsHuiSu != -1)
            {
                if (NowCard[i].num == IsHuiSu)
                {
                    NowCard[i].cardType = CardType.HuiSu;
                    IsHuiSu = -1;
                }
            }
            if (NowCard[i].num == n)
            {
                NotUseCard.Add(NowCard[i].gameObject);
                NotUseCardModel.Add(NowCard[i]);
                //NowCard[i].gameObject.SetActive(false);
                sequence1.Append(NowCard[i].gameObject.transform.DOMoveY((CardStart.position.y + 0.8f), 0.3f).SetEase(Ease.OutExpo));
            }
        }
    }
    private void CardLeave(int n)
    {
        DG.Tweening.Sequence sequence1 = DOTween.Sequence();
        DG.Tweening.Sequence sequence2 = DOTween.Sequence();
        if (needChooseNum == 0)
        {
            for (int i = 0; i < NowCard.Count; i++)
            {
                if (NowCard[i].num == n)
                {
                    sequence1.Append(NowCard[i].gameObject.transform.DOMove(new Vector3(CardLeavePos.position.x, CardLeavePos.position.y, 0), 0.3f).SetEase(Ease.OutExpo));
                    sequence2.Append(NowCard[i].gameObject.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutExpo));
                    //NowCard[i].gameObject.SetActive(false);
                }
            }

        }
    }
}
