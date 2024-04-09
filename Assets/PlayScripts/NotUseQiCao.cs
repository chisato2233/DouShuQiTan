using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class NotUseQiCao : MonoBehaviour,IPointerClickHandler
{
    public GameStart m_game;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_game.IsPlacingBead != null)
        {
            AudioManager.Instance.PlaySfx("PlaceBead");
            m_game.IsPlacingBead.transform.position = this.transform.position;
            m_game.IsPlacingBead.transform.SetParent(m_game.NotUseQi);
            m_game.IsPlacingBead.GetComponent<Bead>().IsUse = false;
            m_game.IsPlacingBead.GetComponent<Bead>().m_grid = null;
            m_game.IsPlacingBead.GetComponent<CircleCollider2D>().enabled = true;
            foreach (Transform child in m_game.IsPlacingBead.transform.GetComponentsInChildren<Transform>())
            {
                if (child.GetComponent<SpriteRenderer>() != null)
                {
                    child.GetComponent<SpriteRenderer>().sortingOrder -= 100;
                }
                else if (child.GetComponent<SortingGroup>() != null)
                {
                    child.GetComponent<SortingGroup>().sortingOrder -= 100;
                }

            }
            m_game.IsPlacingBead.GetComponent<Bead>().onTheTop = false;
            m_game.IsPlacingBead = null;
        }
    }
}
