using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControl : MonoBehaviour
{
	private void Start()
	{
        Cursor.visible = false;
    }
	
	private void Update()
	{
        if (Input.GetKey("escape"))
            Application.Quit();
    }
}
