using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{

    //ID for Powerups
    //0 = Mirror
    //1 = ******
    //2 = ******

    [SerializeField]
    private int powerupID;
    private bool _hitByLeft;

    private Paddle _paddleRight;
    private Paddle _paddleLeft;

    private SoundEffectsBall _sfxBall;


    private void Start()
    {
        _paddleRight = GameObject.Find("Paddle_Right").GetComponent<Paddle>();
        _paddleLeft = GameObject.Find("Paddle_Left").GetComponent<Paddle>();
        _sfxBall = GameObject.FindGameObjectWithTag("Settings").GetComponent<SFXController>()._sfxGameObject.GetComponent<SoundEffectsBall>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            
            //print("la bola me esta tocando las pelotas");
            Ball ball = other.transform.GetComponent<Ball>();
            _sfxBall.PlayPowerUpHitSFX();
            if (ball != null && ball._hitPaddleLeft == true)
            {
                _hitByLeft = true;
                print("PowerUp de izquierda");
                switch (powerupID)
                {
                    
                    case 0:
                        _paddleLeft.MirrorPowerUpActive(_hitByLeft);
                        
                        print("Empieza a spawnear otra vez");
                        break;
                    case 1:
                        
                        _paddleLeft.PaddleEnlargerPowerUpActive(_hitByLeft);
                        
                        print("Empieza a spawnear otra vez raro");
                        break;
                    case 2:
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            if (ball != null && ball._hitPaddleRight == true)
            {
                _hitByLeft = false;
                print("PowerUp de Derecha");
                switch (powerupID)
                {
                    case 0:
                        _paddleRight.MirrorPowerUpActive(_hitByLeft);
                        
                        print("Empieza a spawnear otra vez");
                        break;
                    case 1:
                        
                        _paddleRight.PaddleEnlargerPowerUpActive(_hitByLeft);
                        
                        print("Empieza a spawnear otra vez raro");
                        break;
                    case 2:
                        break;
                    default:
                        Debug.Log("Default Value");
                        break;
                }
            }
            Destroy(gameObject);
        }

    }

    
}
