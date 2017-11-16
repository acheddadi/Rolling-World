using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 30.0f;
    public float multiplier = 1.0f;
	private void Start()
	{
		
	}
	
	private void Update()
	{
        transform.Rotate(0, -speed * multiplier * Time.deltaTime, 0);
	}
}
