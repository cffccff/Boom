using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopIcon : MonoBehaviour, IPointerClickHandler
{
    public GameObject shopPanel;
    private void Start()
    {
        shopPanel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        shopPanel.SetActive(true);
    }
}
