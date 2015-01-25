using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScript : MonoBehaviour
{

		// Use this for initialization
		GameObject player;
		Spawner s;
		public Text titleText;
		public Transform textStart;
		public Transform textEnd;
		Transform personalTextStart;
		Transform personalTextEnd;
		//Camera c;
		public float introDuration = 5.0f;
		float introTimer = -1;
		int spawnerBaseChance;
		int spawnerSpawnIncrement;
		float spawnerFrequency;
		bool on = false;
		bool safeToStart=false;
		bool returnToOrigin = true;

		void Start ()
		{
				TitleScreen ();
			titleText.text = "D.U.D";
		safeToStart = true;
		}

		public void SetSafeToStart(bool b)
	{
		safeToStart=b;

	}

		public void TitleScreen()
		{
				on = true;
		returnToOrigin = true;

		introTimer = introDuration = 5.0f;
				//c = (Camera)GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera> ();
				Spawner spawn;
				spawn = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<Spawner> ();
		ScoreManager scorer = GameObject.FindGameObjectWithTag ("Spawner").GetComponent<ScoreManager> ();
				spawnerFrequency = spawn.spawnBaseFrequency;
				spawnerBaseChance = spawn.baseSpawnChance;
				spawnerSpawnIncrement = spawn.spawnChanceIncrement;
		
				spawn.spawnBaseFrequency = 100;
				spawn.baseSpawnChance = 0;
				spawn.spawnChanceIncrement = 0;

				safeToStart = false;
		
				personalTextEnd = textEnd;
				personalTextStart = textStart;
		titleText.text = "GAME OVER \n Score:" + scorer.GetScore();

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (on) {
						float t = (introDuration - introTimer) / introDuration;
						Vector3 pos = personalTextStart.position + (personalTextEnd.position - personalTextStart.position) * t;
						titleText.transform.position = pos;
						introTimer -= Time.deltaTime;
						if ((personalTextEnd.position - pos).magnitude < 1f && returnToOrigin) {
								Transform temp;
								temp = personalTextStart;
								personalTextStart = personalTextEnd;
								personalTextEnd = temp;
								introTimer = introDuration;
						} else if ((personalTextEnd.position - pos).magnitude < 1 && !returnToOrigin) {
								on = false;
								GameObject go = GameObject.FindGameObjectWithTag ("Spawner");
								Spawner spawn = go.GetComponent<Spawner> ();
								ScoreManager sc = go.GetComponent<ScoreManager> ();
				Player p=GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
				
								spawn.spawnBaseFrequency = spawnerFrequency;
								spawn.baseSpawnChance = spawnerBaseChance;
								spawn.spawnChanceIncrement = spawnerSpawnIncrement;
								spawn.Reset ();
								sc.Reset ();
								p.Wakeup();

						}
		

						if (Input.anyKeyDown && returnToOrigin&& safeToStart) {
								returnToOrigin = false;
								
								introDuration = 1.0f;
								introTimer = 1.0f;
								personalTextStart=transform;
								
						}
					

				}
		}
}
