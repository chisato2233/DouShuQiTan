using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skip : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameStart m_game;
    public GameObject ZiDi;
    public void OnPointerClick(PointerEventData eventData)
    {
        m_game.winChooseCard = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ZiDi.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ZiDi.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        ZiDi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
