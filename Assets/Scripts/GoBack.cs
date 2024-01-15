using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject _panel;

    public void showPanel()
    {
        _panel.SetActive(true);
    }
    public void hidePanel()
    {
        _panel.SetActive(false);
    }
}
