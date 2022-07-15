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
        LevelManager.instance.loadNextData();
        gameObject.SetActive(false);
        Time.timeScale = 1;        
    }
    public void reloadLevel()
    {
        SceneManager.LoadScene("GamePlay");
        LevelManager.instance.LoadLevel();
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
