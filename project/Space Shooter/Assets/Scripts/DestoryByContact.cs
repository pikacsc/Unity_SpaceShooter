using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryByContact : MonoBehaviour {

    private Transform trans;

    private int score;
    public int scoreValue;

    public GameObject explosion;
    public GameObject playerExplosion;
    public GameObject[] items;
    private GameController gameController;
    private PlayerController player;
    
    void Start() 
    {
        trans = GetComponent<Transform>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if(gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Boundary") || other.CompareTag ("Enemy"))
        {
            return;
        }
        if (other.CompareTag("Item"))
        {
            return;
        }
        if (explosion != null)
        {
            Instantiate(explosion, trans.position, trans.rotation);
        }

        if (other.tag == "Player") { 
            Instantiate(playerExplosion, other.GetComponent<Transform>().position, other.GetComponent<Transform>().rotation);
            gameController.GameOver();
        }
        int ran = Random.Range(0, items.Length);
        if(ran <= items.Length-1)
        {
            Instantiate(items[ran], trans.position, trans.rotation);
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        gameController.AddScore(scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
