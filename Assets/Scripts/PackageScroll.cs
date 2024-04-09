using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PackageScroll : MonoBehaviour, IDragHandler, IBeginDragHandler,IEndDragHandler
{
    public float bottom;
    public float ScrollSpeed;
    public float DragSpeed;
    bool IsDrag = false;
    bool IsScroll = false;
    Vector2 startPosition;
    Vector2 EndPosition;
    GameObject MapScroll;
    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public void OnDrag(PointerEventData eventData)
    {
        EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 move = EndPosition - startPosition;
        Vector2 position = transform.GetChild(1).position;
        startPosition = EndPosition;
        if (position.y >= 0 && position.y <= bottom)
        {
            IsDrag = true;
            position += new Vector2(0, move.y * Time.deltaTime * DragSpeed);//一定要记得乘以每帧的时间，相当于每帧移动Speed个速度
            if (position.y > bottom)
                position = new Vector2(position.x, bottom);
            if (position.y < 0)
                position = new Vector2(position.x, 0f);
            transform.GetChild(1).position = position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;
    }

    private void Awake()
    {
        MapScroll = GameObject.Find("backGround");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector2 position = transform.GetChild(1).position;
            if (position.y >= 0 && position.y <= bottom)
            {
                IsScroll = true;
                position -= Input.mouseScrollDelta * Time.deltaTime * ScrollSpeed;
                if (position.y > bottom)
                    position = new Vector2(position.x, bottom);
                if (position.y < 0)
                    position = new Vector2(position.x, 0f);
                transform.GetChild(1).position = position;
            }

        }
        else
        {
            IsScroll = false;
        }

        if(IsScroll||IsDrag)
        {
            
            if(MapScroll!=null)
            {
                MapScroll.GetComponent<ScrollController>().enabled = false;
            }
        }
        else
        {
            if (MapScroll != null)
            {
                MapScroll.GetComponent<ScrollController>().enabled = true;
            }

        }

    }
}
