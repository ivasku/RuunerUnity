using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenuController : MonoBehaviour {

    public Text scoreText;
    public Image background;

    private bool isShowActive;
    private float transition;

	// Use this for initialization
	void Start () {
        transition = 0.0f;
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isShowActive)
        {
            return;
        }

        transition += Time.deltaTime;
        background.color = Color.Lerp(new Color(0,0,0,0), Color.black, transition);

    }

    public void ShowEndMenu(float score)
    {
        this.gameObject.SetActive(true);
        scoreText.text = ((int)score).ToString();
        isShowActive = true;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
