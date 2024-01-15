using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _paddleLeftScoreText;
    [SerializeField] private TMP_Text _paddleRightScoreText;

    [SerializeField] private Transform _paddleLeftTransform;
    [SerializeField] private Transform _paddleRightTransform;
    [SerializeField] private Transform _ballTransform;

    private int _paddleLeftScore;
    private int _paddleRightScore;


    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }


    public void PaddleLeftScored()
    {
        _paddleLeftScore++;
        _paddleLeftScoreText.text = _paddleLeftScore.ToString();
    }

    public void PaddleRightScored()
    {
        _paddleRightScore++;
        _paddleRightScoreText.text = _paddleRightScore.ToString();
    }

    public void Restart()
    {
        _paddleLeftTransform.position = new Vector2(_paddleLeftTransform.position.x, 0);
        _paddleRightTransform.position = new Vector2(_paddleRightTransform.position.x, 0);
        _ballTransform.position = new Vector2(0, 0);
    }

}
