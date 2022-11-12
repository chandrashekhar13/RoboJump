using UnityEngine;
using System.Collections;

public class Saw : MonoBehaviour 
{

	float sawSpeed = 150;

	void Update ()
	{
		transform.Rotate (0, 0, sawSpeed * Time.deltaTime);
	}
}
