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
    public float timer;

    // Fill with all robots to control
    public List<RobotController> robots = new List<RobotController>();

    // Global access to currently active robot
    public RobotController activeRobot;

    // Create singleton
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // If an instance already exists and it's not this one, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // Optionally, if you want this singleton to persist across scenes, uncomment the following line
        // DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Set gravity to simplify math
        Physics.gravity = new Vector3(0, -Gravity, 0);

        HUD.SetActive(true);

        // Initialize Timer
        timer = 0.0f;
        UpdateTimerDisplay();

        // Initially start paused
        Time.timeScale = 0f;

        // Inital selected robot, whatever you want user to start with
            // Actually, no robot set initally cause I cannot find where this bug is smh
            // Camera is set to look at empty transform and no robot selected initially
        //selectRobot(0);
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

                activeRobot = robots[i];
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

