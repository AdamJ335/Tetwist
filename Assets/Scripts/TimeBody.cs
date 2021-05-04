using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
    public static int strikes = 3;
    public int rewindTime;
    private bool canRewind = true;
    private bool isRewinding = false;

    List<Vector2> positions;
    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (strikes == 0)
        {
            canRewind = false;
        }
        if (canRewind)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartRewind();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                StopRewind();
            }
        }


    }
    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }
    void Record()
    {
        positions.Insert(0, transform.position);
    }
    void Rewind()
    {
        if (positions.Count > 0)
        {
            transform.position = positions[0];
            positions.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }
    void StartRewind()
    {

        isRewinding = true;
        StartCoroutine(RewindTimer());
    }
    void StopRewind()
    {
        isRewinding = false;
    }
    IEnumerator RewindTimer()
    {
        yield return new WaitForSeconds(rewindTime);
        isRewinding = false;
        strikes--;
        yield return new WaitForSeconds(rewindTime);

    }
}
