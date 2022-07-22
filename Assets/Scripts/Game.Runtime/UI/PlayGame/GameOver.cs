using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Button playAgain;
    private void Start()
    {
        GameMusic.Instance.PlayLose();
    }
    public void loadLevelsSence()
    {
        GameMusic.Instance.PlayMusicBackGround();
        SceneManager.LoadScene("LevelMap");
        Time.timeScale = 1;
    }
    public void reloadLevel()
    {
        GameMusic.Instance.PlayMusicBattle();
        gameObject.GetComponentInParent<RemoveItem>().remove();// xoá item
        SceneManager.LoadScene("GamePlay");
       // LevelManager.instance.LoadLevel();
       // gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
