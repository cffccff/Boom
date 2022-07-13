using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopIcon : MonoBehaviour, IPointerClickHandler
{
    public Canvas shopPannel;
    public void OnPointerClick(PointerEventData eventData)
    {
        shopPannel.enabled = true;
    }
}
