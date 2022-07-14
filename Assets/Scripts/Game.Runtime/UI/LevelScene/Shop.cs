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
    public GameObject shopPannel;
    ShopData shopData;
    
    [Header("SpeedBuff")]
    public TextMeshProUGUI textCostSpeed;
    //public Button clickSpeed;
    public Image sliceFull;
    public int costSpeed =1000;
    public SaveGold saveGold;

    [Header("BombBuff")]
    public TextMeshProUGUI textCostBomb;
    public int costBomb = 3000;
    public Image sliceBombFull;

    [Header("ExplosionBuff")]
    public TextMeshProUGUI textCostExplosion;
    public int costExplosion = 6000;
    public Image sliceExplosionFull;

    private void Awake()
    {
        saveGold = FindObjectOfType<SaveGold>();
        LoadShopData();// load speedLevel, totalGold

        shopData.totalGold += saveGold.totalGold; //lúc đầu game = 0 nên ko ảnh hưởng// 
        saveGold.totalGold = 0;// load lại scene sau khi + vào totalGold thì reset

        saveGold.speedLevel = shopData.speedLevel; //truyền vào saveGold lúc đầu game
        saveGold.bombLevel = shopData.bombLevel;
        saveGold.explosionLevel = shopData.explosionLevel;

        Load();// load dữ liệu vào có save bên trong         
    }
    void Start()// lúc bắt đầu game và lúc load scene
    {
        LoadShopData();// load speedLevel, totalGold
        //shopData.totalGold = 5000;
        //shopData.speedLevel = 0;
        //shopData.bombLevel = 0;
        //shopData.explosionLevel = 0;


        shopData.totalGold += saveGold.totalGold; //lúc đầu game = 0 nên ko ảnh hưởng// 
        saveGold.totalGold = 0;// load lại scene sau khi + vào totalGold thì reset

        saveGold.speedLevel = shopData.speedLevel; //truyền vào saveGold lúc đầu game
        saveGold.bombLevel = shopData.bombLevel;
        saveGold.explosionLevel = shopData.explosionLevel;


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
    public void UpBomb() //gắn vào button bomb chỉ chạy khi click  
    {
        if (shopData.bombLevel == 5) return;
        if (shopData.totalGold >= costBomb)
        {
            shopData.bombLevel++;
            shopData.totalGold -= costBomb;
            Load();

            saveGold.bombLevel = shopData.bombLevel;// truyền vào saveGold 
        }
    }

    public void UpExplosion() //gắn vào button bomb chỉ chạy khi click  
    {
        if (shopData.explosionLevel == 5) return;
        if (shopData.totalGold >= costExplosion)
        {
            shopData.explosionLevel++;
            shopData.totalGold -= costExplosion;
            Load();

            saveGold.explosionLevel = shopData.explosionLevel;// truyền vào saveGold 
        }
    }
    public void Load()
    {
        //speed
        if (shopData.speedLevel != 0) costSpeed = 1000 * 3 * shopData.speedLevel;
        sliceFull.fillAmount = shopData.speedLevel * 0.2f;

        if (shopData.speedLevel <= 4) textCostSpeed.text = $"{costSpeed.ToString()}";
        else textCostSpeed.text = "MAX";
        //bomb
        if (shopData.bombLevel != 0) costBomb = 3000 * 3 * shopData.bombLevel;
        sliceBombFull.fillAmount = shopData.bombLevel * 0.2f;

        if (shopData.bombLevel <= 4) textCostBomb.text = $"{costBomb.ToString()}";
        else textCostBomb.text = "MAX";
        //explosion
        if (shopData.explosionLevel != 0) costExplosion = 6000 * 3 * shopData.explosionLevel;
        sliceExplosionFull.fillAmount = shopData.explosionLevel * 0.2f;

        if (shopData.explosionLevel <= 4) textCostExplosion.text = $"{costExplosion.ToString()}";
        else textCostExplosion.text = "MAX";

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
            shopData = new ShopData(0, 0, 0, 0);
            SaveShop.Save(shopData);
        }
    }

    public void Esc()
    {
        shopPannel.SetActive(false);
    }
}
