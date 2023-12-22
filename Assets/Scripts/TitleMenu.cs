using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Required for loading scenes

public class TitleMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button backToMainMenu;
    [SerializeField] private Button playButton; // Travel to bridge

    [SerializeField] private GameObject mainMenu;

    void Start()
    {
        // Main Menu Buttons
        startButton.onClick.AddListener(() =>
        {
            //SceneManager.LoadScene("10th Bridge");

            // Load Bridge Selection Screen which is hidden behind mainMenu

            Debug.Log("Start button pressed! Menu now " + mainMenu.activeSelf);
        });

        tutorialButton.onClick.AddListener(() =>
        {
            // Currently doesn't exist
            SceneManager.LoadScene("Tutorial");
        });

        exitButton.onClick.AddListener(() =>
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #endif

            Application.Quit();
        });


        // Map menu buttons
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("10th Bridge");
        });
    }

    public void ToggleMainMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
    }
    public void LoadMap(string mapName)
    {
        SceneManager.LoadScene(mapName);
    }
}
