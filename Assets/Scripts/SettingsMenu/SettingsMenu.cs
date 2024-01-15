using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] public GameObject _settingsPanel;

    public void showSettings()
    {
        _settingsPanel.SetActive(true);
    }
    public void goBack()
    {
        _settingsPanel.SetActive(false);
    }
}
