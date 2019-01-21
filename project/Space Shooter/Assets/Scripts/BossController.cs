using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    public int bossHP;
    private Transform trans;


    public GameObject bossExplosion;
    private GameController gameController;
    private PlayerController player;

    void Start()
    {
        trans = GetComponent<Transform>();
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
        if (bossExplosion != null) //hitted
        {
            Instantiate(bossExplosion, trans.position, trans.rotation);
        }
        bossHP -= 100;
        if(bossHP <= 0)  //finished
        {
            Instantiate(bossExplosion, transform.position, transform.rotation);
            gameController.clearChapter = true;
            gameController.eventText.text = "Congratulation!";
            Destroy(gameObject);
        }
    }

}
