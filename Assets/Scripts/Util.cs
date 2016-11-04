using UnityEngine;
using System.Collections;

public static class Util {

	public static float ConstrainAngle(float angle)
	{
		while (angle > 180) {
			angle -= 360;
		}
		while (angle < -180) {
			angle += 360;
		}
		return angle;
	}
}
