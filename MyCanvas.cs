using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MyCanvas : MonoBehaviour
{

    public TMP_Text ScoreText
    {
        get
        {
            return _scoreText;
        }
    }

    public TMP_Text GameOverText
    {
        get
        {
            return _gameOverText;
        }
    }

    public TMP_Text RunTimeText
    {
        get
        {
            return _runTimeText;
        }
    }

    public TMP_Text GameWinText
    {
        get
        {
            return _gameWinText;
        }
    }

    public TMP_Text RunTimeResult
    {
        get
        {
            return _runTimeResultText;
        }
    }

    public Button RestartButton
    {
        get
        {
            return _restartButton;
        }
    }

    [SerializeField]
    private TMP_Text _scoreText;

    [SerializeField]
    private TMP_Text _runTimeText;

    [SerializeField]
    private Button _restartButton;

    [SerializeField]
    private TMP_Text _gameOverText;

    [SerializeField]
    private TMP_Text _gameWinText;

    [SerializeField]
    private TMP_Text _runTimeResultText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
