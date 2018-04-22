using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerV2 : MonoBehaviour
{
    public float speed = 3.0f;
    public float jumpSpeed = 3.0f;

    public KeyCode leftControll;
    public KeyCode rightControll;
    public KeyCode jumpControll;

    public GameObject CameraObject;
    public GameObject TargetObject;   

    private GameObject otherGameObject;
    private bool isDead;
   
    // Use this for initialization
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < CameraObject.GetComponent<CameraControllerRunner>().animDuration)
        {
            //controller.Move(Vector3.forward * speed * Time.deltaTime); // don`t move while the animation is running
            return;
        }
        Move();
    }

    private void Move()
    {
        if (isDead || !otherGameObject)
        {
            return;
        }

        float step = speed * Time.deltaTime;
        Vector3 ForwardTarget = new Vector3(otherGameObject.transform.position.x,
            this.gameObject.transform.position.y, TargetObject.transform.position.z);

        if (Input.GetKeyDown(leftControll) || (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2) && (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.width / 2))
        {
                       

            if (otherGameObject.tag.Equals("middleLane") || otherGameObject.tag.Equals("rightLane"))
            {
                 this.transform.position = new Vector3(this.transform.position.x - 1.8f,
                     this.transform.position.y,
                     this.transform.position.z);

                /*this.gameObject.GetComponent<Rigidbody>().AddForce(
                    new Vector3(-70, 0, 0), ForceMode.Impulse);*/

              /*  ForwardTarget.x = ForwardTarget.x - 5.8f;

                StartCoroutine(MoveOverSeconds(this.gameObject,
                    new Vector3(this.transform.position.x - 1.8f,
                     this.transform.position.y,
                     this.transform.position.z), 1f));*/

            }
        }

        if (Input.GetKeyDown(rightControll) || (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2) && (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.width / 2))
        {
            if (otherGameObject.tag.Equals("middleLane") || otherGameObject.tag.Equals("leftLane"))
            {
                this.transform.position = new Vector3(this.transform.position.x + 1.8f, this.transform.position.y,
                     this.transform.position.z);
            }
        }
               
        //if grounded
        if (Input.GetKeyDown(jumpControll) && this.gameObject.transform.position.y < 0.6f || (Input.GetMouseButtonDown(0) && Input.mousePosition.y > Screen.height / 2) && this.gameObject.transform.position.y < 0.6f)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,5,0);
        }

        

        this.gameObject.transform.position = Vector3.MoveTowards(transform.position,
            ForwardTarget, step);
    }

    public void SetSpeed(float speedModifier)
    {
        speed = speed + speedModifier;
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (other.gameObject.tag.EndsWith("Lane"))
        {
            otherGameObject = other.gameObject;
        }

        if (other.gameObject.tag.Equals("obstacle"))
        {
            isDead = true;           
            this.gameObject.GetComponent<ScoreController>().Dead();
        }
    }


    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            // yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(0.01f);

        }
        transform.position = end;
    }

}
