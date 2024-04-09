using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowNeedUse : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public GameStart m_game;
    public void OnPointerClick(PointerEventData eventData)
    {
        m_game.showNeedUse = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/ͼ��");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Art/ͼ�갵");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
