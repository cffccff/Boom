using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public void loadLevelsSence()
    {
        SceneManager.LoadScene("LevelMap");
        Time.timeScale = 1;
    }
    public void nextLevel()
    {
        FindObjectOfType<GameManager>().loadEnemy(PlayerPrefs.GetInt("SelectedLevel") + 1);//load enemy

        Movement.instance.transform.position = new Vector2 (1, 0);// reset vị trí player

        gameObject.GetComponentInParent<RemoveItem>().remove();// xoá item
        
        LevelManager.instance.loadNextData(); //load map       

        
        gameObject.SetActive(false); //đóng pannel

        Time.timeScale = 1;  //chạy game      
    }
    public void reloadLevel()
    {
        SceneManager.LoadScene("GamePlay");
        LevelManager.instance.LoadLevel();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    
}
