using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerRunner : MonoBehaviour {

    public GameObject player;

    private Transform lookAt;
    private Vector3 offset;

    private Vector3 moveVector;
    private float animTransition = 0.0f;
    public float animDuration = 2.0f;
    private Vector3 animOffset = new Vector3(0,5,5);

	// Use this for initialization
	void Start () {
        lookAt = player.transform;
        offset = this.gameObject.transform.position - lookAt.position;

    }
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + offset;

        moveVector.x = 0;        
        moveVector.y = Mathf.Clamp(moveVector.y, 3,5);

        if (animTransition > 1.0f)
        {
            this.transform.position = new Vector3(moveVector.x, 3, moveVector.z); //moveVector
        }
        else
        {
            //anim at the start of the game
            this.transform.position = Vector3.Lerp(moveVector + animOffset, moveVector, animTransition);
            animTransition += Time.deltaTime * 1 / animDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }


       

    }
}
