using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Color = System.Drawing.Color;

public class startButton : MonoBehaviour
{
    public int num;
    public startScene start;
    // Start is called before the first frame update
    private void Awake()
    {
        //start = transform.parent.GetComponent<startScene>();
        start = transform.GetComponentInParent<startScene>();
    }

    public void Disable() {
        var newcolor = gameObject.GetComponent<TextMeshProUGUI>().color;
        newcolor.a = 0.3f;
        transform.parent.gameObject.GetComponent<EventTrigger>().enabled = false;
        transform.parent.gameObject.GetComponent<Button>().interactable = false;
        gameObject.GetComponent<TextMeshProUGUI>().color = newcolor;
    }
    public void Enter()
    {
        //IsEnter = true;
        start.Enter(transform.parent.gameObject);
    }
    public void Exit()
    {
        //IsEnter = true;
        start.Exit(transform.parent.gameObject);
    }
}
