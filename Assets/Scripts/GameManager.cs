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

    public GameObject drone;
    public GameObject climbingRobot;

    void Start()
    {
        Physics.gravity = new Vector3(0, -Gravity, 0);

        // Initially start paused
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        RobotSelector();
    }

    // Switch Robots
    void RobotSelector()
    {
        // Select Drone
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            drone.GetComponent<DroneController>().enabled = true;
            climbingRobot.GetComponent<ClimbingRobotController>().enabled = false;
            followCamera.Follow = drone.transform;
            followCamera.LookAt = drone.transform;
            Debug.Log("Drone activated!");
        }
        // Select Climbing Robot
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            drone.GetComponent<DroneController>().enabled = false;
            climbingRobot.GetComponent<ClimbingRobotController>().enabled = true;
            followCamera.Follow = climbingRobot.transform;
            followCamera.LookAt = climbingRobot.transform;
            Debug.Log("Climbing Robot activated!");
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

