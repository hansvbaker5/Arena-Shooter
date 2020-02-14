using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public CanvasGroup canvasGroupMainMenu;

    public static bool GameIsPaused = false;

    private Animator fadeAnimator;

    [HideInInspector]
    public int levelToLoad;

    public GameObject[] guns;

    public Transform spawnPointLeft, spawnPointRight;

    bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(0, -2.5f, 0), Quaternion.identity);
        Instantiate(guns[0], new Vector3(2.5f, -2.5f, 0), Quaternion.identity);

        fadeAnimator = GameObject.Find("Fade").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        GameObject currentPlayer = GameObject.FindGameObjectWithTag("Player");

        if (currentPlayer == null)
        {
            Instantiate(player, new Vector3(0, -2.5f, 0), Quaternion.identity);
            Instantiate(guns[0], new Vector3(2.5f, -2.5f, 0), Quaternion.identity);
        }

        if (currentPlayer.GetComponent<Player>().score == 10)
        {
            if (!spawned)
            {
                Instantiate(guns[1], spawnPointLeft.position, Quaternion.identity);
                Instantiate(guns[2], spawnPointRight.position, Quaternion.identity);
                spawned = true;
            }
        }
    }

    public void Resume()
    {
        AudioListener.pause = false;

        canvasGroupMainMenu.alpha = 0;
        canvasGroupMainMenu.blocksRaycasts = false;

        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        AudioListener.pause = true;

        canvasGroupMainMenu.alpha = 1;
        canvasGroupMainMenu.blocksRaycasts = true;

        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Menu(int levelIndex)
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void FadeToLevel(int levelIndex)
    {
        Time.timeScale = 1f;
        GameIsPaused = false;

        levelToLoad = levelIndex;
        fadeAnimator.SetTrigger("FadeOut");
    }
}

