using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject[] enemys;
    //public Transform enemyParent;
    public int countEnemy;
    private void Start()
    {

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        countEnemy = enemys.Length;
    }
    public void CheckWinStage()// nếu enemy chết thì truyền vào đây
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            Invoke(nameof(NextLevel), 1f);
        }
    }
    private void NextLevel()// chuyển qua màn tiếp theo
    {
        levelManager.LoadLevel();

        FindObjectOfType<SaveGold>().saveGold();//truyền vàng kiểm đc của màn trước vào saveGold
    }
}
