using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour {

    public GameObject character;
    public Points statusPoints;
    public int points = 0;
    public TextMesh mark;
    public string filePath;

    private bool dead = false, destroyed = false;

    void Awake()
    {
        if (statusPoints == null)
        {
            Debug.Log("No existe. Lo creo.");
            statusPoints = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (statusPoints != this)
        {
            Debug.Log("Existe. Destruido.");
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        mark.text = points.ToString();
        NotificationCenter.DefaultCenter().AddObserver(this, "Dead");
        NotificationCenter.DefaultCenter().AddObserver(this, "Destroy");
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene("DeathScene");
        this.transform.position = new Vector3(-2, 1, 0);
        //image.SetActive(true);
    }

    void Dead(Notification notif)
    {
        if (!dead)
        {
            mark.text = "Final points: " + points.ToString();
            //Fichero(points.ToString());
            StartCoroutine(MyCoroutine());
        }
        dead = true;
    }

    void Destroy(Notification notif)
    {
        mark.text = "";
    }

    // Update is called once per frame
    void Update () {
        if (!dead)
        {
            points = (character.GetComponent<CharacterController>().points) / 10;
            mark.text = points.ToString();
        }
    }

    //private void FixedUpdate()
    //{
    //    points = (character.GetComponent<CharacterController>().points)/10;
    //    mark.text = points.ToString();
    //}

    //private void Fichero(string puntos)
    //{
    //    string aux = File.ReadAllText(filePath);
    //    File.WriteAllText(filePath,aux + " \nPROBANDO CON: " + puntos);
    //    Debug.Log("Apuntados " + puntos + " puntos.");
    //}
}
