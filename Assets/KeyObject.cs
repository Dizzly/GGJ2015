using UnityEngine;
using System.Collections;

public class KeyObject : MonoBehaviour {

	

	public enum KEY_REQUIREMENT{
		KEY_1=0,
		KEY_2=1,
		KEY_3=2,
		KEY_4=3,
		KEY_MOD_1=4,
		KEY_MOD_2=5,
		KEY_MOD_3=6,
		KEY_MOD_4=7,
		KEY_NULL_MAX=8,
	}

	public int key_req=(int)KEY_REQUIREMENT.KEY_NULL_MAX;

	bool complete=false;
	bool added=false;
	bool removed=false;

	int scoreValue;
	ScoreManager scoreMan;

	public Color[] keyColors = new Color[8]{
		Color.red,
		Color.blue,
		Color.yellow,
		Color.green,
		Color.red,
		Color.blue,
		Color.yellow,
		Color.green,
	};

	// Use this for initialization
	void Start () {
	
	}

	public int GetRequirement()
	{
		return key_req;
	}

	public void IsComplete(bool b)
	{
		complete = b;
		if (scoreMan) {
						scoreMan.AwardScore (scoreValue);
				}

	}

	public void Disable()
	{
		added = true;
		removed = true;
	}

	public void InitScore(int score, ScoreManager s)
	{
		scoreValue = score;
		scoreMan = s;
	}

	public void SetKey(int key)
	{
		key_req = key;
		gameObject.renderer.material.color = keyColors [key];
		GetComponentInChildren<LightPulser> ().l.color = keyColors [key];
	}

	void OnTriggerEnter2D(Collider2D col) 
	{
			Player p=col.gameObject.GetComponent<Player>();
		if (p&&!added) {
					p.AddKeyObject (this);
			added=true;
				}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (!complete) {
			Player p=col.gameObject.GetComponent<Player>();	
			if(p&&!removed)
			{
				removed=true;
				p.LooseGame();
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (complete) {
				
			DestroyObject(this.gameObject);
		}

		if (transform.position.y < -8) {
			DestroyObject(this.gameObject);
		}

	}
}
