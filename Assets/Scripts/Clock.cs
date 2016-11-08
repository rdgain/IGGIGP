using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {

	//-- set start time 00:00
	public int second = 0;
	public bool realTime;
	
	public GameObject pointerSeconds;
    
    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster

void Start() 
{
	//-- set real time
	if (realTime)
	{
		second=System.DateTime.Now.Second;
	}

}

void Update() 
{
    float numDivisions = GameManagerScript.numMoments * GameManagerScript.lengthOfMoment;

    //-- calculate pointer angles
	float rotationSeconds = - (360.0f / numDivisions)  * GameManagerScript.time;

    //-- draw pointers
    pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);

}
}
