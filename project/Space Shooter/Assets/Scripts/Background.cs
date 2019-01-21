using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public GameObject bgUL;

    public float bgULSpeed = 0.1f;

    private float bgULDirY = 0.0f;

	
	// Update is called once per frame
	void Update () {
        bgULDirY += Time.deltaTime * bgULSpeed;

        bgUL.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, bgULDirY);
	}
}
