using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ScrollController : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public float ScrollSpeed;
    public float DragSpeed;
    private bool IsScroll=false;
    private bool IsDrag=false;
    private ExploreSystem exploreSystem;
    private List<List<node>> Nodes=new List<List<node>> { };
    public List<List<GameObject>> NodeObjects=new List<List<GameObject>> { };
    Vector2 startPosition;
    Vector2 EndPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition= Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        EndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 move = EndPosition - startPosition;
        Vector2 position = transform.position;
        startPosition = EndPosition;
        if (position.y >= -10 && position.y <= 10)
        {
            IsDrag = true;
            position += new Vector2(0, move.y * Time.deltaTime * DragSpeed);//一定要记得乘以每帧的时间，相当于每帧移动Speed个速度
            if (position.y > 10)
                position = new Vector2(position.x, 10);
            if (position.y < -10)
                position = new Vector2(position.x, -10);
            transform.position = position;
        }
        
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        IsDrag = false;
    }
    private void Awake()
    {
        exploreSystem = GameObject.Find("ExploreSystem").GetComponent<ExploreSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector2 position = transform.position;
            if (position.y >= -10 && position.y <= 10)
            {
                IsScroll = true;
                position -= Input.mouseScrollDelta * Time.deltaTime * ScrollSpeed;
                if (position.y > 10)
                    position = new Vector2(position.x, 10);
                if (position.y < -10)
                    position = new Vector2(position.x, -10);
                transform.position = position;
            }

        }
        else
            IsScroll = false;
        if(IsScroll||IsDrag)
        {
            Nodes = exploreSystem.Nodes;
            NodeObjects = exploreSystem.NodeObjects;
            for (int i=1;i<Nodes.Count;i++)
            {
                for(int j=0;j<Nodes[i].Count;j++)
                {
                    node _node = Nodes[i][j];
                    for(int t=0;t<_node.LinkedNodes.Count;t++)
                    {
                        var num = _node.LinkedNodes[t];
                        var line=GameObject.Find("line" + i + j+num);
                        if (line != null)
                        {
                            LineRenderer linerenderer = line.GetComponent<LineRenderer>();

                            linerenderer.SetPositions(
                                new Vector3[]
                                { new Vector3(NodeObjects[i-1][num].transform.position.x,NodeObjects[i-1][num].transform.position.y,0),
                                new Vector3(NodeObjects[i][j].transform.position.x,NodeObjects[i][j].transform.position.y,0) });
                        }

                    }
                }
            }
        }
    }
    
}
