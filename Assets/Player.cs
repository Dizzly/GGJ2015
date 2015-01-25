using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

		// Use this for initialization


		int extraLives_ = 0;//no extra lives by default
	
		const int maxKeyObjects_ = 30;
		public int writeIndex = 0;
		public int readIndex = 0;
		KeyObject[] keysToHitQueue = new KeyObject[maxKeyObjects_];

	public float flashDurationInSec;
	float flashTimer=0;

		public void AddKeyObject (KeyObject k)
		{
				keysToHitQueue [writeIndex] = k;
				IncrementWrite ();
		}

		public void LooseGame ()
		{
		if (extraLives_==0) {
						print ("Dead");
				}

		//DEBUG REMOVE ON RELEASE
		IncrementRead ();
		}

		void IncrementRead ()
		{
				readIndex++;
				if (readIndex == maxKeyObjects_) {
						readIndex = 0;		
				}
		}

		void IncrementWrite ()
		{
				writeIndex++;
				if (writeIndex == maxKeyObjects_) {
						writeIndex = 0;		
				}
		}

	void PlayNormal()
	{


	}

		void Start ()
		{
	
		}

	void FlashLight(Color c)
	{
		Light l = GetComponent<Light> ();
		l.enabled = true;
		l.color = c;
		flashTimer = flashDurationInSec;

	}

		int TranslateInput ()
		{
				int key = (int)KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX;
				if (Input.GetButtonDown ("Key1")) {
						key = (int)KeyObject.KEY_REQUIREMENT.KEY_1;
				} else if (Input.GetButtonDown ("Key2")) {
						key = (int)KeyObject.KEY_REQUIREMENT.KEY_2;
				} else if (Input.GetButtonDown ("Key3")) {
						key = (int)KeyObject.KEY_REQUIREMENT.KEY_3;
				} else if (Input.GetButtonDown ("Key4")) {
						key = (int)KeyObject.KEY_REQUIREMENT.KEY_4;
		
				}
				return key;
		}
	
		// Update is called once per frame
		void Update ()
		{

				//disable if for the light flash
				if (flashTimer!=0) {
			flashTimer-=Time.deltaTime;
			if(flashTimer<=0)
			{
				Light l = GetComponent<Light> ();
				l.enabled = false;
			}
		}

				if (keysToHitQueue [readIndex] != null) {
						int key = TranslateInput ();
						KeyObject k = keysToHitQueue [readIndex];
						if (k.GetRequirement () == key) {
								k.IsComplete (true);
				keysToHitQueue[readIndex]=null;
				FlashLight(k.keyColors[key]);
				AudioSource a=this.GetComponent<AudioSource>();
				a.PlayOneShot(k.GetAudioClip());
								IncrementRead ();
			} else if(key!=(int)KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX) {
								//LooseGame ();
								/*This is because objects passing us do not
								 * increment the read pointer
								 * So for debug we remove this way of dying
								 * 
								 *
								 *
								 *
								 */
						}
				}

		}
}
