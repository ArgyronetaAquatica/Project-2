using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{

    [SerializeField] Text _currentScoreTextView = null;
    [SerializeField] GameObject _popupMenu = null;
    [SerializeField] PlayerController _player = null;
    [SerializeField] Text _healthText = null;
    [SerializeField] Slider _healthSlider = null;
    [SerializeField] Slider _timerSlider = null;
    [SerializeField] GameObject killMenu = null;
    [SerializeField] GameObject gameOverMenu = null;
    [SerializeField] Text currentScoreText = null;
    [SerializeField] Text highScoreText = null;
    [SerializeField] float timeLimit = 30f;
    [SerializeField] AudioClip deathClip = null;
    [SerializeField] AudioClip timeOutClip = null;

    int _currentScore;
    int _currentHealth;
    public bool menuToggle = false;
    public bool gameOver = false;
    float timer = 0;

    bool clipPlayed = false;

    private void Start()
    {
        _popupMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _currentHealth = _player.health;
        _healthText.text = _currentHealth.ToString() + "%";
        _healthSlider.value = _currentHealth;
    }

    private void Update()
    {
        
        if (!menuToggle)
        {
            timer += Time.deltaTime;
            //update timer slider
            float timePercentage = timer / timeLimit * 100;
            _timerSlider.value = 100 - timePercentage;
        }

        //if enemy killed
        //increase score
        if (Input.GetKeyDown(KeyCode.Escape) && _player.alive && !gameOver)
        {
            menuToggle = !menuToggle;
        }
        if (menuToggle)
        {
            EnablePopupMenu();
        }
        else
        {
            ResumeLevel();
        }

        //update health bar
        if (_currentHealth != _player.health)
        {
            _currentHealth = _player.health;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
            _healthText.text = _currentHealth.ToString() + "%";
            _healthSlider.value = _currentHealth;
        }

        //kill check
        if (_currentHealth <= 0)
        {
            _player.alive = false;
            _player.health = 0;
            PlayerDeath();
        }

        //timer check
        if (timer >= timeLimit)
        {
            GameOver();
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
        menuToggle = false;
        Time.timeScale = 1;//unpause
        _popupMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartLevel()
    {
        //reload level
        SceneManager.LoadScene("Level01");
    }

    private void EnablePopupMenu()
    {
        menuToggle = true;
        Time.timeScale = 0;//pause
        _popupMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void PlayerDeath()
    {
        killMenu.SetActive(true);
        if (!clipPlayed)
        {
            AudioHelper.PlayClip2D(deathClip, 1f);
            clipPlayed = true;
        }
        Time.timeScale = 0;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void GameOver()
    {
        gameOverMenu.SetActive(true);
        if (!clipPlayed)
        {
            AudioHelper.PlayClip2D(timeOutClip, 1f);
            clipPlayed = true;
        }
        Time.timeScale = 0;
        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        int highScore = PlayerPrefs.GetInt("HighScore");
        if (_currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", _currentScore);
            UnityEngine.Debug.Log("New high score: " + _currentScore);
        }
        currentScoreText.text = "Score: " + _currentScore.ToString();
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    public void IncreaseScore(int scoreIncrease)
    {
        _currentScore += scoreIncrease;
        _currentScoreTextView.text = "Score: " + _currentScore.ToString();
    }

}
