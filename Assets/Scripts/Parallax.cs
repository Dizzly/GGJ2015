using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	private Vector3 newPosition;
	public float _Zoffset=1.0f;
	public float _objWidth;
	public bool _moveBackgound;
	public Transform spawnTransform;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float speed = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed;
		if(_moveBackgound)
		{
			transform.Translate((-speed/_Zoffset) * Time.deltaTime,0,0);
		}
		if (transform.position.x < -_objWidth * 2) {
			transform.position = new Vector3 (spawnTransform.position.x + _objWidth,
			                                                                 transform.position.y,
			                                                                 transform.position.z);
		}
	}

	void OnBecameInvisible() {
		//transform.position = new Vector3 (spawnTransform.position.x + _objWidth,
		//                                spawnTransform.position.y,
		//                                spawnTransform.position.z);
	}
}
