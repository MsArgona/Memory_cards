using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu setting")]
    [SerializeField] private GameObject MainMenuPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject AboutGamePanel;

    //[SerializeField] private AudioClip clickButtonClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        MainMenuPanel.active = true;
        SettingsPanel.active = false;
        AboutGamePanel.active = false;
    }

    void Update()
    {
        
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void BackButton()
    {
        MainMenuPanel.active = true;
        SettingsPanel.active = false;
        AboutGamePanel.active = false;
    }

    public void AboutGame()
    {
        MainMenuPanel.active = false;
        SettingsPanel.active = false;
        AboutGamePanel.active = true;
    }

    public void OpenSettings()
    {
        MainMenuPanel.active = false;
        SettingsPanel.active = true;
        AboutGamePanel.active = false;
    }
}
