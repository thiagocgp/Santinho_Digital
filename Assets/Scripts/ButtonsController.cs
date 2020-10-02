using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public GameObject buttonOptions;
    public GameObject buttonClose;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;

    public string urlButton1 = "";
    public string urlButton2 = "";
    public string urlButton3 = "";
    public string urlButton4 = "";
    public string urlButton5 = "";

    private bool option;
    private bool wait;
    private MeshRenderer mesh;

    void Start()
    {
        option = false;
        wait = false;
        mesh = gameObject.GetComponent<MeshRenderer>();
        ChangeButtonStatus();
    }

    void Update()
    {
        checkButtonUrlClicked();
        ChangeButtonStatus();
    }

    private void checkButtonUrlClicked()
    {
        if (Input.GetMouseButton(0) && !wait)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameobj = hitInfo.collider.gameObject;

                if (gameobj.name.Equals("Button1") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    Application.OpenURL(urlButton1);
                }

                if (gameobj.name.Equals("Button2") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    Application.OpenURL(urlButton2);
                }

                if (gameobj.name.Equals("Button3") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    Application.OpenURL(urlButton3);
                }

                if (gameobj.name.Equals("Button4") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    Application.OpenURL(urlButton4);
                }


                if (gameobj.name.Equals("Button5") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    Application.OpenURL(urlButton5);
                }


                if (gameobj.name.Equals("Button_options") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    option = true;            
                    buttonActiveController();
                }

                if (gameobj.name.Equals("Button_close") && mesh.enabled)
                {
                    wait = true;
                    StartCoroutine(waitSeconds());
                    option = false;
                    buttonActiveController();
                }
                
            }
        }
    }

    private void buttonActiveController()
    {
        if (option)
        {
            buttonOptions.SetActive(false);
            buttonClose.SetActive(true);
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
            button5.SetActive(true);
        }
        else
        {
            buttonOptions.SetActive(true);
            buttonClose.SetActive(false);
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            button5.SetActive(false);
        }
    }

    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(1);
        wait = false;
    }

    public void ChangeButtonStatus()
    {
        if (urlButton1 == "")
        {
            button1.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.15f));
        }
        else
        {
            button1.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
        }

        if (urlButton2 == "")
        {
            button2.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.15f));
        }
        else
        {
            button2.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
        }

        if (urlButton3 == "")
        {
            button3.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.15f));
        }
        else
        {
            button3.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
        }

        if (urlButton4 == "")
        {
            button4.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 0.15f));
        }
        else
        {
            button4.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(1, 1, 1, 1));
        }
    }

}
