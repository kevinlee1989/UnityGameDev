using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Udacity.GameDevelopment.Shared;
using System;
using UnityEngine.SceneManagement;
using Cinemachine;

//Polishing library
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rigidbody2D = null;

    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private AudioSource _audioSource1;

    [SerializeField]
    private AudioSource _audioSource2;

    [SerializeField]
    private AudioClip _audioClip1;

    [SerializeField]
    private AudioClip _audioClip2;

    [SerializeField]
    private AudioClip _audioClip3;

    [SerializeField]
    private MyCanvas _myCanvas;

    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;


    private bool _isGameOver = false;
    private bool _isFlipX = false;
    private bool _isRunning = false;
    private bool _hasJumped = false;
    public int _score = 0;
    public float pauseDuration = 3f;

    public float flipThreshold = 90f; // Thresholder angle for game over

    



    // Start is called before the first frame update
    void Start()
    {
        _audioSource2.clip = _audioClip3;
        _audioSource2.Play();
        Debug.Log("hello");

        //AudioManager.Instance.PlayAudioClip("Rising01", 0.7f);

        _rigidbody2D.simulated = false;
        _spriteRenderer.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
        _spriteRenderer.transform.DOScale(new Vector3(1, 1, 1), .5f).SetDelay(1).onComplete += () =>
        {
            _rigidbody2D.simulated = true;
        };

    }

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal"); // When horizontal key inserted, value 1(right), -1(left) 
        float verticalMovement = Input.GetAxis("Vertical");

        float zRotation = transform.eulerAngles.z; // Get the current rotation of direction of y

        //if any key is not inserted 
        if (horizontalMovement != 1 && horizontalMovement != -1)
        {
            _isRunning = false;
            _animator.SetBool("IsRunning", _isRunning);
        }

        if (_isGameOver)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isFlipX = !_isFlipX;
            _spriteRenderer.flipX = _isFlipX;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //_isFlipX = false; // boolean to flip the Rhino
            //_spriteRenderer.flipX = _isFlipX; // Actual statement to flip the Rhino

            // activate Rhino Running if the key is dectected
            _isRunning = true;
            _animator.SetBool("IsRunning", _isRunning);

            
            
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //_isFlipX = true; // boolean to flip the Rhino
            //_spriteRenderer.flipX = _isFlipX; // Actual statement to flip the Rhino

            // activate Rhino Running if the key is dectected
            _isRunning = true;
            _animator.SetBool("IsRunning", _isRunning);
        }

        // if user input upArrow key to jump.
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            _isRunning = true;
            _animator.SetBool("IsRunning", _isRunning);

            
            
        }
        if (_isFlipX == true)
        {
            float movement = Mathf.Max(horizontalMovement * _speed * Time.deltaTime, 0);

            transform.position += new Vector3(movement, 0, 0);
        }

        if (_isFlipX == false)
        {
            float movement = Mathf.Min(horizontalMovement * _speed * Time.deltaTime, 0);

            transform.position += new Vector3(movement, 0, 0);
        }
        // moving the player
        //transform.position += new Vector3
            //(
                //horizontalMovement * _speed * Time.deltaTime, // moving left and right with constant speed
                //0,
                //0
            //);

        if (zRotation > 180) zRotation -= 360;

        // Game over Conditon1
        if (Mathf.Abs(zRotation) > flipThreshold)
        {
            // showing the GameOverText
            _myCanvas.GameOverText.enabled = true;

            //elapsedTime = 0f;

            StartCoroutine(PauseGameCoroutine());

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Resume the game
            Time.timeScale = 1f;
        }

        // Winning a game
        if(_score >= 7)
        {

            _myCanvas.RunTimeResult.enabled = true;    

            _myCanvas.GameWinText.enabled = true;

            Time.timeScale = 0f;

            
            //_myCanvas.RunTimeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString() + "s";
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name.ToLower().Contains("platform"))
        {
            _audioSource1.clip = _audioClip1;
            _audioSource1.Play();

            _hasJumped = false;

            Debug.Log("landed on the platform");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.ToLower().Contains("pickup"))
        {
            Destroy(other.gameObject);

            _score++;

            _myCanvas.ScoreText.text = $"Score : {_score:000}";

            _spriteRenderer.DOColor(Color.red, 0.2f);
            _spriteRenderer.DOColor(Color.white, 0.2f).SetDelay(0.1f);

            _spriteRenderer.transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), .3f);

            // Shaking the camera
            _virtualCamera.transform.DOShakePosition(0.2f, new Vector3(3.2f, 3.2f, 3.2f));
        }

        // Game over Conditon2
        if (other.gameObject.name.ToLower().Contains("gameoverzone"))
        {
            _myCanvas.GameOverText.enabled = true;

            //elapsedTime = 0f;

            StartCoroutine(PauseGameCoroutine());

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Resume the game
            Time.timeScale = 1f;
        }

    }

    private void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))  && !_hasJumped)
        {
            Jump();
            _hasJumped = true;

            _audioSource1.clip = _audioClip2;
            _audioSource1.Play();

            Debug.Log("Jump!!");
        }
    }

    //Jump function
    private void Jump()
    {
        _rigidbody2D.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
    }


    // Puasing a game
    private IEnumerator PauseGameCoroutine()
    {
        // Pause the game
        Time.timeScale = 0f;

        // Wait for the specified duration while the game is paused
        yield return new WaitForSecondsRealtime(pauseDuration);

        
    }

    

}
