using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARSceneController : MonoBehaviour    
{
    public GameObject scanImage;
    public RectTransform rtRedLine;
    public Text errorTxt;
    public GameObject warningTxt;

    private bool isScanning = true;
    private float t = 0.0f;
    private float initPos = 490.0f;
    private float endPos = -490.0f;
    private bool wait = false;
    private IEnumerator coroutine;

    void Start()
    {
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (!isConnected)
            {
                errorTxt.text = "Verifique sua conexão com a internet";
            }
        }));

        coroutine = ShowWarningMsg(25);
        StartCoroutine(coroutine);
    }    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainScene();
        }

        if (isScanning)
        {
            t += 0.7f * Time.deltaTime;
            rtRedLine.anchoredPosition = new Vector3(0, Mathf.Lerp(initPos, endPos, t), 0);

            if (t > 1.0f)
            {
                float temp = initPos;
                initPos = endPos;
                endPos = temp;
                t = 0.0f;
            }
        }

        if (!wait)
        {
            wait = true;
            StartCoroutine(CheckInternetSeconds(7));
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

    public void HideScanImage()
    {
        scanImage.SetActive(false);
        isScanning = false;
        warningTxt.SetActive(false);
        StopCoroutine(coroutine);
    }

    public void ShowScanImage()
    {
        scanImage.SetActive(true);
        isScanning = true;
        coroutine = ShowWarningMsg(20);
        StartCoroutine(coroutine);
    }

    public void OpenFAQ()
    {
        Application.OpenURL("https://storage.googleapis.com/santinhos/documentos/faq.pdf");
    }

    IEnumerator CheckInternetSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        wait = false;
        StartCoroutine(CheckInternetConnection(isConnected =>
        {
            if (!isConnected)
            {
                errorTxt.text = "Verifique sua conexão com a internet";
            }
            else
            {
                errorTxt.text = "";
            }
        }));
    }

    IEnumerator CheckInternetConnection(Action<bool> action)
    {
        UnityWebRequest request = new UnityWebRequest("https://google.com");
        yield return request.SendWebRequest();
        if (request.error != null)
        {
            action(false);
        }
        else
        {
            action(true);
        }
    }

    IEnumerator ShowWarningMsg(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (isScanning)
        {
            warningTxt.SetActive(true);
        }
    }
}
