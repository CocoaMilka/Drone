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
            mainMenu.SetActive(false);

            Debug.Log("Start button pressed!");
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



        // Bridge Selection Buttons
        startButton.onClick.AddListener(() =>
        {
            if (mainMenu) { mainMenu.SetActive(true); }
        });

        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("10th Bridge");
        });
    }
}
