using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeadinPackage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private void Awake()
    {
        transform.GetChild(2).gameObject.SetActive(false);

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.GetChild(2).gameObject.SetActive(false);
    }
}
