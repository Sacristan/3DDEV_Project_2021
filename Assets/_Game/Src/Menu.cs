using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}