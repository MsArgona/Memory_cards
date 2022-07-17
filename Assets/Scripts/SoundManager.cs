using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Image soundStatusButton;
    [SerializeField] Sprite SoundOn;
    [SerializeField] Sprite SoundOff;

    private bool muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
        }

        Load();
        UpdateButtonImage();
    }

    public void SoundButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;          
        }
        else
        {
            muted = false;
            AudioListener.pause = false;
        }

        UpdateButtonImage();
        Save();
    }

    private void UpdateButtonImage()
    {
        if (muted) soundStatusButton.sprite = SoundOn;
        else soundStatusButton.sprite = SoundOff;
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
