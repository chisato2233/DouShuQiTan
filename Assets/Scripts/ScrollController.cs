using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ScrollController : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IScrollHandler {
    [SerializeField]
    private float scrollSpeed = 500f;
    [SerializeField]
    private float dragSpeed = 100f;
    [SerializeField]
    private ExploreSystem exploreSystem;
    [SerializeField]
    private GameObject Sider;

    [SerializeField] private float ShowMapTime = 1.0f;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool isDragging;

    public  bool Enable = false;

    private void Awake() {
        if (!exploreSystem) {
            exploreSystem = GameObject.Find("ExploreSystem").GetComponent<ExploreSystem>();
        }
    }

    public void Start() {
        
    }

    private void Update() {
      
    }
    public void ShowMap() {
        var endPosition = transform.position;
        endPosition.y = 10;
        
        transform.DOMove(endPosition, ShowMapTime).OnUpdate(UpdateLines)
            .OnComplete(() => {
                Enable = true;
                StartCoroutine(Sider.GetComponent<TuLi>().AutoShow());
            });
    }
    public void OnBeginDrag(PointerEventData eventData) {
        startPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData) {
        if (!Enable) return;
        
        endPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2 move = endPosition - startPosition;
        Vector2 position = transform.position;
        startPosition = endPosition;
        float newYPosition = Mathf.Clamp(position.y + move.y * Time.deltaTime * dragSpeed, -10f, 10f);
        transform.position = new Vector2(position.x, newYPosition);
        UpdateLines();
    }

    public void OnEndDrag(PointerEventData eventData) {
        isDragging = false;
    }

    public  void UpdateLines() {
        List<List<node>> nodes = exploreSystem.Nodes;
        List<List<GameObject>> nodeObjects = exploreSystem.NodeObjects;

        for (int i = 1; i < nodes.Count; i++) {
            for (int j = 0; j < nodes[i].Count; j++) {
                node node = nodes[i][j];
                foreach (var num in node.LinkedNodes) {
                    GameObject line = GameObject.Find($"line_{i}_{j}_{num}");
                    if (line) {
                        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                        lineRenderer.SetPositions(new Vector3[]
                        {
                            new Vector3(nodeObjects[i-1][num].transform.position.x, nodeObjects[i-1][num].transform.position.y, 0),
                            new Vector3(nodeObjects[i][j].transform.position.x, nodeObjects[i][j].transform.position.y, 0)
                        });
                    }
                }
            }
        }
    }

    public void OnScroll(PointerEventData eventData) {
        if (!Enable) return;
        Vector2 position = transform.position;
        float newYPosition = Mathf.Clamp(position.y - eventData.scrollDelta.y * Time.deltaTime * scrollSpeed, -10f, 10f);
        transform.position = new Vector2(position.x, newYPosition);
        isDragging = true;
        UpdateLines();
    }
}
