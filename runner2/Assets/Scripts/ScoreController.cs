using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public DeathMenuController deathMenu;
    public GameObject DoublePointsParticles;

    private float score;
    private float timeInGame;

    private int diffLevel = 1;
    private int maxDiffLevel = 20;
    private int scoreToNextLevel = 10;
    private int timeToNextLevel = 10;
    private bool isDead;

    private bool doublePointsPowerUp;

	// Use this for initialization
	void Start () {
        score = 0;
        scoreText.text = "0";
    }
	
	// Update is called once per frame
	void Update () {

        if (isDead)
        {
            return;
        }

        if (timeInGame >= timeToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime;
        timeInGame += Time.deltaTime;

        if (doublePointsPowerUp)
        {
            score++; // increment again score
        }

        scoreText.text = ((int)score).ToString();
	}

    private void LevelUp()
    {
        if (diffLevel == maxDiffLevel)
        {
            return;
        }

        timeToNextLevel *= 2;
        diffLevel++;

        this.gameObject.GetComponent<PlayerControllerV2>().SetSpeed(diffLevel);
    }

    public void Dead()
    {
        isDead = true;
        if (PlayerPrefs.GetFloat("score") < score)
        {
            PlayerPrefs.SetFloat("score", score);
        }        
        deathMenu.ShowEndMenu(score);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("double_points"))
        {
            doublePointsPowerUp = true;
            DoublePointsParticles.SetActive(true);
            Invoke("DisableDoublePoints", 3.5f);
        }
    }

    private void DisableDoublePoints()
    {
        doublePointsPowerUp = false;
        DoublePointsParticles.SetActive(false);
    }
}
