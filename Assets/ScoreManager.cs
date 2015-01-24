using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int currentScore;
	public int bossScore = 500;

	public void AwardScore(int i)
	{
		currentScore += i;
		if (currentScore >= bossScore)
		{
			//Acquiring spawner via Tag
			GameObject spawnerObj = GameObject.FindWithTag("Spawner");
			Spawner spawner = (Spawner)spawnerObj.GetComponent(typeof(Spawner));
			spawner.TrackBallStatus();
		}
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
