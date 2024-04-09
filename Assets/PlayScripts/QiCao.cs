using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class QiCao : MonoBehaviour, IPointerClickHandler
{
    private GameStart m_GameStart;

    public int abscissa = 0;
    public int ordinate = 0;
    public bool IsUse = false;
    public GameObject Selecting;

    public void OnInit(GameStart game, int abscissa, int ordinate)
    {
        m_GameStart = game;
        this.abscissa = abscissa;
        this.ordinate = ordinate;
    }

    private void Awake()
    {
        Selecting.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_GameStart.IsPlacingBead != null)
        {
            AudioManager.Instance.PlaySfx("PlaceBead");
            m_GameStart.IsPlacingBead.transform.position = this.transform.position;
            m_GameStart.IsPlacingBead.transform.SetParent(m_GameStart.UsingQi);
            //m_GameStart.usingBeadNum++;
            //m_GameStart.BeadsInGrids.RemoveAt(m_GameStart.IsPlacingBead.GetComponent<Bead>().InGridNum);
            //m_GameStart.inGridBeadNum--;
            m_GameStart.IsPlacingBead.GetComponent<CircleCollider2D>().enabled = true;
            m_GameStart.IsPlacingBead.GetComponent<Bead>().IsUse = false;
            foreach(Transform child in m_GameStart.IsPlacingBead.transform.GetComponentsInChildren<Transform>())
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
            m_GameStart.IsPlacingBead.GetComponent<Bead>().onTheTop = false;
            m_GameStart.IsPlacingBead.GetComponent<Bead>().OnInitGrid(this.gameObject);
            if (abscissa != 0 && ordinate != 0)
            {
                m_GameStart.usingBeads[abscissa, ordinate] = m_GameStart.IsPlacingBead;
            }
            IsUse = true;
            bool remove = false;
            for (int j = 0; j < m_GameStart.BeadsInGrids.Count; j++)
            {
                if (!remove && m_GameStart.BeadsInGrids[j].attribute == m_GameStart.IsPlacingBead.GetComponent<Bead>().attribute && m_GameStart.BeadsInGrids[j].grade == m_GameStart.IsPlacingBead.GetComponent<Bead>().grade)
                {
                    m_GameStart.BeadsInGrids.RemoveAt(j);
                    remove = true;
                }
            }
            m_GameStart.IsPlacingBead = null;
        }
    }
}
