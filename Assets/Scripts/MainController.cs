using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject infoPanel;
    public GameObject mascara;
    public GameObject loadPanel;
    public Slider loadingBar;
    public Text version;

    AsyncOperation async;

    private RectTransform rtMenu;
    private bool menuAnimShow = false;
    private bool menuAnimHide = false;
    private float t;

    void Start()
    {
        version.text = "v" + Application.version;
        rtMenu = menuPanel.GetComponent<RectTransform>();
        t = 0.0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (menuAnimShow)
        {
            t += 3.5f * Time.deltaTime;
            rtMenu.anchoredPosition = new Vector3(Mathf.Lerp(-350f, 350f, t), -370f, 0);            

            if (t > 1.0f)
            {
                menuAnimShow = false;
                t = 0.0f;
                rtMenu.anchoredPosition = new Vector3(350f, -370f, 0);
            }
        }

        if (menuAnimHide)
        {
            t += 3.5f * Time.deltaTime;
            rtMenu.anchoredPosition = new Vector3(Mathf.Lerp(350f, -350f, t), -370f, 0);            

            if (t > 1.0f)
            {
                menuAnimHide = false;
                t = 0.0f;
                rtMenu.anchoredPosition = new Vector3(-350f, -370f, 0);
            }
        }
    }

    public void ShowMenu()
    {
        menuAnimShow = true;
        mascara.SetActive(true);
    }

    public void HideMenu()
    {
        menuAnimHide = true;
        mascara.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void OpenARScene()
    {
        StartCoroutine(LoadingScreen());
    }

    public void OpenUrlFacebook()
    {
        Application.OpenURL("https://www.facebook.com/starteth");
    }

    public void OpenUrlInstagram()
    {
        Application.OpenURL("https://www.instagram.com/star_teth");
    }

    public void OpenUrlLinkedin()
    {
        Application.OpenURL("https://www.linkedin.com/in/starteth-realidade-aumentada-7502a11b1");
    }

    public void OpenUrlSite()
    {
        Application.OpenURL("https://starteth.com.br");
    }

    public void OpenUrlImagensRef()
    {
        Application.OpenURL("https://storage.googleapis.com/santinhos/documentos/exemplos_santinho_digital.pdf");
    }

    public void OpenPoliticaPrivacidade()
    {
        Application.OpenURL("https://starteth.com.br/privacidade-teth");
    }

    public void ShowInfoPanel()
    {
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel()
    {
        infoPanel.SetActive(false);
    }

    IEnumerator LoadingScreen()
    {
        loadPanel.SetActive(true);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while(!async.isDone)
        {
            loadingBar.value = async.progress;
            if(async.progress == 0.9f)
            {
                loadingBar.value = 1.0f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
