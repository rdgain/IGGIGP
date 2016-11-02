using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	//-- set start time 00:00
	public int second = 0;
	public bool realTime;
	
	public GameObject pointerSeconds;
    
    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

    //-- internal vars
    int seconds;
    float msecs;

void Start() 
{
    msecs = 0.0f;
    seconds = 0;
	//-- set real time
	if (realTime)
	{
		second=System.DateTime.Now.Second;
	}

}

void Update() 
{
    //-- calculate time
    msecs += Time.deltaTime * clockSpeed;
    float numDivisions = GameManagerScript.numMoments * GameManagerScript.lengthOfMoment;
    if (msecs >= 1.0f)
    {
        msecs -= 1.0f;
        seconds++;
        if(seconds >= numDivisions)
        {
            seconds = 0;
        }
    }


    //-- calculate pointer angles
    float rotationSeconds = - (360.0f / numDivisions)  * seconds;

    //-- draw pointers
    pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);

}
}
