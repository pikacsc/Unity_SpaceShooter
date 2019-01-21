using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour {

    public float multiplier = 0.1f;
    public float effectDuration = 5f;

    public GameObject pickupEffect;
    public string itemName;
    private GameObject gameControllerObject;
    private GameController gameController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
        if (other.CompareTag("Boundary") || other.CompareTag("Enemy"))
        {
            return;
        }
    }

    IEnumerator Pickup(Collider player)
    {
        gameControllerObject = GameObject.FindWithTag("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();


        //Spawn powerup effect
        Instantiate(pickupEffect, transform.position, transform.rotation);

        //Apply effect to the player
        PlayerController currentPlayerOptions = player.GetComponent<PlayerController>();
        float originalFireRate = currentPlayerOptions.fireRate;
        currentPlayerOptions.fireRate -= multiplier;

        gameController.itemText.text += itemName;

        //make item invisible
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
       

        //make it permanent effect to temporary effect
        //Wait x amout of seconds
        yield return new WaitForSeconds(effectDuration);
        //Reverse the effect on our player

        currentPlayerOptions.fireRate = originalFireRate;
        gameController.itemText.text = "Item : ";
        Destroy(gameObject);
    }
}
