using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPointsHelper : MonoBehaviour {

    public GameObject player;

    private Vector3 moveVector;
    private Vector3 offset;
    private Transform lookAt;

    // Use this for initialization
    void Start () {
        lookAt = player.transform;
        offset = this.gameObject.transform.position - lookAt.position;
    }
	
	// Update is called once per frame
	void Update () {
        moveVector = lookAt.position + offset;
        moveVector.x = 0;
        moveVector.y = 0;
        this.transform.position = moveVector;
    }

}
