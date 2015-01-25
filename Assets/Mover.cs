using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {


	public float speed_;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//if the local speed is set to negative, then dont do any moving
		if (speed_ > 0) {
						float speed = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed;

						this.transform.Translate (new Vector3 (-speed * Time.deltaTime, 0, 0));
				}
		if (Input.GetButtonDown ("Slowdown")) {
			speed_-=0.1f;
		}
		else if(Input.GetButtonDown("Speedup"))
		{
			speed_+=0.1f;
		}
	}

}
