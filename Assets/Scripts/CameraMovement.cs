﻿using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{

    public float dampTime = 0.15f;
	public float shake_strength = 0.1f;
	public float shake_time = 0.2f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

	private bool shaking = false;
	private float shake_time_left;

    public Color morning_color, day_color, evening_color, night_color;

	void Start()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

		if (shaking) {
			float sx = Random.value*shake_strength - shake_strength/2;
			float sy = Random.value*shake_strength - shake_strength/2;
			transform.position += new Vector3 (sx, sy, 0);
			shake_time_left -= Time.deltaTime;
			if (shake_time_left < 0) {
				shake_time_left = shake_time;
				shaking = false;
			}
		}

        switch (GameManagerScript.moment)
        {
            case GameManagerScript.MORNING: GetComponent<Camera>().backgroundColor = morning_color; break;
            case GameManagerScript.DAY: GetComponent<Camera>().backgroundColor = day_color; break;
            case GameManagerScript.EVENING: GetComponent<Camera>().backgroundColor = evening_color; break;
            case GameManagerScript.NIGHT: GetComponent<Camera>().backgroundColor = night_color; break;
        }
    }

	public void Shake()
	{
		shaking = true;
		shake_time_left = shake_time;
	}
}