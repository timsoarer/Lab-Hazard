using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInteractions : MonoBehaviour
{
    public static void GoToMainGame()
    {
        SceneManager.LoadScene(1);
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
