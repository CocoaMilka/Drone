using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public float Gravity = 10f;
    private bool isMenuOpen = false;

    void Start()
    {
        Physics.gravity = new Vector3(0, -Gravity, 0);

        // Initially start paused
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;

        if (isMenuOpen)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
        Time.timeScale = 1f; // Resume game
    }

    void OpenMenu()
    {
        menu.SetActive(true);
        Time.timeScale = 0f; // Pause game
    }

    public void ResetScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}

