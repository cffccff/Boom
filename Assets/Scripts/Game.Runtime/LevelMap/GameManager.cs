using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject[] enemys;
    [SerializeField] GameObject victoryPanel;
    private Transform enemies;
    [SerializeField] GameObject player;

    //Load Enemy
    //  public GameObject[] enemyLevels;
    bool active;

    public int countEnemy;
    private void Start()
    {
     //   enemyLevels = new GameObject[transform.childCount];// cấp phát vùng nhớ

        //enemys = GameObject.FindGameObjectsWithTag("Enemy");
        //countEnemy = enemys.Length;

        //
        //countEnemy = enemies.transform.childCount;
        //Debug.Log("Enemy total:" + countEnemy);
        LoadEnemy();
       // loadEnemy(PlayerPrefs.GetInt("SelectedLevel"));
    }
    private void LoadEnemy()
    {
        enemies = GameObject.Find("Enemies").transform;
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        
        enemies.GetChild(selectedLevel - 1).gameObject.SetActive(true);
        Transform enemiesInSelectedLevel = enemies.GetChild(selectedLevel - 1).gameObject.transform;
        int totalEnemyInSelectedLevel = enemiesInSelectedLevel.childCount;
        countEnemy = totalEnemyInSelectedLevel;
        Debug.Log("Total Enemy is:" + countEnemy);
        for (int i = 0; i < totalEnemyInSelectedLevel; i++)
        {
            enemiesInSelectedLevel.GetChild(i).gameObject.SetActive(true);
        }
    }
    //public void loadEnemy(int level)
    //{       
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        enemyLevels[i] = transform.GetChild(i).gameObject;
    //        enemyLevels[i].SetActive(active = level == i + 1);
    //    }
    //    Debug.Log(level);
    //}

    public void CheckWinStage()// nếu enemy chết thì truyền vào đây
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            victoryPanel.SetActive(true);
            SetNewCurrentLevel();
            saveGold();
        }
    }
    void saveGold()
    {        
        FindObjectOfType<SaveGold>().saveGold();
    }
    private void SetNewCurrentLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("currentLevel");
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
        if (currentLevel == selectedLevel)
        {
            currentLevel++;
            PlayerPrefs.SetInt("currentLevel", currentLevel);
        }
        gameObject.SetActive(false);
        player.SetActive(false);
    }
}
