using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using DG.Tweening.Plugins.Core.PathCore;
using DouShuQiTan;
using Newtonsoft.Json;
using UnityEngine;

public static class GameData {
    /// <summary>
    /// 无需存档
    /// </summary>
    public static int NanDu = 0;
    //风0-2，云3-5，精英6，Boss7，各位置上的数字表示有多少该敌人
    public static int[] Enemy = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
    //是否属于挑战关，奖励不同
    public static bool IsChallenge = false;
    //是否是第一关
    public static bool IsOne = false;
    //记录该结点为第几个结点，战斗结算用，为了存档
    public static int abscissa;
    public static int ordinate;
    public static bool IsWin = false;
    /// <summary>
    /// 需存档
    /// </summary>
    public static float HP = 50;
    public static float MaxHp = 50;
    //当前持有咒印
    public static List<int> ZhouYin = new List<int> { };


    //牌数不大于16，全部手牌通过字典存储
    //当前持有手牌，一个位置代表一张卡牌，数字为该卡牌类型在字典中的序号，初始化为初始手牌，进入战斗后读取载入对应预制体
    public static List<int> HoldCard = new List<int> { };
    //卡牌是否升级，与上一个List一一对应，表明该牌是否升级，bool值内存小
    public static List<bool> IsUpGrade = new List<bool> { };
    //卡牌是否有阵型
    public static List<bool> HasZhenXing = new List<bool> { };
    //卡牌阵型
    public static List<List<int[]>> ZhenXing = new List<List<int[]>> { };

    public static CardData CardData = new CardData();






    //枢元等级强化效果//初始等级，默认值为0（1级（个别
    public static List<int> ExtraGrade = new List<int> { };
    //每次升级额外升级，默认值为0（每次升一级（个别
    public static List<int> OnceUpGrade = new List<int> { };
    //进化概率，前三次升级进化出属性的概率（全部（提高的次数，每次提高2%）
    public static List<int> ExtraRandom = new List<int> { };
    //属性概率，0无1土2水3火4风（全部（百分号前的数）
    public static List<int[]> ShuXingGaiLv = new List<int[]> { };
    //各属性是否强化
    public static int HuoUp, ShuiUp, TuUp, FengUp;

    public static string Selected咒印Name = null;

    public static void Init() {
        CardData.Init();
    }

    public static void Reset() {
        HP = 50;
        HoldCard.Clear();
        IsUpGrade.Clear();
        HasZhenXing.Clear();
        ZhenXing.Clear();
        ExtraGrade.Clear();
        OnceUpGrade.Clear();
        ExtraRandom.Clear();
        ShuXingGaiLv.Clear();

        CardData.Reset();

        TuUp = 0;
        ShuiUp = 0;
        HuoUp = 0;
        FengUp = 0;
        Selected咒印Name = "";
    }

    public static void Save() {
        if (Selected咒印Name == "") {
            PlayerPrefs.SetString("咒印Name", Selected咒印Name);
        }
    }

    public static void Load() {
        HP = PlayerPrefs.GetFloat("HP",50);
        Selected咒印Name = PlayerPrefs.GetString("咒印Name", "");
    }
   
}

namespace DouShuQiTan {
    public class CardData {
        public Dictionary<string ,DouShuQiTan.CardInfo> CardInfos = new Dictionary<string, CardInfo>();
        public string CardInfoFilePath { get;private set; }

        public void Init() {
            CardInfoFilePath = Application.persistentDataPath + "/CardInfo.json";
        }

        public void Reset() { CardInfos.Clear(); }

        public void LoadCard() {
            string json = File.ReadAllText(CardInfoFilePath);
            CardInfos = JsonConvert.DeserializeObject<Dictionary<string, CardInfo>>(json);
        }

        public void SaveCard() {
            string json = JsonConvert.SerializeObject(CardInfos, Formatting.Indented);
            if (json=="{}") return;
            File.WriteAllText(CardInfoFilePath, json);
        }
    }
}