using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
public void close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
public void loadLevelsSence()
    {
        SceneManager.LoadScene("LevelMap");
        Time.timeScale = 1;
    }
}
