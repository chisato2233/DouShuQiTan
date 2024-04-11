#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using ExcelDataReader;
using System.Data;
using System.Collections.Generic;
using DouShuQiTan;
using UnityEditor.UIElements;


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
        foreach (var card in library.Cards) {
            card.RuntimeCard = AssetDatabase.LoadAssetAtPath($"Assets/NewProjectContent/Prefabs/Card/{card.Name}.prefab", typeof(GameObject)) as GameObject;
            CreateAndAttachScript(card.Name, card.RuntimeCard);
        }
        AssetDatabase.SaveAssets();
    }

    public static void MyActionToCardLibrary(CardLibrary library) {
        foreach (var card in library.Cards) {
            card.RuntimeCard.transform.GetChild(card.RuntimeCard.transform.childCount - 2).name  = "Mask";
        }
        AssetDatabase.SaveAssets();
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
            CardTemplate card = ScriptableObject.CreateInstance<CardTemplate>();
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
            card.RuntimeCard = Resources.Load<GameObject>(resourcePaths[i-startRow]);

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
        }

       

        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (scriptAsset != null && cardPrefab != null) {
            
            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(cardPrefab) as GameObject;
            prefabInstance.AddComponent(scriptAsset.GetClass());
           
            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"Assets/NewProjectContent/Prefabs/Card/{cardName}.prefab");
            
            GameObject.Destroy(prefabInstance);
        }
    }

    
}
#endif
