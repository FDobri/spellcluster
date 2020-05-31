using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    GameObject pauseGamePanel;

    void Start()
    {
        pauseGamePanel.SetActive(false);
        pauseGamePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseGamePanel.activeInHierarchy)
            {
                Pause();
            }
            else if (pauseGamePanel.activeInHierarchy)
            {
                Continue();
            }
        }

        if(Input.GetKeyDown(KeyCode.Q) && (pauseGamePanel.activeInHierarchy))
        {
            Application.Quit();
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        pauseGamePanel.SetActive(true);
    }

    private void Continue()
    {
        Time.timeScale = 1;
        pauseGamePanel.SetActive(false);
    }
}
