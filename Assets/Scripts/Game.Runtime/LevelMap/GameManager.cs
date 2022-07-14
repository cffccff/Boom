using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManager levelManager;
   // public GameObject[] enemys;
    [SerializeField] GameObject victoryPanel;
    private GameObject enemies;
    [SerializeField] GameObject losePanel;
    //public Transform enemyParent;
    private int countEnemy;
    private void Start()
    {

        // enemys = GameObject.FindGameObjectsWithTag("Enemy");
        // countEnemy = enemys.Length;
        enemies = GameObject.Find("Enemies");
        countEnemy = enemies.transform.childCount;
        Debug.Log("Enemy total:" + countEnemy);
    }
    public void CheckWinStage()// nếu enemy chết thì truyền vào đây
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            victoryPanel.SetActive(true);
        }
    }
    private void NextLevel()// chuyển qua màn tiếp theo
    {
        levelManager.LoadLevel();

        FindObjectOfType<SaveGold>().saveGold();//truyền vàng kiểm đc của màn trước vào saveGold
    }
}
