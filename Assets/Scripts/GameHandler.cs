using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameOverScreen GameOverScreen;
    public void GameOver()
    {
        Time.timeScale = 0f;
        GameOverScreen.Setup(Score.currentScore);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (TetrisBlock.gameOver == true)
        {
            GameOver();
        }
    }
}
