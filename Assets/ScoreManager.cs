using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	int currentScore;

	private int lastRecorderdScore = 0;

	public int bossScore = 500;
	private int staticBossScore;

	public void UpdateLastScore()
	{
		staticBossScore = bossScore;
		lastRecorderdScore = currentScore;
		bossScore *= 3;
	}

	public void Reset()
	{
		currentScore = 0;
		lastRecorderdScore = 0;
		bossScore = staticBossScore;
	}

	public int GetScore()
	{
		return currentScore;
	}
	public void AwardScore(int i)
	{
		currentScore += i;
		if (currentScore - lastRecorderdScore >= bossScore)
		{
			//Acquiring spawner via Tag
			GameObject spawnerObj = GameObject.FindWithTag("Spawner");
			Spawner spawner = (Spawner)spawnerObj.GetComponent(typeof(Spawner));
			spawner.TrackBallStatus();


		}
	}
	// Use this for initialization
	void Start () {
		staticBossScore = bossScore;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnGUI()
	{
		GUI.Label (new Rect(0, 0, 100, 100),currentScore.ToString());
	}
}
