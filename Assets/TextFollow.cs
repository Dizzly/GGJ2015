using UnityEngine;
using System.Collections; 
using UnityEngine.UI;

public class TextFollow : MonoBehaviour {

	 GameObject text;

	string[] keyStrings= new string[5]
	{
		"B",
		"X",
		"Y",
		"A",
		""
	};

	// Use this for initialization
	void Start () {
	
	}

	public void Init(GameObject t, int key)
	{
		text = t;
		t.GetComponent<Text> ().text = keyStrings [key];
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
