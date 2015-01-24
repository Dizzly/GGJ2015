using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	private Vector3 newPosition;
	public float _speed = 1.0f;
	public float _Zoffset=1.0f;
	public float _objWidth;
	public bool _moveBackgound;
	public Transform spawnTransform;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_moveBackgound)
		{
			transform.Translate((-_speed/_Zoffset) * Time.deltaTime,0f,0f);
		}
		else
		{
			//do nothing
		}
	}

	void OnBecameInvisible() {
		transform.position = new Vector3 (spawnTransform.position.x + _objWidth,
		                                 spawnTransform.position.y,
		                                 spawnTransform.position.z);
	}
}
