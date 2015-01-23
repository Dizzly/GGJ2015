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

	}

	void OnTriggerEnter2D(Collider2D col) 
	{
		if (col.gameObject.GetComponent<Player>()) {
			Player p=col.gameObject.GetComponent<Player>();
			p.AddKeyObject(this);
		}
	}

	void OnCTriggerExit2D(Collider2D col)
	{
		if (!complete) {
			Player p=col.gameObject.GetComponent<Player>();	
			p.LooseGame();
		}
	}

	// Update is called once per frame
	void Update () {
		if (complete) {
				
			DestroyObject(this.gameObject);
		}

	}
}
