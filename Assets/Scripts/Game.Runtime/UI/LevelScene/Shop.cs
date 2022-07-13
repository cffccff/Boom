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
    public Canvas shopPannel;
    ShopData shopData;
    
    [Header("SpeedBuff")]
    public TextMeshProUGUI textCostSpeed;
    public Button click;
    public Image sliceFull;
    public int costSpeed =1000;
    public SaveGold saveGold;
    private void Awake()
    {
        saveGold = FindObjectOfType<SaveGold>();
    }
    void Start()// lúc bắt đầu game và lúc load scene
    {
        LoadShopData();// load speedLevel, totalGold
        //shopData.totalGold = 100000;
        shopData.totalGold += saveGold.totalGold; //lúc đầu game = 0 nên ko ảnh hưởng     
        saveGold.speedLevel = shopData.speedLevel; //truyền vào saveGold lúc đầu game
        Load();// load dữ liệu vào có save bên trong         
    }
    void TextTotalGold()
    {
        textTotalGold.text = $"GOLD {shopData.totalGold.ToString()}";
    }
    public void UpSpeed()// gắn vào button speed chỉ chạy khi click  
    {
        if (shopData.speedLevel == 5) return;

        if (shopData.totalGold >= costSpeed)
        {
            shopData.speedLevel++;
            shopData.totalGold -= costSpeed;
            Load();

            saveGold.speedLevel = shopData.speedLevel;// truyền vào saveGold 
        }
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

    public void Esc()
    {
        shopPannel.enabled = false;
    }
}
