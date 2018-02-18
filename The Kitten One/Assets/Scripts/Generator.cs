using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject[] objects;
    public float minTime = 1f;
    public float maxTime = 2f;
    public int height = 0;
    
    // Use this for initialization
	void Start () {
        //Generate();
        NotificationCenter.DefaultCenter().AddObserver(this, "Begin");
        NotificationCenter.DefaultCenter().AddObserver(this, "Dead");
    }

    void Begin(Notification notif)
    {
        Generate();
    }

    
    void Dead(Notification notif)
    {

    }
    

    // Update is called once per frame
    void Update() {
        //if (GetComponent<Rigidbody2D>().position == new Vector2(x1, y) || GetComponent<Rigidbody2D>().position == new Vector2(x2, y))
        //{
        //    speed = -speed;
        //    GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, speed);
        //}
    }

    void Generate()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + Random.Range(0, height), 0);
        Instantiate(objects[Random.Range(0,objects.Length)], pos, Quaternion.identity);
        //Range = Valores ENTEROS, incluyendo el mínimo, pero no incluyendo el máximo
        Invoke("Generate", Random.Range(minTime, maxTime));
    }
}
