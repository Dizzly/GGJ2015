using UnityEngine;
using System.Collections;

public class LightPulser : MonoBehaviour {


	public Light l;

	public float intensityMax;
	public float intensityMin;

	public float intensityChangeSpeed;

	public bool on=true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (on) {
			l.intensity+=intensityChangeSpeed;
			if(l.intensity>intensityMax||
			   l.intensity<=intensityMin)
			{
				intensityChangeSpeed= -intensityChangeSpeed;
				if(l.intensity>intensityMax)
				{
					l.intensity=intensityMax;
				}
			}
		}
	}
}
