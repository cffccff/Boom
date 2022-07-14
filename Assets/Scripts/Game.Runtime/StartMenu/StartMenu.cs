using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button instructionButton;
    public Button quitButton;
    public GameObject OptionMenu;
    public GameObject MainMenu;
    public GameObject InstructionMenu;
    public Button backOptionMenu;
    public Button backInstructionMenu;
    public GameMusic gameMusic;
    private void Start()
    {
      playButton.onClick.AddListener(PlayGame);
        optionsButton.onClick.AddListener(DisplayOptionPanel);
        instructionButton.onClick.AddListener(DisplayInstructionPanel);
        quitButton.onClick.AddListener(QuitGame);
        backOptionMenu.onClick.AddListener(HideOptionPanel);
        backInstructionMenu.onClick.AddListener(HideInstructionPanel);
    }

    private void PlayGame()
    {
        
    }
    private void DisplayOptionPanel()
    {
        OptionMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
    private void DisplayInstructionPanel()
    {
        InstructionMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
    private void QuitGame()
    {
         Application.Quit();
    }
    private void HideOptionPanel()
    {
        OptionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
    private void HideInstructionPanel()
    {
        InstructionMenu.SetActive(false);
        MainMenu.SetActive(true);
    }
}
