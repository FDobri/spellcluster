using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void LoadSceneClick()
    {
        SceneManager.LoadScene("PlayScene1", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
