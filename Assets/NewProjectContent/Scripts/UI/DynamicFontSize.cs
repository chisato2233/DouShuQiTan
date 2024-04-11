using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class DynamicFontSize : MonoBehaviour {
    public int baseScreenHeight = 1440; // 基准屏幕高度
    public int baseFontSize = 36; // 基准字体大小

    private TextMeshProUGUI textComponent;

    void Start() {
        textComponent = GetComponent<TextMeshProUGUI>();
        AdjustFontSize();
    }

    void AdjustFontSize() {
        
        float screenHeightRatio = (float)Screen.height / baseScreenHeight;
        
        textComponent.fontSize = Mathf.RoundToInt(baseFontSize * screenHeightRatio);
    }
}