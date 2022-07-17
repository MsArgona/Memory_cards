using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{

    public void BackMainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
