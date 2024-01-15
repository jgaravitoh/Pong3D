using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{
    [SerializeField] private float _startVelocity = 4.0f;
    [SerializeField] private float _velocityMultiplier = 1.025f;
    private Rigidbody _ballRb;
    private Paddle _paddleLeft;
    private Paddle _paddleRight;
    public bool _hitPaddleLeft = false;
    public bool _hitPaddleRight = false;

    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private TrailRenderer _trailR;

    //private  GameObject _sfxBallGO;
    private SoundEffectsBall _sfxBall;
    void Start()
    {
        _ballRb = GetComponent<Rigidbody>();
        if (_ballRb == null)
        {
            Debug.LogError("Ball RigidBody is null!");
        }

        _paddleLeft = GameObject.Find("Paddle_Left").GetComponent<Paddle>();
        _paddleRight = GameObject.Find("Paddle_Right").GetComponent<Paddle>();

        if (_paddleLeft == null)
        {
            Debug.LogError("Left Paddle is null!");
        }

        if (_paddleRight == null)
        {
            Debug.LogError("Right Paddle is null!");
        }


        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null!");
        }
        
        LaunchRandom();

        if (_trailR == null)
        {
            Debug.LogError("The Trail Renderer is null!");
        }
        
        //_sfxBallGO = ;

        _sfxBall = GameObject.FindGameObjectWithTag("Settings").GetComponent<SFXController>()._sfxGameObject.GetComponent<SoundEffectsBall>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(_ballRb.velocity.y);
    }

    private void LaunchRandom()
    {
        _trailR.enabled = false;
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        _ballRb.velocity = new Vector3(xVelocity, yVelocity) * _startVelocity;
        _spawnManager.StartSpawning();

        StartCoroutine(DisableTrailTemporally());
        
        //print("spawneando");
    }

    IEnumerator DisableTrailTemporally()
    {
        yield return new WaitForSeconds(0.25f);
        _trailR.enabled = true;
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //Hits a wall...
        if (collision.gameObject.CompareTag("Wall"))
        {
            _sfxBall.PlayHitWallSDX(); //reproduces hit wall sfx
        }
        //Hits a Paddle...
        if (collision.gameObject.CompareTag("PaddleLeft") || collision.gameObject.CompareTag("PaddleRight"))
        {
            _ballRb.velocity *= _velocityMultiplier;
            if (collision.gameObject.CompareTag("PaddleLeft")){
                _hitPaddleLeft = true;
                _hitPaddleRight = false;
            }
            if (collision.gameObject.CompareTag("PaddleRight")){
                _hitPaddleRight = true;
                _hitPaddleLeft = false;
            }

            _sfxBall.PlayBallHitSFX(); //reproduces hit sfx

        }
    }

    //Enters a Goal...
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("GoalLeft"))
        {
            _spawnManager.StopSpawning();
            _spawnManager.DestroyPowerUp();
            _spawnManager._spawnCounter = 0;
            //print("deja de spawnear");
            GameManager.Instance.PaddleRightScored();

            _paddleLeft._isMirrorPowerUpActive = false;
            _paddleLeft._leftMirrorImg.SetActive(false);
            _paddleRight._isMirrorPowerUpActive = false;
            _paddleLeft._rightMirrorImg.SetActive(false);

            _paddleLeft._isPaddleEnlargerPowerUpActive = false;
            _paddleLeft._leftEnlargerImg.SetActive(false);
            _paddleRight._isPaddleEnlargerPowerUpActive = false;
            _paddleLeft._rightEnlargerImg.SetActive(false);

            _sfxBall.PlayBallHitGoalSFX(); //reproduces scored sfx

            GameManager.Instance.Restart();

            LaunchRandom();
        }
        else if (collision.gameObject.CompareTag("GoalRight"))
        {
            _spawnManager.StopSpawning();
            _spawnManager.DestroyPowerUp();
            _spawnManager._spawnCounter = 0;
            
            GameManager.Instance.PaddleLeftScored();

            _paddleLeft._isMirrorPowerUpActive = false;
            _paddleLeft._leftMirrorImg.SetActive(false);
            _paddleRight._isMirrorPowerUpActive = false;
            _paddleLeft._rightMirrorImg.SetActive(false);

            _paddleLeft._isPaddleEnlargerPowerUpActive = false;
            _paddleLeft._leftEnlargerImg.SetActive(false);
            _paddleRight._isPaddleEnlargerPowerUpActive = false;
            _paddleLeft._rightEnlargerImg.SetActive(false);

            _sfxBall.PlayBallHitGoalSFX(); //reproduces scored sfx

            GameManager.Instance.Restart();



            LaunchRandom();
        
        }
    }




}
