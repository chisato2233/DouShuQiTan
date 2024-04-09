using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enter()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 65;
    }
    public void Exit()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().fontSize = 50;
    }
}
