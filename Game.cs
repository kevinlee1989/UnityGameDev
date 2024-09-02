using Udacity.GameDevelopment.Shared; //KEEP
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Udacity.GameDevelopment.PlatformerGame.Game_Begin
{
    /// <summary>
    /// This is the main entry point to the game.
    /// </summary>
    public class Game : MonoBehaviour
    {

        //  Fields ----------------------------------------
        [SerializeField]
        private MyCanvas _myCanvas;

        //  Properties ------------------------------------
        private int _score = 0;
        
        //  Unity Methods ---------------------------------
        private void Awake()
        {
            Debug.Log("This Scene and script are purposefully simple. This is a starting point for your game development");

            _myCanvas.GameOverText.enabled = false;
            _myCanvas.GameWinText.enabled = false;
            _myCanvas.RunTimeResult.enabled = false;
        }
        
        
        private void Start()
        {
            _myCanvas.RestartButton.onClick.AddListener(RestartButton_OnClicked);

            

        }

        private void RestartButton_OnClicked()
        {
            _score = 0;

            RefreshUI();
        }

        //  Methods ---------------------------------------
        private void RefreshUI()
        {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        //  Event Handlers --------------------------------
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.name.ToLower().Contains("pickup"))
            {
                Debug.Log("pickup!");
                _score++;

                _myCanvas.ScoreText.text = $"Score : {_score:000}";

               
            }
        }

        private void Update()
        {
            if (_myCanvas.RunTimeText != null)
            {
                 float elapsedTime = Time.time; // How much time spent in the game
                _myCanvas.RunTimeText.text = "Time: " + Mathf.FloorToInt(elapsedTime).ToString() + "s";

                _myCanvas.RunTimeResult.text = $"You took time : {elapsedTime} ";
            }
        }
        
    }
}
