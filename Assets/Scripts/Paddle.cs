using System.Collections;
using UnityEngine;
using System;

using UnityEngine.UI;
public class Paddle : MonoBehaviour
{
    private float _speed = 9.5f;

    [SerializeField] private bool _isPaddleLeft;

    private float VerticalBound = 4.5f;
    private float VerticalBoundForEnlarger = 3.8f;

    public bool _isMirrorPowerUpActive = false;
    public bool _isPaddleEnlargerPowerUpActive = false;
    public bool _isPaddleEnlargerPowerUpUsed = false;

    [SerializeField] public GameObject _leftMirrorImg;

    [SerializeField] public GameObject _leftEnlargerImg;

    [SerializeField] public GameObject _rightMirrorImg;

    [SerializeField] public GameObject _rightEnlargerImg;

    private SpawnManager _spawnManager;


    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null!");
        }
        
    }
    void Update()
    {
        if (!PauseMenu._isPaused)
        {
            CalculateMovement();
            //MirrorPowerUpActive(true);
        }

    }
    private void CalculateMovement()
    {
        float verticalInput;

        if (_isPaddleLeft == true)
        {
            verticalInput = Input.GetAxisRaw("VerticalLeft");
        }
        else
        {
            verticalInput = Input.GetAxisRaw("VerticalRight");
        }

        Vector3 _paddlePosition = transform.position;
        if (_isPaddleEnlargerPowerUpActive == false || _isPaddleEnlargerPowerUpUsed == false)
        {
            _paddlePosition.y = Mathf.Clamp(_paddlePosition.y + verticalInput * _speed * Time.deltaTime, -VerticalBound, VerticalBound);
            transform.position = _paddlePosition;
        }

        if (_isPaddleEnlargerPowerUpActive == true && _isPaddleEnlargerPowerUpUsed == true)
        {
            _paddlePosition.y = Mathf.Clamp(_paddlePosition.y + verticalInput * _speed * Time.deltaTime, -VerticalBoundForEnlarger, VerticalBoundForEnlarger);
            transform.position = _paddlePosition;
        }

    }

    public void MirrorPowerUpActive(bool _hitByLeft)
        {
        
            if (_hitByLeft)
            {
                _leftMirrorImg.SetActive(true);
            } 
            else if (!_hitByLeft)
            {
                _rightMirrorImg.SetActive(true);   
            }
        
            Rigidbody _ballRb = GameObject.Find("Ball").GetComponent<Rigidbody>();

            if (_ballRb == null)
            {
                Debug.LogError("RigidBody of ball is null");
                return;
            }
            _spawnManager.StopSpawning();
            _isMirrorPowerUpActive = true;
            StartCoroutine(MirrorPowerUpCoroutine(_ballRb, _hitByLeft));
        }
    private IEnumerator MirrorPowerUpCoroutine(Rigidbody ballRb, bool hitByLeft)
        {
            float _CastTimeLimit = Time.time + 6;
            while (_isMirrorPowerUpActive && Time.time < _CastTimeLimit)
            {
                if (!PauseMenu._isPaused)
                {

                    if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && hitByLeft && _isPaddleLeft)
                    {

                        ballRb.velocity = new Vector3(ballRb.velocity.x, -ballRb.velocity.y, 0);
                        _isMirrorPowerUpActive = false;
                        print("Se desactivo powerup IZQUIERDA");
                    }

                    if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !hitByLeft && !_isPaddleLeft)
                    {

                        ballRb.velocity = new Vector3(ballRb.velocity.x, -ballRb.velocity.y, 0);
                        _isMirrorPowerUpActive = false;
                        print("Se desactivo powerup DERECHA");
                    }
                }
                

                yield return null; // Esperar hasta la siguiente actualización del frame
            }
        _rightMirrorImg.SetActive(false);
        _leftMirrorImg.SetActive(false);

        _isMirrorPowerUpActive = false;
        print("Se desactivo powerup");
        _spawnManager.StartSpawning();
    }


    public void PaddleEnlargerPowerUpActive(bool _hitByLeft)
    {
        //_isPaddleEnlargerPowerUpUsed = false;
        if (_hitByLeft)
        {
            _leftEnlargerImg.SetActive(true);
        }
        else if (!_hitByLeft)
        {
            _rightEnlargerImg.SetActive(true);
        }
        _spawnManager.StopSpawning();
        _isPaddleEnlargerPowerUpActive = true;
        StartCoroutine(PaddleEnlargerPowerUpCoroutine(_hitByLeft));
    }

    private IEnumerator PaddleEnlargerPowerUpCoroutine(bool _hitByLeft)
    {
        
        float _CastTimeLimit = Time.time + 10;
        Vector3 _PaddleScale = transform.localScale;
        while (_isPaddleEnlargerPowerUpActive && Time.time < _CastTimeLimit)
        {
            if (!PauseMenu._isPaused)
            {

                if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && _hitByLeft && _isPaddleLeft)
                {
                    _isPaddleEnlargerPowerUpUsed = true;
                    _PaddleScale = new Vector3(_PaddleScale.x, 2, _PaddleScale.z);
                    transform.localScale = _PaddleScale;
                    //print("Se desactivo powerup IZQUIERDA");
                }


                if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) && !_hitByLeft && !_isPaddleLeft)
                {
                    _isPaddleEnlargerPowerUpUsed = true;
                    _PaddleScale = new Vector3(_PaddleScale.x, 2, _PaddleScale.z);
                    transform.localScale = _PaddleScale;
                    //print("Se desactivo powerup DERECHA");
                }
            }
            yield return null; // Esperar hasta la siguiente actualización del frame
        }

        _rightEnlargerImg.SetActive(false);
        _leftEnlargerImg.SetActive(false);

        _spawnManager.StartSpawning();
        _PaddleScale = new Vector3(_PaddleScale.x, 1, _PaddleScale.z);
        transform.localScale = _PaddleScale;
        _isPaddleEnlargerPowerUpUsed = false;
        _isPaddleEnlargerPowerUpActive = false;
        print("Se desactivo powerup");
    }
}


