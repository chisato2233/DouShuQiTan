using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameStart m_game;
    public GameObject skill1;
    public GameObject skill2;
    private void Start()
    {
        skill1.SetActive(false);
    }
    private void Update()
    {
        if (m_game.SkillNowNum >= m_game.SkillNeedNum)
        {
            skill2.SetActive(false);
        }
        else
        {
            skill2.SetActive(true);
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        m_game.UseSkill();
        skill1.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_game.gameTurn != GameTurn.playerTurn||m_game.SkillNowNum<m_game.SkillNeedNum) return;
        skill1.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_game.gameTurn != GameTurn.playerTurn || m_game.SkillNowNum < m_game.SkillNeedNum) return;
        skill1.SetActive(false);
    }
}
