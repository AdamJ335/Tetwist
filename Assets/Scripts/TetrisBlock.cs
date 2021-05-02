using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;

    public float fallTime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public static bool gameOver;
    private static Transform[,] tetrisGrid = new Transform[width, height];


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.isPaused)// Stops inputs when game is paused
        {
            //Makes Tetrominos move
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(-1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(1, 0, 0);
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
                if (!ValidMove())
                {
                    transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
                }
            }

            //makes tetromino fall faster, also checks if it cant go any further down therefore its locked into place and another is spawned
            if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime))
            {
                transform.position += new Vector3(0, -1, 0);
                if (!ValidMove())
                {
                    transform.position -= new Vector3(0, -1, 0);
                    if (!ValidMove())
                    {
                        gameOver = true;
                    }
                    else
                    {
                        AddToGrid();
                        CheckForLines();
                        this.enabled = false;
                        FindObjectOfType<SpawnTetromino>().NewTetromino();
                    }

                }
                previousTime = Time.time;

            }
        }
    }


    //checks to see if theres a complete line then removes the line and sets all the items lower
    void CheckForLines()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (tetrisGrid[j, i] == null)
            {
                return false;
            }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Score.currentScore++;
            Destroy(tetrisGrid[j, i].gameObject);
            tetrisGrid[j, i] = null;
        }

    }
    //moves all rows above downward to make sure that theres no gaps
    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {

                if (tetrisGrid[j, y] != null)
                {
                    tetrisGrid[j, y - 1] = tetrisGrid[j, y];
                    tetrisGrid[j, y] = null;
                    tetrisGrid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            tetrisGrid[roundedX, roundedY] = children;
        }

    }

    bool ValidMove()
    {
        foreach (Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
            {
                return false;
            }
            if (tetrisGrid[roundedX, roundedY] != null)
            {
                return false;
            }
        }

        return true;
    }

}
