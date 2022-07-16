using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemys;
    [SerializeField] GameObject victoryPanel;
    private GameObject enemies;

    //Load Enemy
    public GameObject[] enemyLevels;
    bool active;

    public int countEnemy;
    private void Start()
    {
        enemyLevels = new GameObject[transform.childCount];// cấp phát vùng nhớ

        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        countEnemy = enemys.Length;

        //
        //countEnemy = enemies.transform.childCount;
        //Debug.Log("Enemy total:" + countEnemy);
        loadEnemy(PlayerPrefs.GetInt("SelectedLevel"));
    }
    private void GetParentEnemy()
    {
        enemies = GameObject.Find("Enemies");
        int selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
       // GameObject enemiesInSelectedLevel = enemies.getc
    }
    public void loadEnemy(int level)
    {       
        for (int i = 0; i < transform.childCount; i++)
        {
            enemyLevels[i] = transform.GetChild(i).gameObject;
            enemyLevels[i].SetActive(active = level == i + 1);
        }
        Debug.Log(level);
    }

    public void CheckWinStage()// nếu enemy chết thì truyền vào đây
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            victoryPanel.SetActive(true);
            SetNewCurrentLevel();



            Time.timeScale = 0; 
            saveGold();
        }
    }
    void saveGold()
    {
        Debug.Log("hien thi");
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
    }
}
