using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TimeManager : MonoBehaviour
{
	public static float timeLeft; //time left to next level

    [SerializeField] TextMeshProUGUI _mainLabel;


	// Use this for initialization
	void Start ()
    {
		timeLeft = 0;
	}
		
	
	// Update is called once per frame
	void Update () {

		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0 && timeLeft >= -4)
        {

			switch (GameManager.Instance.result)
            {
				case Results.NONE:
					if (GameManager.Instance.currentLevel == 0)
						_mainLabel.text = "Slow";
					else if (GameManager.Instance.currentLevel == 1)
						_mainLabel.text = "Faster";
					else if (GameManager.Instance.currentLevel == 2)
						_mainLabel.text = "Insane!";
					break;

				case Results.VICTORY:
					_mainLabel.text = "Congratulations!";
					break;
				case Results.GAMEOVER:
					_mainLabel.text = "Try again...";
					break;
			}
		}
        else
        {
			if (Mathf.Round (timeLeft) >=10)
				_mainLabel.text = "00 : " + Mathf.Round (timeLeft).ToString ();
			else 
				_mainLabel.text = "00 : 0" + Mathf.Round (timeLeft).ToString ();
		}
	}
}
