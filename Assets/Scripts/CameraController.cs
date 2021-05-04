using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed;
    Transform currentView;
    public static int checkScore = 0;
    private bool isTwisted = false;
    // Start is called before the first frame update
    void Start()
    {
        currentView = views[0];
    }
    void Update()
    {
        if (checkScore >= 100 && !isTwisted)
        {
            FindObjectOfType<AudioManager>().Play("gameTwist");
            currentView = views[1];
            checkScore = 0;
            isTwisted = true;
        }
        if (checkScore >= 50 && isTwisted)
        {
            FindObjectOfType<AudioManager>().Play("gameTwist");
            currentView = views[0];
            checkScore = 0;
            isTwisted = false;
        }
    }

    void LateUpdate()
    {
        //Lerp positioning(linear inerpolation)
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);

        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentView.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentView.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentView.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
        );
        transform.eulerAngles = currentAngle;
    }
}
