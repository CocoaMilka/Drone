using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using Cinemachine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject HUD;

    public float Gravity = 10f;
    private bool isMenuOpen = false;

    public CinemachineFreeLook followCamera;

    // Timer
    public TextMeshProUGUI timerText;
    private float timer;

    // Fill with all robots to control
    public List<RobotController> robots = new List<RobotController>();

    void Start()
    {
        // Set gravity to simplify math
        Physics.gravity = new Vector3(0, -Gravity, 0);

        // Initially start paused
        Time.timeScale = 0f;

        HUD.SetActive(true);

        // Inital selected robot, whatever you want user to start with
        selectRobot(0);

        // Initialize Timer
        timer = 0.0f;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        timer += Time.deltaTime; // Increase the timer by the time since the last frame
        UpdateTimerDisplay();
    }

    // Selects the robot at robotIndex and deselects all other robots
    public void selectRobot(int robotIndex)
    {
        Debug.Log("Button Press");
        for (int i = 0; i < robots.Count; i++)
        {
            if (i == robotIndex)
            {
                robots[i].isSelected = true;

                // Adjust Camera
                followCamera.Follow = robots[i].transform;
                followCamera.LookAt = robots[i].transform;

                Debug.Log(robots[i].name + " is selected.");
            }
            else
            {
                robots[i].isSelected = false;
            }
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

    // Timer stuffs (for scoring)
    private void UpdateTimerDisplay()
    {
        // Format the timer value into minutes:seconds format and display it
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

