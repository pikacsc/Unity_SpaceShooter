using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBling : MonoBehaviour {

    public GameObject sphereUL;

    public float sphereULSpeed = 0.1f;

    private float sphereULDirX = 0.0f;
    private float sphereULDirY = 0.0f;

	// Update is called once per frame
	void Update () {

        sphereULDirX += Time.deltaTime * sphereULSpeed;
        sphereULDirY += Time.deltaTime * sphereULSpeed;
        sphereUL.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(sphereULDirX, sphereULDirY);
	}
}
