#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEditor.UIElements;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.UI;
using System.Linq;
using NUnit.Framework;


public class CardXlslLoader {
    static string[] resourcePaths = new string[] {
        "Test/Attack",
        "Test/Defend",
        "Test/Special",
        "Test/QinHuo",
        "Test/YuShui",
        "Test/FengTu",
        "Test/KongFeng",
        "Test/WangFu",
        "Test/HuiSu",
        "Test/JuJi1",
        "Test/JuJi2",
        "Test/FanFu",
        "Test/XingKong1",
        "Test/XingKong2",
        "Test/GuXiu",
        "Test/YiShi",
        "Test/YiLing",
        "Test/NingShi1",
        "Test/NingShi2",
        "Test/JiaoXie",
        "Test/ZhenShe",
        "Test/QuDao1",
        "Test/QuDao2",
        "Test/FanZhao1",
        "Test/FanZhao2",
        "Test/LiaoGu1",
        "Test/LiaoGu2",
        "Test/LuLi1",
        "Test/LuLi2",
        "Test/NiLv"
    };



    public static void LoadCardsFromXLSX(string cardPath,int tableIndex,int startRow ) {
        string path = EditorUtility.OpenFilePanel("Select XLSX file", "", "xlsx");
        if (!string.IsNullOrEmpty(path)) {
            LoadXLSXFile(path,cardPath,tableIndex,startRow);
        }
    }

    public static void CreateCardScript(CardLibrary library) {
        foreach (var card in library.Cards) { }
        AssetDatabase.SaveAssets();
    }

    public static void MyActionToCardLibrary(CardLibrary library,GameObject obj) {
        List<Transform> lists = new List<Transform>();
        FindDeepChild(obj.transform.parent, "Square", lists);

        foreach (var s in lists) {
            s.localPosition = new Vector2(-0.24f, 3.6f);
            s.localScale = new Vector3(0.7f, 0.5f, 1.0f);
            s.gameObject.SetActive(true);
            s.GetChild(0).GetComponent<TextMeshPro>().fontSize = 8;
        }
    }


     static void FindDeepChild(Transform aParent, string aName, List<Transform> lists) {
        foreach (Transform child in aParent) {
            if (child.name == aName)
                lists.Add(child);
            FindDeepChild(child, aName,lists);
        }
     }
    static void CreateUIImagePrefab(GameObject sourcePrefab) {
        // Create the root Canvas GameObject
        GameObject Root = new GameObject(sourcePrefab.name + "_UICanvas");
        Root.AddComponent<RectTransform>();
        Canvas canvas = Root.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Root.AddComponent<CanvasScaler>();
        Root.AddComponent<GraphicRaycaster>();

        // Create the root GameObject
        GameObject uiRoot = new GameObject(sourcePrefab.name + "_UI");
        uiRoot.AddComponent<RectTransform>();
        uiRoot.transform.SetParent(Root.transform);

        //Background
        GameObject bg = new GameObject("backGround");
        bg.transform.SetParent(uiRoot.transform);
        Image rootImage = bg.AddComponent<Image>();
        bg.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        
        SpriteRenderer rootSpriteRenderer = sourcePrefab.GetComponent<SpriteRenderer>();
        if (rootSpriteRenderer != null) {
            rootImage.sprite = rootSpriteRenderer.sprite;
            rootImage.SetNativeSize();
        }


        // Iterate through all children of the source prefab
        foreach (Transform child in sourcePrefab.transform) {
            ProcessChild(child, uiRoot.transform);
        }


        GameObject examplePos = new GameObject("Example Pos");
        examplePos.transform.SetParent(uiRoot.transform);
        examplePos.AddComponent<RectTransform>().anchoredPosition = new Vector2(-178, 191);


        // Optionally save the newly created UI prefab
        string path = "Assets/NewProjectContent/Prefabs/CardUI/" + sourcePrefab.name + "_UI.prefab";
        PrefabUtility.SaveAsPrefabAsset(uiRoot, path);
        Debug.Log("Saved new UI prefab to: " + path);

        // Cleanup
        GameObject.DestroyImmediate(uiRoot);
    }

    static void ProcessChild(Transform child, Transform uiParent) {
        // Check for TextMeshPro and SpriteRenderer on the child
        TextMeshPro tmp = child.GetComponent<TextMeshPro>();
        if (tmp != null) {
            GameObject uiText = new GameObject(child.name + "_Text");
            uiText.transform.SetParent(uiParent);
            TextMeshProUGUI tmpUGUI = uiText.AddComponent<TextMeshProUGUI>();
            tmpUGUI.text = tmp.text;
            tmpUGUI.font = tmp.font;
            tmpUGUI.color = tmp.color;
            tmpUGUI.alignment = TextAlignmentOptions.Center;

            if (tmpUGUI.name == "value_Text") {
                tmpUGUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -384);
                tmpUGUI.fontSize = 220; 
                tmpUGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(279.97f, 251.2439f);
            }else if (tmpUGUI.name == "name_Text") {
                tmpUGUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -22);
                tmpUGUI.fontSize = 238.8f;
                tmpUGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(268.3805f, 626.6432f);
            }else if (tmpUGUI.name == "Text (TMP)_Text") {
                tmpUGUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -593.9406f);
                tmpUGUI.fontSize = 82.2f;
                tmpUGUI.GetComponent<RectTransform>().sizeDelta = new Vector2(736.3192f, 269.3612f);
            }
        }

        SpriteRenderer childSpriteRenderer = child.GetComponent<SpriteRenderer>();
        if (childSpriteRenderer != null) {
            GameObject uiImage = new GameObject(child.name + "_Image");
            uiImage.transform.SetParent(uiParent);
            Image image = uiImage.AddComponent<Image>();
            image.sprite = childSpriteRenderer.sprite;

            if (child.name == "Select Effect") {
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                image.SetNativeSize();
                image.transform.SetAsFirstSibling();

            }else if (image.name == "御水_Image") {
                image.GetComponent<RectTransform>().anchoredPosition = new Vector2(16.765f, 489.53f);
                image.GetComponent<RectTransform>().localScale = new Vector3(-0.23f,-0.23f,-0.23f);
                image.SetNativeSize();
                var c = image.color;
                c.a = 0.5f;
                image.color = c;
                image.name = "logo";
            }
        }
    }


    private static void LoadXLSXFile(string filePath, string cardPath, int tableIndex, int startRow) {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        Debug.Log(resourcePaths.Length);

        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read,FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
        DataSet result = excelReader.AsDataSet();
        excelReader.Close();
        stream.Close();
        
        List<CardTemplate> cards = new List<CardTemplate>();


        DataTable table = result.Tables[tableIndex];
        for (int i = startRow; i < table.Rows.Count; i++) {
            DataRow row = table.Rows[i];
            CardTemplate card = ScriptableObject.CreateInstance<DouShuQiTan.CardTemplate>();
            card.Id = int.Parse(row[0].ToString());
            card.Name = row[1].ToString();
            card.Type = row[2].ToString();
            card.枢元Num = row[3].ToString();
            string[] rangeParts = row[4].ToString().Split('*');
            if (rangeParts.Length == 2) {
                card.Range = new Vector2Int(int.Parse(rangeParts[0]), int.Parse(rangeParts[1]));
            }
            else if (row[4].ToString() == "散"){
                card.Range = new Vector2Int(-1,-1);
            }
            card.Describe = row[5].ToString();
            card.UpgradeDescribe = row[8].ToString();
            card.Comment = row[11].ToString();
           

            AssetDatabase.CreateAsset(card, $"{cardPath}/{card.Id}-{card.Name}.asset");
            AssetDatabase.SaveAssets();
            
            cards.Add(card);
        }
        var library = CreateCardLibrary(cards);
        LoadCardFromSO(library);
    }
    
    private static CardLibrary CreateCardLibrary(List<CardTemplate> cards) {
        // 创建CardLibrary的实例并赋值
        CardLibrary library = ScriptableObject.CreateInstance<CardLibrary>();
        library.Cards = cards;

        // 保存ScriptableObject到Assets目录
        AssetDatabase.CreateAsset(library, "Assets/NewProjectContent/CardLibrary.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = library;
        return library;
    }



    private static void LoadCardFromSO(CardLibrary library) {
        // 指定目标路径
        string targetPath = "Assets/NewProjectContent/Prefabs/Card/";
        if (!Directory.Exists(targetPath)) {
            Directory.CreateDirectory(targetPath);
        }

        foreach (var card in library.Cards) {
            if (card.RuntimeCard != null) {
                
                GameObject prefabCopy = GameObject.Instantiate(card.RuntimeCard);
                prefabCopy.name = card.Name; 

                string prefabPath = $"{targetPath}{prefabCopy.name}.prefab";
                PrefabUtility.SaveAsPrefabAsset(prefabCopy, prefabPath);
                GameObject.Destroy(prefabCopy);
                Debug.Log($"Created new prefab at: {prefabPath}");
            }
        }
    }



    private static void CreateAndAttachScript(string cardName, GameObject cardPrefab) {
        var scriptCardName = cardName;
        scriptCardName = scriptCardName.Replace("（", "_");
        scriptCardName = scriptCardName.Replace("）", "");

        Debug.Log($"Create {scriptCardName} ");
        string scriptPath = $"Assets/NewProjectContent/Scripts/Runtime/Card/{scriptCardName}_RuntimeCard.cs";
        if (!File.Exists(scriptPath)) {
            using (StreamWriter sw = new StreamWriter(scriptPath)) {
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("");
                sw.WriteLine("namespace DouShuQiTan{");
                sw.WriteLine($"     public class {scriptCardName}_RuntimeCard : RuntimeCard {{");
                sw.WriteLine("          // Add your script content here");
                sw.WriteLine("      }");
                sw.WriteLine("}");
            }
            AssetDatabase.ImportAsset(scriptPath);
            AssetDatabase.SaveAssets();
        }

       

        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (scriptAsset != null && cardPrefab != null) {
            
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(cardPrefab) as GameObject;
            prefabInstance.AddComponent(scriptAsset.GetClass());
           
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"Assets/NewProjectContent/Prefabs/Card/{cardName}.prefab");
            
            GameObject.DestroyImmediate(prefabInstance);
        }
    }

    
}
#endif
