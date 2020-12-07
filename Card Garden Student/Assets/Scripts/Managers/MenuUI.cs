using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuUI : SingletonPattern<MenuUI>
{
    [Header("Game UI Screens")]
    public GameObject[] gamePanels;

    [SerializeField] private bool canPause = true;
    [HideInInspector] public bool isPaused = false;

    private void Update()
    {
        if (canPause)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    UnpauseGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }


    // Change Scene by entering Scene Name. Make sure it is loaded in the build settings.
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Quits game, does not work in play mode in editor.
    public void EndGame()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }

    public void PauseGame()
    {
        OpenPanel(0);
        isPaused = true;
        Time.timeScale = 0f;
        GameStateManager.Instance.ChangeState(GameState.GamePause);
    }
    public void UnpauseGame()
    {
        ClosePanel(0);
        isPaused = false;
        Time.timeScale = 1.0f;
        GameStateManager.Instance.ChangeState(GameState.MidEncounter);
    }

    public void RestartScene()
    {
        UnpauseGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Sets panels active and inactive. Technically works for anything just in case, as long as they're in the UIPanel array. Requries index.
    public void OpenPanel(int panelIndex)
    {
        gamePanels[panelIndex].SetActive(true);
    }

    public void ClosePanel(int panelIndex)
    {
        gamePanels[panelIndex].SetActive(false);
    }

    // Alternative just to try some things~
    public void EnableObject(GameObject objectToEnable)
    {
        objectToEnable.SetActive(true);
    }
    public void DisableObject(GameObject objectToDisable)
    {
        objectToDisable.SetActive(false);
    }
}
