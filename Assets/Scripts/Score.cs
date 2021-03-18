using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;

    void Start()
    {
        highScore.SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
    }

    public void UpdateScore()
    {
        int scoreNumber = Random.Range(1, 7);
        score.text = scoreNumber.ToString();

        if (scoreNumber > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", scoreNumber);
            highScore.SetText(scoreNumber.ToString());
        }


    }
}
