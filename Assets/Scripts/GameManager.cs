using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public float Gravity = 10f;
    private bool isMenuOpen = false;

    public CinemachineFreeLook followCamera;

    // Fill with all robots to control
    public List<RobotController> robots = new List<RobotController>();

    void Start()
    {
        // Set gravity to simplify math
        Physics.gravity = new Vector3(0, -Gravity, 0);

        // Initially start paused
        Time.timeScale = 0f;

        // Inital selected robot, whatever you want user to start with
        selectRobot(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
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
}

