#if UNITY_EDITOR
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DouShuQiTan;
using UnityEditor;
using UnityEngine;

public class 枢元UpgradeLoadXlsl : EditorWindow {
    [MenuItem("Tools/Load ShuYuan Upgrade Effects")]
    public static void ShowWindow() {
        GetWindow<枢元UpgradeLoadXlsl>("Load 枢元 Upgrade Effects");
    }

    void OnGUI() {
        if (GUILayout.Button("Load Effects From Excel")) {
            LoadEffectsFromExcel();
        }
    }

    private static void LoadEffectsFromExcel() {
        string path = EditorUtility.OpenFilePanel("Select Excel File", "", "xlsx");
        if (string.IsNullOrEmpty(path)) return;

        FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateReader(stream);
        DataSet result = excelReader.AsDataSet();
        excelReader.Close();
        stream.Close();

        List<枢元UpgradeTemplate> effects = new List<枢元UpgradeTemplate>();
        
        DataTable table = result.Tables[0]; // 假设数据在第一个表中
        foreach (DataRow row in table.Rows) {
            枢元UpgradeTemplate effect = ScriptableObject.CreateInstance<枢元UpgradeTemplate>();
            effect.Id = int.Parse(row[0].ToString());
            effect.Name = row[1].ToString();
            effect.Type = row[2].ToString();
            effect.Description = row[3].ToString();
            effect.Comment = row[6].ToString();

            // 保存ScriptableObject
            string assetPath = $"Assets/NewProjectContent/ScriptableObjects/枢元UpgradeSO/{effect.Name}.asset";
            AssetDatabase.CreateAsset(effect, assetPath);
        }
        AssetDatabase.SaveAssets();
        Debug.Log("ShuYuan Upgrade Effects loaded successfully.");
    }

    
    private static 枢元UpgradeLibrary CreateEffectLibrary(List<枢元UpgradeTemplate> effects) {
        // 创建CardLibrary的实例并赋值
        枢元UpgradeLibrary library = ScriptableObject.CreateInstance<枢元UpgradeLibrary>();
        library.枢元Upgrades= effects;

        // 保存ScriptableObject到Assets目录
        AssetDatabase.CreateAsset(library, "Assets/NewProjectContent/Libraries/枢元UpgradeLibrary.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = library;
        return library;
    }

    private static void CreateAndAttachScript(string effectName, GameObject prefab) {
        var scriptEffectName = effectName;
        scriptEffectName = scriptEffectName.Replace("（", "_");
        scriptEffectName = scriptEffectName.Replace("）", "");

        Debug.Log($"Create {scriptEffectName} ");
        string scriptPath = $"Assets/NewProjectContent/Runtime/枢元UpgradeScripts/{scriptEffectName}_Runtime枢元Upgrade.cs";
        if (!File.Exists(scriptPath)) {
            using (StreamWriter sw = new StreamWriter(scriptPath)) {
                sw.WriteLine("using UnityEngine;");
                sw.WriteLine("");
                sw.WriteLine("namespace DouShuQiTan{");
                sw.WriteLine($"     public class {scriptEffectName}_Runtime枢元Upgrade : Runtime枢元Upgrade {{");
                sw.WriteLine("          // Add your script content here");
                sw.WriteLine("      }");
                sw.WriteLine("}");
            }
            AssetDatabase.ImportAsset(scriptPath);
        }



        var scriptAsset = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptPath);
        if (prefab == null) {

        }
        if (scriptAsset != null && prefab != null) {

            GameObject prefabInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            prefabInstance.AddComponent(scriptAsset.GetClass());

            PrefabUtility.SaveAsPrefabAsset(prefabInstance, $"Assets/NewProjectContent/Prefabs/枢元Upgrade/{effectName}.prefab");

            GameObject.Destroy(prefabInstance);
        }
    }

    private static void CreateNewPrefab(string name, string path) {
        // 创建新的游戏物体
        GameObject newGameObject = new GameObject(name);

        // 确保路径以'/'结尾
        if (!path.EndsWith("/")) path += "/";
        string fullPath = $"{path}{name}.prefab";

        // 检查路径是否存在，不存在则创建
        if (!AssetDatabase.IsValidFolder(path)) {
            string[] folders = path.Split('/');
            string currentPath = folders[0];
            for (int i = 1; i < folders.Length; i++) {
                currentPath += "/" + folders[i];
                if (!AssetDatabase.IsValidFolder(currentPath)) {
                    AssetDatabase.CreateFolder(currentPath.Substring(0, currentPath.LastIndexOf('/')), folders[i]);
                }
            }
        }

        // 保存游戏物体为预制体
        PrefabUtility.SaveAsPrefabAsset(newGameObject, fullPath);

        // 销毁场景中的临时游戏物体
        DestroyImmediate(newGameObject);

        Debug.Log($"Prefab created at {fullPath}");
    }
}
#endif