using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum Results{VICTORY, GAMEOVER, NONE};

public class GameManager : MonoBehaviour {

    public int maxBalls;

    [HideInInspector] public int currentLevel;
    [HideInInspector] public Results result;
    [HideInInspector] public int ballCount;

    [SerializeField] float[] thrusts; //forces applied on balls at each level
    [SerializeField] float[] times; //time available at each level
    [SerializeField] Transform[] ballPositions; //starting positions of balls
    [SerializeField] Ball _ballPrefab;
    [SerializeField] GameObject _menuButtons;
    [SerializeField] GameObject _mainLabel;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get {
            if (_instance == null)
                _instance = FindObjectOfType<GameManager>();
            return _instance;
        }
    }

	// Use this for initialization
	void Start ()
    {
		//ballCount = maxBalls;
		result = Results.NONE;
	}


	void spawnBalls(float thrust){

		if (_ballPrefab != null)
        {
            for (int i = 0; i < maxBalls; i++)
            {
                Ball ball = Instantiate<Ball>(_ballPrefab, ballPositions[i].position, Quaternion.identity);
                ball.thrust = thrust;
            }

			ballCount = maxBalls;
		} 
        else
        {
            Debug.Log("No ball prefab to load");
        }
	}

	// Update is called once per frame
	void Update () {

		//Level 0 (game loading)
		if (currentLevel == 0)
        {
			if (TimeManager.timeLeft < -3 && ballCount == 0)
            {
                currentLevel++;

                spawnBalls (thrusts[currentLevel - 1]);
				TimeManager.timeLeft = times[currentLevel - 1];

            }

        }

		//Next levels
		else if (currentLevel > 0 && currentLevel < 3)
        {
			if (TimeManager.timeLeft > 0 && ballCount == 0)
            {
				TimeManager.timeLeft = 0;
				this.GetComponent<AudioSource> ().Play ();
			} 
			
			else if (TimeManager.timeLeft < -3 && ballCount == 0)
            {
                currentLevel++;

                spawnBalls (thrusts[currentLevel - 1]);
                TimeManager.timeLeft = times[currentLevel - 1];

            }
			
			else if (TimeManager.timeLeft <= 0 && ballCount > 0)
            {
				result = Results.GAMEOVER;

                if (TimeManager.timeLeft < -3)
                    LoadMenu();

            }

		}

		
		//Last level
		else if (currentLevel >= 3)
        {
			if (TimeManager.timeLeft > 0 && ballCount == 0)
            {
				result = Results.VICTORY;
				TimeManager.timeLeft = 0;

			} else if (TimeManager.timeLeft <= -3 && ballCount == 0)
            {
                LoadMenu();

            } else if (TimeManager.timeLeft <= 0 && ballCount > 0)
            {
				result = Results.GAMEOVER;

                if (TimeManager.timeLeft < -3)
                    LoadMenu();

            }

		}

	}

    public void LoadGame()
    {
        _menuButtons.SetActive(false);
        _mainLabel.SetActive(true);
    }

    public void LoadMenu()
    {
        currentLevel = 0;
        TimeManager.timeLeft = 0;
        result = Results.NONE;

        _mainLabel.SetActive(false);
        _menuButtons.SetActive(true);
    }


	public void QuitGame(){

		Application.Quit ();
	}
}
