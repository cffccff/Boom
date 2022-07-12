using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour, IPointerClickHandler
{
    public TextMeshProUGUI textTotalGold;
    public Canvas shopPannel;

    [Header("SpeedBuff")]
    public TextMeshProUGUI textCostSpeed;
    public Button click;
    public Image sliceFull;
    int totalGold =100000;
    int costSpeed =1000;
    public int speed =0;
    int level =0;

    public void OnPointerClick(PointerEventData eventData)
    {
        shopPannel.enabled = true;
    }
    void Start()
    {
        TextTotalGold();
    }
    void TextTotalGold()
    {
        textTotalGold.text =$"GOLD {totalGold.ToString()}";
    }
    public void UpSpeed()// gắn vào button speed
    {
        if (level == 5) return;

        if (totalGold >= costSpeed);
        {           
            totalGold -= costSpeed;
            costSpeed = costSpeed * 2;
            speed++;
            level++;
            sliceFull.fillAmount += 0.2f;

            if (level <= 4) textCostSpeed.text = $"{costSpeed.ToString()}";
            else textCostSpeed.text = "MAX";
            TextTotalGold();
        }
    }   
}
