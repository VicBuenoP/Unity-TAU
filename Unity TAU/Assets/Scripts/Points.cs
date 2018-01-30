using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour {

    public GameObject character;
    public int points = 0;
    public TextMesh mark;

	// Use this for initialization
	void Start () {
        mark.text = "Puntuación: " + points.ToString();
    }
	
	// Update is called once per frame
	void Update () {
        mark.text = "Puntuación: " + points.ToString();
    }

    private void FixedUpdate()
    {
        points = (character.GetComponent<CharacterController>().points)/10;
        mark.text = "Puntuación: " + points.ToString();
    }
}
