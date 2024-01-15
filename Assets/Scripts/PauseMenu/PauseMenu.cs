using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject _pauseMenu;
    public static bool _isPaused;
    public GameObject _settingsPanel;
    private void Start()
    {
        _pauseMenu.SetActive(false);
        _settingsPanel = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsController>()._settingsScreen;
        //I hate myself because of how i had to reference this object, this shit fixes bug of unpausing while being in settings panel


    }
    private void Update()
    {
        if((Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P)) && !CountDown._isCountingDown && !_settingsPanel.activeSelf)
        {
            if(_isPaused == true)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        /*
        if (Input.GetKeyDown(KeyCode.Escape) && CountDown._isCountingDown == true)
        {
            CountDown _countDownComponent = gameObject.GetComponent<CountDown>();
            _countDownComponent._countDownPanel.SetActive(false);
            _countDownComponent.StopCountDown();
            PauseGame();
        }
        */

    }
    public void PauseGame()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        _isPaused = true;
    }
    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        //Time.timeScale = 1f;
        //_isPaused = false;
        gameObject.GetComponent<CountDown>().StartCountDown();
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        _isPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
