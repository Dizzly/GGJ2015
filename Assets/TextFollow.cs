using UnityEngine;
using System.Collections; 
using UnityEngine.UI;

public class TextFollow : MonoBehaviour {

	 GameObject text;

	// Use this for initialization
	void Start () {
	
	}

	public void Init(GameObject t, int key)
	{
		text = t;
		t.GetComponent<Text> ().text = key.ToString ();
	}
	
	// Update is called once per frame
	void Update () {
		if (text) {
						text.transform.position = Camera.main.WorldToScreenPoint (transform.position);
				}
		}

	void OnDestroy()
	{
		GameObject.Destroy (text);

	}
}
