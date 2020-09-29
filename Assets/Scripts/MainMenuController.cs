using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] Text _highScoreTextView = null;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        int highScore = PlayerPrefs.GetInt("HighScore");
        _highScoreTextView.text = highScore.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
