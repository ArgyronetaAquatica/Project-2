using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{

    [SerializeField] Text _currentScoreTextView = null;
    [SerializeField] GameObject _popupMenu = null;

    int _currentScore;
    bool _menuToggle = false;

    private void Start()
    {
        _popupMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IncreaseScore(5);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _menuToggle = !_menuToggle;
        }
        if (_menuToggle)
        {
            EnablePopupMenu();
        }
        else
        {
            ResumeLevel();
        }
    }

    public void ExitLevel()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            UnityEngine.Debug.Log("New high score: " + _currentScore);
        }
        SceneManager.LoadScene("MainMenu");
    }

    public void ResumeLevel()
    {
        _menuToggle = false;
        _popupMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void EnablePopupMenu()
    {
        _menuToggle = true;
        _popupMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

}
