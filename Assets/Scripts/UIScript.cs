using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public int coinNb = 0;

    public static bool gameIsPaused = false;

    [SerializeField]
    Text TxtCoin;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    Texture2D cursorArrow;

    EffectManager effectManager;

    AudioSource audioSource;

    private GameObject[] respawns;

    private void Awake()
    {
        Cursor.SetCursor(cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
        effectManager = GameObject.Find("LevelTransition").GetComponent<EffectManager>();
        audioSource = GetComponent<AudioSource>();
        TxtCoin.text = $"{coinNb}";
    }

    void Update()
    {
        TxtCoin.text = $"{coinNb}";
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            audioSource.volume = .1f;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            audioSource.volume = .5f;
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    public void Resume()
    {
        gameIsPaused = false;
        audioSource.volume = .5f;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScreen");
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Reload()
    {
        if (effectManager)
        {
            effectManager.ReLoad();
        }
    }

    public void ResetKeys()
    {
        respawns = GameObject.FindGameObjectsWithTag("Tangible");
        foreach (GameObject respawn in respawns)
        {
            respawn.GetComponent<DragScript>().ResetKeys();
        }
    }

}
