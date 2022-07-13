using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiGold : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textGold;
    public int gold;
    public void addGold(int _gold)
    {
        gold += _gold;
        textGold.text = $"GOLD {gold.ToString()}";        
    }
}
