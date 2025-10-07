using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject MainPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;

    void Start()
    {
        ShowMain(); 
    }

    public void ShowMain()
    {
        MainPanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void ShowLogin()
    {
        MainPanel.SetActive(false);
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    public void ShowRegister()
    {
        MainPanel.SetActive(false);
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
}
