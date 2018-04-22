using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed = 3.0f;
    public KeyCode leftControll;
    public KeyCode rightControll;
    public KeyCode jumpControll;

    public GameObject CameraObject;

    private Vector3 moveVector;
    private CharacterController controller;

    private float verticalVelocity = 0.0f;
    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;

    // Use this for initialization
    void Start () {
        controller = this.gameObject.GetComponent<CharacterController>();     
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time < CameraObject.GetComponent<CameraControllerRunner>().animDuration)
        {
            controller.Move(Vector3.forward* speed * Time.deltaTime); // don`t move while the animation is running
            return;
        }
        Move();

    }

    private void Move()
    {
        moveVector = Vector3.zero; // reset movement Vector

        if (controller.isGrounded)
        {                     
            if (Input.GetKeyDown(jumpControll))
            {
                this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 5, 0);
            }

        }
       

        //calc X, Y, Z values
        moveVector.x = Input.GetAxisRaw("Horizontal") * speed;
        //moveVector.y = moveVector.y; // ovde treba skok da preradim
        moveVector.z = speed;


        controller.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed (float speedModifier)
    {
        speed = speed + speedModifier;
    }

}
