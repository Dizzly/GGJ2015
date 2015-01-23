using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization


	int extaLives_=0;//no extra lives by default

	bool modifierKeyPressed_;//the modifier key to switch between colour and shape

	const int maxKeyObjects_=100;
	int writeIndex=0;
	int readIndex=0;

	KeyObject[] keysToHitQueue = new KeyObject[maxKeyObjects_];

	public void AddKeyObject(KeyObject k)
	{
				if (writeIndex == maxKeyObjects_-1) {
						writeIndex=0;
				}
		keysToHitQueue [writeIndex] = k;
		writeIndex++;
	}

	public void LooseGame()
	{

	}

	void Start () {
	
	}

	int TranslateInput()
	{
		int key = (int)KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX;
		if (Input.GetButton ("Key1")) {
			key=(int)KeyObject.KEY_REQUIREMENT.KEY_1;
				} else if (Input.GetButton ("Key2")) {
			key=(int)KeyObject.KEY_REQUIREMENT.KEY_2;
				} else if (Input.GetButton ("Key3")) {
			key=(int)KeyObject.KEY_REQUIREMENT.KEY_3;
				}else if(Input.GetButton("Key4")){
			key=(int)KeyObject.KEY_REQUIREMENT.KEY_4;
		
		}
		if(Input.GetButton("ModKey"))
		  {
			if(key!=(int)KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX)
			{
				key+=4;
			}

		}
		return key;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
