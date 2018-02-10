using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour {

    public GameObject image;
    //public GameObject tryagain, mainmenu;

    // Use this for initialization
    void Start () {
        NotificationCenter.DefaultCenter().AddObserver(this, "Dead");
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("DeathScene");
        image.SetActive(true);
    }
	
    void Dead (Notification notif)
    {
        StartCoroutine(MyCoroutine());
        //tryagain.SetActive(true);
        //mainmenu.SetActive(true);
    }

	// Update is called once per frame
	void Update () {
		 
	}
}
