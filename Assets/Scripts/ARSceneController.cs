using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARSceneController : MonoBehaviour    
{
    public GameObject camImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainScene();
        }
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void HideCamImage()
    {
        camImage.SetActive(false);
    }

    public void ShowCamImage()
    {
        camImage.SetActive(true);
    }
}
