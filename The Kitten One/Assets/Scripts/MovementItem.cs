using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementItem : MonoBehaviour
{

    public float speed = 1f;
    private bool begun = false;

    // Use this for initialization
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "Begin");
        NotificationCenter.DefaultCenter().AddObserver(this, "Dead");
    }

    void Begin(Notification notif)
    {
        begun = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
    }


    void Dead(Notification notif)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    }
    

    // Update is called once per frame
    void Update()
    {
        if (begun)
            GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
