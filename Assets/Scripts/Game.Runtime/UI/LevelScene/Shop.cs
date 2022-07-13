using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public TextMeshProUGUI textTotalGold;
    ShopData shopData;

    [Header("SpeedBuff")]
    public TextMeshProUGUI textCostSpeed;
    public Button click;
    public Image sliceFull;
    public int costSpeed =1000;

    void Start()
    {
        LoadShopData();// load speedLevel, totalGold
        Load();
    }
    void TextTotalGold()
    {
        textTotalGold.text = $"GOLD {shopData.totalGold.ToString()}";
    }
    public void UpSpeed()// gắn vào button speed khi click thì chạy    
    {
        if (shopData.speedLevel == 5) return;

        if (shopData.totalGold > 1000)
        {
            shopData.speedLevel++;
            shopData.totalGold -= costSpeed;
            Load();
        };
    }
    public void Load()
    {
        if (shopData.speedLevel != 0) costSpeed = 1000 * 3 * shopData.speedLevel;
        sliceFull.fillAmount = shopData.speedLevel * 0.2f;

        if (shopData.speedLevel <= 4) textCostSpeed.text = $"{costSpeed.ToString()}";
        else textCostSpeed.text = "MAX";
        TextTotalGold();
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        SaveShop.Save(shopData);
    }
    public void LoadShopData()// load dữ liệu
    {
        shopData = SaveShop.Load();// lấy dữ liệu từ file save
        if(shopData == null)
        {
            shopData = new ShopData(0, 0);
            SaveShop.Save(shopData);
        }
    }
}
