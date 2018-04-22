using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Text scoreText;
    public DeathMenuController deathMenu;

    private float score;

    private int diffLevel = 1;
    private int maxDiffLevel = 10;
    private int scoreToNextLevel = 10;
    private bool isDead;

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

        if (score >= scoreToNextLevel)
        {
            LevelUp();
        }

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
	}

    private void LevelUp()
    {
        if (diffLevel == maxDiffLevel)
        {
            return;
        }

        scoreToNextLevel *= 2;
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
}
