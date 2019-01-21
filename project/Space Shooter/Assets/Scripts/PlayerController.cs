using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private AudioSource fireSound;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject[] shot;
    
    public Transform shotSpawn;

    public float fireRate;
    private float nextFire;

    public int CurrentWeapone;
    private int score;

    void Start()
    {
        CurrentWeapone = 0;
        rb = GetComponent<Rigidbody>();
        fireSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot[CurrentWeapone], shotSpawn.GetComponent<Transform>().position, shotSpawn.GetComponent<Transform>().rotation);
            fireSound.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3( moveHorizontal, 0.0f, moveVertical);
        //rigidbody.velocity = movement; 
        rb.velocity = movement * speed;


        rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x , boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    public void plusFirerate(float firerateplus)
    {
        fireRate += firerateplus;
    }
}
