using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public GameObject fadeFunction;
    // Start is called before the first frame update

    public void FadeToLevel()
    {
        if (fadeFunction.GetComponent<GameManager>() != null)
        {
            fadeFunction.GetComponent<GameManager>().Menu(fadeFunction.GetComponent<GameManager>().levelToLoad);
        }else if(fadeFunction.GetComponent<MainMenu>() != null)
        {
            fadeFunction.GetComponent<MainMenu>().Level();
        }
    }
}
