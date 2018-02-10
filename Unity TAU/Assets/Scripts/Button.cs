﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour {

    public string name = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseDown()
    {
        //Application.LoadLevel("Principal"); -> Obsoleto desde la V_5.3
        NotificationCenter.DefaultCenter().PostNotification(this, "Destroy");
        SceneManager.LoadScene(name);
    }
}
