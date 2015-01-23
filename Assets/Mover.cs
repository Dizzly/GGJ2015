using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {


	public float speed_;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (new Vector3 (-speed_*Time.deltaTime, 0, 0));
	}
}
