using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class BallsCounterText : MonoBehaviour
{

	Text textComponent;

	// Use this for initialization
	void Start ()
    {
		textComponent = this.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
		char[] text = textComponent.text.ToCharArray();
		text [text.Length - 1] = (char)(48 + GameManager.Instance.maxBalls - GameManager.Instance.ballCount );
		textComponent.text = new string(text);
	}
}
