using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator fadeAnimator;

    public void Fade()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void Level()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
