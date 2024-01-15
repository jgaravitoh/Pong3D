using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountDown : MonoBehaviour
{

    [SerializeField] private TMP_Text _countDownFront;
    [SerializeField] private TMP_Text _countDownBack;
    [SerializeField] public GameObject _countDownPanel;
    public static bool _isCountingDown = false;

    private void Start()
    {
        StartCoroutine(CountingDown());
    }

    public void StartCountDown()
    {
        
        StartCoroutine(CountingDown());
    }
    public void StopCountDown()
    {
        StopCoroutine(CountingDown());
    }

    IEnumerator CountingDown()
    {
        _isCountingDown = true;
        PauseMenu._isPaused = true;
        Time.timeScale = 0f;
        _countDownPanel.SetActive(true);
        _countDownFront.text = "3";
        _countDownBack.text = "3";
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1));
        _countDownFront.text = "2";
        _countDownBack.text = "2";
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1));
        _countDownFront.text = "1";
        _countDownBack.text = "1";
        yield return StartCoroutine(CoroutineUtilities.WaitForRealTime(1));
        //time up, now resume the app
        _isCountingDown = false;
        _countDownPanel.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu._isPaused = false;

    }
}
public static class CoroutineUtilities
{
    public static IEnumerator WaitForRealTime(float delay)
    {
        while (true)
        {
            float pauseEndTime = Time.realtimeSinceStartup + delay;
            while (Time.realtimeSinceStartup < pauseEndTime)
            {
                yield return 0;
            }
            break;
        }
    }
}