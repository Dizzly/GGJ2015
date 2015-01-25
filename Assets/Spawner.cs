using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {

	
	private enum GameStatus
	{
		Normal,
		WaitingLastBalls,
		DuDAnimation,
		BossFighting,
		EndBossFighting
	};

	private object m_Handle = new object();

	private GameStatus gs;

	public GameObject template;
	public GameObject textTemplate;
	public ScoreManager scoreMan;

	public List<Transform> spawnPos= new List<Transform>();

	//Tracking position on a sorted List
	private List<Transform> trackedPosition = new List<Transform> ();
	public float baseMinimumSpacing = 3.0f;
    private float minimumSpacing;

	public float minXSpawn;
	public float maxXSpawn;


   public int bossBaseSpawnCount = 6;
   public int wavesBaseCount = 6;

	private int bossSpawnCount;
   private int wavesCount;

   public float waveBaseSpacing = 3.0f;
   private float waveSpacing;
	
	public float baseSpeed = 2.0f;
	private float currentSpeed_;

	public int currentScore_=100;

    public float xEstent;
	public float xBaseEstentOffset = 1.0f;

   //This parameter multiple seconds and tell us 
   //how long between two different spawn trials are passed

	public float spawnBaseFrequency = 0.3f;	
	private float spawnFrequency;

	private float spawnTimer;
		
	public int baseSpawnChance;

	public int spawnChanceIncrment;

	int spawnChance;

	// Use this for initialization
	void Start () {

		
		Mesh keyMesh = template.GetComponent<MeshFilter>().mesh;
		KeyObject k = template.GetComponent<KeyObject> ();
		xEstent = keyMesh.bounds.extents.x * k.transform.localScale.x;

      spawnChance = baseSpawnChance;

      minimumSpacing = baseMinimumSpacing;
      

      bossSpawnCount = bossBaseSpawnCount;
      wavesCount = wavesBaseCount;

      waveSpacing = waveBaseSpacing;

		spawnFrequency = spawnBaseFrequency;

		GameObject camObj = GameObject.FindWithTag("MainCamera");
		SpeedVar sv = camObj.GetComponent<SpeedVar> ();
		currentSpeed_ = baseSpeed = sv.GlobalSpeed;

		//Starting the game
		gs = GameStatus.Normal;

	}

	private int CompareFunction(Transform c1, Transform c2)
	{
		float x1 = c1.position.x;
		float x2 = c2.position.x;

		if (x1 > x2) {
						return 1;		
				} else if (x1 < x2) {
						return -1;
				} else
						return 0;
	}

	bool isPositionAvailable(Vector3 pos, ref int insertIndex)
	{
		bool canSpawn = false;

		if(pos.x > minXSpawn && pos.x < maxXSpawn)
		{
			if(trackedPosition.Count == 0){
				canSpawn = true;
				Debug.Log("Empty");
				insertIndex = 0;
			}
			else if(trackedPosition.Count == 1)
			{
				if(Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[0].position.x)) >= minimumSpacing)
				{
					if(pos.x < trackedPosition[0].position.x){
						canSpawn = true;
						insertIndex = 0;
						Debug.Log("Ahead");					
					}
					else{
						canSpawn = true;
						insertIndex = 1;
						Debug.Log("Ahead");
					}
				}
			}
			else{
				for (int i = 0; i < trackedPosition.Count; i++) {
					
					//if i == 0 we just need to check against the first one if is suitable
					if (i == 0)
					{
						if(pos.x < trackedPosition[i].position.x &&
						   Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							canSpawn = true;
							insertIndex = i;
							Debug.Log("Ahead");
							return canSpawn;
						}

					}
					// if i == 1 to count - 1 we need just to check if we are in good position
					// and then checking spacing against current item
					// and previous one
					else if(i < trackedPosition.Count)

					{
						if(	pos.x > trackedPosition[i-1].position.x &&
							pos.x < trackedPosition[i].position.x &&
						   	Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i - 1].position.x)) >= minimumSpacing &&
						   	Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							canSpawn = true;
							insertIndex = i;
							Debug.Log("Insert");
							return canSpawn;
						}

					}
					//It can be after the last position
					if(i == trackedPosition.Count - 1)
					{
						if(pos.x > trackedPosition[i].position.x &&
						   Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							Debug.Log("Back");
							canSpawn = true;
 							insertIndex = i + 1;
							return canSpawn;
						}
					}
				} 
			}
		}
		return canSpawn;
	}

	void SpawnKeyObj()
	{
		Vector3 pos = this.transform.position;

		if(spawnPos.Count>0)
		{
			pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
		}			                 

		//Condition on general spawning with minium space distance
		int insertIndex = -1;
		lock (m_Handle)
		{
			if (isPositionAvailable(pos, ref insertIndex))
			{
				//Predicting where this posSpawn will make the player		    
				GameObject g=(GameObject)GameObject.Instantiate (template,
				                                                 pos,
				                                                 Quaternion.identity);
				GameObject text=(GameObject)GameObject.Instantiate (textTemplate);
				
				
				GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
				
				text.transform.SetParent(canvas.transform,false);
				
				//Adding rigidbody force over y
				Rigidbody2D rd = g.GetComponent<Rigidbody2D>();
				rd.velocity = new Vector2(0, Random.Range(5, 10));
				
				Mover m = g.GetComponent<Mover> ();
				m.speed_ = currentSpeed_;
				KeyObject k = g.GetComponent<KeyObject> ();
				int key = Random.Range (0, 4);
				k.SetKey (key,false);
				Debug.Log(key);
				k.InitScore (currentScore_, scoreMan);
				TextFollow t = g.GetComponent<TextFollow> ();
				t.Init (text, key);
				
				
				trackedPosition.Insert(insertIndex,k.transform);
				
			}
		}
	}

	void SpawnInactiveKeyObj()
	{
		Vector3 pos = this.transform.position;
		if(spawnPos.Count>0)
		{
			pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
		}

		//Condition on general spawning with minium space distance
		int insertIndex = -1;

		if (isPositionAvailable (pos, ref insertIndex)) 
		{
			
			GameObject g=(GameObject)GameObject.Instantiate (template,
			                                                 pos,
			                                                 Quaternion.identity);
			GameObject text=(GameObject)GameObject.Instantiate (textTemplate);
			
			
			GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
			
			text.transform.SetParent(canvas.transform,false);

			Mover m = g.GetComponent<Mover> ();
			m.speed_ = currentSpeed_;
			KeyObject k = g.GetComponent<KeyObject> ();
			int key = Random.Range (0, 8);
			k.SetKey (key,false);
			k.Disable ();
			TextFollow t = g.GetComponent<TextFollow> ();
			t.Init (text,(int) KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX);

				trackedPosition.Insert(insertIndex,k.transform);

			k.Disable ();

		}
	}


   void SpawnWaveKeyObj()
   {
      GameObject dudObj = GameObject.FindWithTag("DUD");
	  Vector3 pos = dudObj.transform.position;
	  
      //Condition on general spawning with minium space distance
      int insertIndex = -1;
	

      if (isPositionAvailable(pos, ref insertIndex))
      {
         //Predicting where this posSpawn will make the player		    
         GameObject g = (GameObject)GameObject.Instantiate(template,
                                                          pos,
                                                          Quaternion.identity);
         GameObject text = (GameObject)GameObject.Instantiate(textTemplate);


         GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");

         text.transform.SetParent(canvas.transform, false);

         //Adding rigidbody force over y
         Rigidbody2D rd = g.GetComponent<Rigidbody2D>();
         rd.velocity = new Vector2(0, Random.Range(5, 10));

         Mover m = g.GetComponent<Mover>();
         m.speed_ = currentSpeed_;
         KeyObject k = g.GetComponent<KeyObject>();
         int key = Random.Range(0, 4);
         k.SetKey(key, false);
         Debug.Log(key);
         k.InitScore(currentScore_, scoreMan);
         TextFollow t = g.GetComponent<TextFollow>();
         t.Init(text, key);
         
         trackedPosition.Insert(insertIndex, k.transform);

         //managing the wave
         //We will hit this condition when calling with regenerated wave
         //we know here that next ball must be at minimum distance
		 
		 bossSpawnCount = bossSpawnCount - 1;
		 if(bossSpawnCount > 0)
         { 
				minimumSpacing = xEstent + xBaseEstentOffset;
            
         }
		else
		{
				wavesCount--;
				//regenerating wave
				//we need to add more space between this and the following wave
				//trick to generate space between waves
				bossSpawnCount = bossBaseSpawnCount;
				minimumSpacing = waveSpacing;
			}
			
			
		}
	}
	
	//This methond is called from the ScoreManager to
   //update us that we have to stop spawning the balls
	public void TrackBallStatus()
	{
		if (gs == GameStatus.Normal) 
		{
			spawnChance = 0;
			gs = GameStatus.WaitingLastBalls;
		}
	}

	///Start the slowing down animation
	///This method is called when all the balls are finished after reaching 100 * levelCount
	void StartBossAnimation()
	{
   	  //TODO
      //Aknowelegde DuD that we have to start animation
      //Fire event to someone
	  //Acquiring spawner via Tag
		GameObject spawnerObj = GameObject.FindWithTag("DUD");
		DudController dud = (DudController)spawnerObj.GetComponent(typeof(DudController));
		dud.StartAnimation();

	}

	///This function is called when the Animation is finished
   ///It is acknoledge by esternal
	public void EndBossAnimation()
	{
      //changing chance, minimum space, increasing velocity

      spawnChance = 100;
	  currentSpeed_ += 0.3f;
	  
	  minimumSpacing = xEstent + xBaseEstentOffset;
      spawnFrequency = 0.1f;

      bossSpawnCount= bossBaseSpawnCount;
      wavesCount = wavesBaseCount;

      gs = GameStatus.BossFighting;
	}

	private void EndBossFighting()
	{
		GameObject spawnerObj = GameObject.FindWithTag("DUD");
		DudController dud = (DudController)spawnerObj.GetComponent(typeof(DudController));
		dud.StopAnimation();
	}

	void UpdateScore()
	{
		GetComponent<ScoreManager> ().UpdateLastScore();
		currentScore_ = currentScore_ * 6 / 4;
	}

	private void UpdateGameParameters()
	{
		GameObject camObj = GameObject.FindWithTag("MainCamera");
		SpeedVar sv = camObj.GetComponent<SpeedVar> ();

		currentSpeed_ = baseSpeed = sv.GlobalSpeed += 0.25f;

		spawnFrequency = spawnBaseFrequency -= 0.2f;
		minimumSpacing = baseMinimumSpacing -= 0.3f;
		xBaseEstentOffset -= 0.2f;
		//currentSpeed_ = baseSpeed += 0.2f;
		bossSpawnCount = bossBaseSpawnCount += 1;
		wavesCount = wavesBaseCount += 1;

		spawnChance = baseSpawnChance += 5;
		spawnChanceIncrment += 2;

	}

	public void BackToNormal()
	{
		UpdateScore ();
		UpdateGameParameters ();
	}

   //Good name I choose very goos
	public void DeleteDestroyingObject(Transform tr)
	{
		//Assuming the order list when this function is called from KeyObject.cs we have to delete the first element
		Debug.Log (trackedPosition.Count);
		trackedPosition.Remove (tr);
	}

	//Calulating probability to spawna a ball
	void TrySpawnObjects()
	{
		int rand = Random.Range (0, 100);
		if (rand < spawnChance) {				
			SpawnKeyObj ();
			spawnChance=baseSpawnChance;
				} else {
			spawnChance+=spawnChanceIncrment;		
		}
	}

	// Update is called once per frame
	void Update () {

      switch (gs)
      {
         case GameStatus.Normal:

            if (spawnTimer > spawnFrequency)
            {
               spawnTimer = 0;
               TrySpawnObjects();
            }
            else
            {
               spawnTimer += Time.deltaTime;
            }

            break;
         case GameStatus.WaitingLastBalls:

            if (trackedPosition.Count == 0) 
            {
               StartBossAnimation();
               gs = GameStatus.DuDAnimation;
            }

            break;
         case GameStatus.DuDAnimation:
            //we stay in this status unil someone does not call us end animation
            // do nothing
            //TODO CHANGE THIS
            //EndBossAnimation();
            break;
         case GameStatus.BossFighting:

            //if (spawnTimer > spawnFrequency)
            //{
             //  spawnTimer = 0;
               //If we finished the waves we change status
               if(wavesCount > 0)
               {
                  SpawnWaveKeyObj();
               }
               else 
               {
                  //tricky to separate waves
					minimumSpacing = xEstent + xBaseEstentOffset;
					EndBossFighting();
                  gs = GameStatus.EndBossFighting;
               }                               
            //}
            //
			//else
            //{
            //   spawnTimer += Time.deltaTime;
            //}

            break;
         case GameStatus.EndBossFighting:
            //TODO hit a breakpoint
			if(trackedPosition.Count == 0)
			{
				BackToNormal();
				gs=GameStatus.Normal;
			}
            break;
         default:
            break;
      }
      
		
		if (Input.GetButtonDown("Debug")) {
			SpawnKeyObj();		
		}
		// x
		if (Input.GetButtonDown ("Slowdown")) {
			currentSpeed_-=0.1f;
		}
		// z
		else if(Input.GetButtonDown("Speedup"))
		{
			currentSpeed_+=0.1f;
			DebugPrint ();
		}
	}

	private void DebugPrint(){
		foreach (var tr in trackedPosition) {
			Debug.Log(tr.position.x);
				}
		Debug.Log("");
	}
}
