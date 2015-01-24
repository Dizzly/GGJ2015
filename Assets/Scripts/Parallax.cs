using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	private Vector3 newPosition;
	public float _speed = 1.0f;
	public bool _moveBackgound;
	public Transform spawnTransform;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(_moveBackgound)
		{
			transform.Translate(_speed * Time.deltaTime,0f,0f);
		}
		else
		{
			//do nothing
		}
	}

	void OnBecameInvisible() {
		transform.position = spawnTransform.position;
	}
}
