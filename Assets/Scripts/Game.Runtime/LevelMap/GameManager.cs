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
        //enemies = GameObject.Find("Enemies");
        //countEnemy = enemies.transform.childCount;
        //Debug.Log("Enemy total:" + countEnemy);
        loadEnemy();
    }
    public void loadEnemy()
    {       
        for (int i = 0; i < transform.childCount; i++)

        {
            enemyLevels[i] = transform.GetChild(i).gameObject;
            enemyLevels[i].SetActive(active = PlayerPrefs.GetInt("SelectedLevel") == i + 1);
        }
    }

    public void CheckWinStage()// nếu enemy chết thì truyền vào đây
    {
        countEnemy--;
        if (countEnemy <= 0)
        {
            victoryPanel.SetActive(true);
            Time.timeScale = 0; 
            saveGold();
        }
    }
    void saveGold()
    {
        Debug.Log("hien thi");
        FindObjectOfType<SaveGold>().saveGold();
    }
}
