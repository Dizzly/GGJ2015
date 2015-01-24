using UnityEngine;
using System.Collections;

public class DudController : MonoBehaviour
{

		Animation anim;

		struct AnimObj
		{
				float speed;
				float xPos;
		}

		public float shockedDelay = 3.0f;
		public	float timer = -0.1f;

	private float oldSpeed=0;

		public bool isAnimating = false;
		public bool shouldMove = false;
		public bool isShocked = false;
		public bool startRunning = false;
		public bool stopFight = false;

		Vector3 destination;

		// Use this for initialization
		void Start ()
		{
				anim = GetComponent<Animation> ();
				anim.Stop ();
		}

		public void StartAnimation ()
		{
				isAnimating = true;
				shouldMove = true;
				anim.Play ("Panting");
		oldSpeed = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed;
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed = 1.4f;
		}

		public void StopAnimation ()
		{
				stopFight = true;
		}

		void Shimmy ()
		{
				float xSpeed = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed;
				if ((destination - this.transform.position).magnitude < 0.05f) {
						destination = (Random.insideUnitSphere * 0.4f) + this.transform.position;
						destination.z = 0;
						destination.y = this.transform.position.y;
				} else if(this.transform.position.x<7) {
						this.transform.Translate ((destination - this.transform.position).normalized * -xSpeed * Time.deltaTime);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				float xSpeed = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed;

				if (isAnimating) {
						if (!stopFight) {
								if (shouldMove) {
										this.transform.Translate (new Vector3 (
				xSpeed * Time.deltaTime, 0, 0));
								}
								if (this.transform.position.x <= 6 && !isShocked) {
										shouldMove = false;
										//play shocked animation
										timer = shockedDelay;
										
										this.transform.Rotate (new Vector3 (0, 180, 0));
										isShocked = true;
								}
								if (timer > 0 && isShocked &&
										!startRunning) {
										timer -= Time.deltaTime;

								} else if (timer < 0 && isShocked && !startRunning) {
										isShocked = false;
										//this.transform.Rotate (new Vector3(0,180,0));
										destination = this.transform.position;
										anim.Play ();
										GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>().EndBossAnimation();
					GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed=oldSpeed+0.4f;
										startRunning = true;
								}
								if (startRunning) {
										Shimmy ();
								}
						} else {
								this.transform.Translate (new Vector3 (-xSpeed * Time.deltaTime, 0, 0));
								if (this.transform.position.x > 11) {
										stopFight = false;
										shouldMove = false;
										isShocked = false;
										startRunning = false;
										isAnimating = false;
					GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
					//GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<SpeedVar> ().GlobalSpeed=oldSpeed;
										timer = 0;

								}
						}
				}
		}
}
