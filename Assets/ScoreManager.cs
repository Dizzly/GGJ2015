using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int currentScore;


	public void AwardScore(int i)
	{
		currentScore += i;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnGUI()
	{
		
		GUI.Label (new Rect(0, 0, 100, 100),currentScore.ToString());
	}
}
