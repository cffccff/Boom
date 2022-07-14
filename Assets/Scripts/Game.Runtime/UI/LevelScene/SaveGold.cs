using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//script này dondestroy dùng để truyền dữ liệu giữa 2 scene levels và playgame
public class SaveGold : MonoBehaviour
{
    public UiGold uigold;
    //totalGold sẽ được scripts Shop lấy giá trị để save load
    public int totalGold;

    // truyền giá trị vào các script của player ở scene playgame 
    public int speedLevel;
    public int bombLevel;
    public int explosionLevel;
    private void Start()
    {
        
    }
    public void saveGold() //
    {
        uigold = FindObjectOfType<UiGold>();
        totalGold += uigold.gold; //cộng gold vào totalGold
    }


}
