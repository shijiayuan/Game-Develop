using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public Button gamebtn;
	// Use this for initialization
	void Start () {
        gamebtn.onClick.AddListener(startGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void startGame()
    {
        SceneManager.LoadScene("game");
    }
    
}
