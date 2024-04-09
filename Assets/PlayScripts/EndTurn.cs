using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndTurn : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameStart m_GameStart;
    public GameObject trueEnd;
    public GameObject endTurn2;
    private void Start()
    {
        trueEnd.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (m_GameStart.gameTurn != GameTurn.playerTurn)
        {
            endTurn2.SetActive(true);
        }
        else
        {
            endTurn2.SetActive(false);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        m_GameStart.EndPlayerTurn();
        trueEnd.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (m_GameStart.gameTurn != GameTurn.playerTurn) return;
        trueEnd.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (m_GameStart.gameTurn != GameTurn.playerTurn) return;
        trueEnd.gameObject.SetActive(false);
    }
}
