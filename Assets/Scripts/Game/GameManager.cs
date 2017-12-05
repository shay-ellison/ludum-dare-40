using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;


    private string startScreen = "StartScreen";
    private string winScreen = "WinScreen";
    private string loseScreen = "LoseScreen";

    private List<string> scenes = new List<string>
    {
        "StartScreen",
        "Level1",
        "Level2",
        "WinScreen"
    };
    private int currentSceneIndex = 0;

    public void GoToNextScene()
    {
        currentSceneIndex = (currentSceneIndex + 1) % scenes.Count;
        string sceneName = scenes[currentSceneIndex];
        SceneManager.LoadScene(sceneName);
    }

    public void OnScreenName(string name)
    {
        currentSceneIndex = scenes.IndexOf(name);
    }

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            // Starting a new scene, need to hang onto the original gameObject + manager
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
