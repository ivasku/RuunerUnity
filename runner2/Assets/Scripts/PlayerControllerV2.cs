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

    public GameObject[] PowerUpPrefabs;
    public GameObject[] PowerUpPositions;

    public GameObject InvincibleParticles;   
    public GameObject PickUpParticles;

    private GameObject otherGameObject;
    private bool isDead;
    private GameObject currentPowerUp;

    private bool invinciblePowerUp;
   
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < PowerUpPrefabs.Length; i++)
        {
            PoolManager.instance.CreatePool(PowerUpPrefabs[i], 2);
        }
        InvokeRepeating("spawnPowerUp", 10.0f, 12.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time < CameraObject.GetComponent<CameraControllerRunner>().animDuration)
        {
            // don`t move while the animation is running
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

        if (Input.GetKeyDown(leftControll) || (Input.GetMouseButtonDown(0) && Input.mousePosition.x < Screen.width / 2) && (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height / 2))
        {
            if (otherGameObject.tag.Equals("middleLane") || otherGameObject.tag.Equals("rightLane"))
            {
                 this.transform.position = new Vector3(this.transform.position.x - 1.8f,
                     this.transform.position.y,
                     this.transform.position.z);         
            }
        }

        if (Input.GetKeyDown(rightControll) || (Input.GetMouseButtonDown(0) && Input.mousePosition.x > Screen.width / 2) && (Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height / 2))
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


        if (other.gameObject.tag.Equals("invincible"))
        {
            //you cannot die for 5 seconds , no need for object pooling, only 1 particle system
            Instantiate(PickUpParticles, other.gameObject.transform.position,
                other.gameObject.transform.rotation);// maybe spawn some particles when pickUp on player
            other.gameObject.SetActive(false);            
            InvincibleParticles.SetActive(true);
            invinciblePowerUp = true;
            Invoke("TurnOffInvincible", 5.0f);
            Destroy(PickUpParticles, 0.5f);
        }

        //we spawn the particles here because we already have the reference 
        if (other.gameObject.tag.Equals("double_points"))
        {
            Instantiate(PickUpParticles, other.gameObject.transform.position,
               other.gameObject.transform.rotation);            
            Destroy(PickUpParticles, 0.5f);
        }

            if (other.gameObject.tag.Equals("obstacle"))
        {
            if (!invinciblePowerUp)
            {
                isDead = true;
                this.gameObject.GetComponent<ScoreController>().Dead();
            }
        }

        // always die when you fall
        if (other.gameObject.tag.Equals("obstacle_hole"))
        {           
            isDead = true;
            this.gameObject.GetComponent<ScoreController>().Dead();            
        }

    }

    private void TurnOffInvincible()
    {
        InvincibleParticles.SetActive(false);
        invinciblePowerUp = false;
    }


    private void spawnPowerUp()
    {        
        int RandomPowerUpPrefab = Random.Range(0, PowerUpPrefabs.Length);
        int RandomPosition = Random.Range(0, PowerUpPositions.Length);

        currentPowerUp = Instantiate(PowerUpPrefabs[RandomPowerUpPrefab],
                PowerUpPositions[RandomPosition].transform.position,
                Quaternion.identity);

        Destroy(currentPowerUp, 5.0f);
    }
       
}
