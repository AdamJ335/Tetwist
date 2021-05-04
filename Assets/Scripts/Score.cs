using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI strikesText;
    string strikeString;
    public static int currentScore = 0;


    void Start()
    {
        highScoreText.SetText(PlayerPrefs.GetInt("HighScore", 0).ToString());
    }

    public void Update()
    {
        switch (TimeBody.strikes)
        {
            default: strikeString = ""; break;
            case 1: strikeString = "I"; break;
            case 2: strikeString = "II"; break;
            case 3: strikeString = "III"; break;

        }
        scoreText.text = currentScore.ToString();
        strikesText.text = strikeString;
        //Debug.Log(currentScore);

        if (currentScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScoreText.SetText(currentScore.ToString());
        }


    }
}