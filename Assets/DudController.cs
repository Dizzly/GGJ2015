using UnityEngine;
using System.Collections;

public class DudController : MonoBehaviour {

	Animation anim;

	public float xSpeed;

	struct AnimObj{
		float speed;
		float xPos;
	}

	public float shockedDelay=3.0f;
	public	float timer=-0.1f;

	public bool isAnimating=false;
	public bool shouldMove=false;
	public bool isShocked=false;
	public bool startRunning=false;

	Vector3 destination;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
	}

	void StartAnimation()
	{
		isAnimating = true;
		shouldMove = true;
	}

	void Shimmy()
	{
				if ((destination - this.transform.position).magnitude < 0.05f) {
						destination = (Random.insideUnitSphere * 0.4f) + this.transform.position;
			destination.z=0;
			destination.y=this.transform.position.y;
		} else {
			this.transform.Translate((destination-this.transform.position).normalized*xSpeed*Time.deltaTime);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimating) {
			if(shouldMove)
			{
			this.transform.Translate(new Vector3(
				-xSpeed*Time.deltaTime,0,0));
			}
			if(this.transform.position.x<=6&&!isShocked)
			{
				shouldMove=false;
				//play shocked animation
				timer=shockedDelay;
				isShocked=true;
			}
			if(timer>0&&isShocked)
			{
				timer-=Time.deltaTime;

			}
			else if(timer<0&&isShocked)
			{
				isShocked=false;
				destination=this.transform.position;
				startRunning=true;
			}
			if(startRunning)
			{
				Shimmy ();
			}
			                    
		}
	}
}
