using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public static int currentScore = 0;

    void Start()
    {
        highScoreText.SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
    }

    public void Update()
    {
        scoreText.text = currentScore.ToString();
        //Debug.Log(currentScore);

        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.SetText(currentScore.ToString());
        }


    }
}