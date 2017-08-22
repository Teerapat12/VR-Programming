using UnityEngine;
using System.Collections;

public class gdigger : MonoBehaviour {

	public int minX = 0;
	public int maxX = 50;
	public int minY = 0;
	public int maxY = 50;

	public Vector2 loc = new Vector2 (25f, 25f);


	static int goldpitNum = 5;
	public Vector2[] goldPitsLoc = new Vector2[goldpitNum];

	static int trapNum = 2;
	static int bombDistance = 2;
	static int dangerAlert = 4;
	static int dangerWarning = 6;

	public Vector2[] traps = new Vector2[trapNum];

	public Vector2 xSpeed = new Vector2 (1f, 0f);	
	public Vector2 ySpeed = new Vector2 (0f, 1f);




	int move = 0;
	bool gameEnd = false;

	// Use this for initialization
	void Start () {

		//Greeting message
		print ("Hello, young digger");
		print ("Your task is to find and dig that gold hidden using gold-o-meter.");
		print ("Your gold-o-meter will show you the distance");
		print ("Use arrow key to move around");
		print ("Good Luck");
		print ("==================");

		print ("Number of gold: "+goldpitNum);	
		print ("Number of traps: "+trapNum);

		//Initialize game
		for(int i=0;i<goldpitNum;i++){
			goldPitsLoc [i] = new Vector2 (Random.Range (minX, maxX), Random.Range (minY, maxY));
		}

		//Initialize trap
		for (int i = 0; i < trapNum; i++) {
			traps [i] = new Vector2 (Random.Range (minX, maxX), Random.Range (minY, maxY));
		}
	}

	void gameEnding(){
		print ("YOU WIN!!!!!!!!!");
		print ("==================");
		print ("Total move: " + move);
		gameEnd = true;
	}

	void gameOver(){
		print ("GAME OVER!!!!!!");
		print("===================");
		print ("Total move: " + move);
		gameEnd = true;
	}


	void collectedGold(int diggedIndex){

		for (int i = diggedIndex; i < goldpitNum-1; i++) {
			goldPitsLoc [i] = goldPitsLoc [i + 1];
		}

		goldpitNum -= 1;

		print ("Gold Digged!!!!");
		print (goldpitNum + " left gold to dig.");

		if (goldpitNum == 0) {
			gameEnding ();
		}
			

	}

	void checkBomb(){
		for (int i = 0; i < trapNum; i++) {
			float distance = Vector2.Distance (loc, traps[i]);
			if (distance <= dangerWarning) {
				print ("Warning! Bomb nearby. Be careful");
			}
			if (distance <= dangerAlert) {
				print ("DANGERR!! Bomb nearby!! Backout!!");
			}
			else if (distance == bombDistance) {
				gameOver ();
			}
		}
	}

	void calculateDistance(){
		move += 1;
		checkBomb ();
		float minDistance = 9999999999;
		int minIndex = -1;
		for (int i = 0; i < goldpitNum; i++) {
			float distance = Vector2.Distance (loc, goldPitsLoc[i]);
			if (distance < minDistance) {
				minIndex = i;
				minDistance = distance;
			}
				
		}
			
		Debug.ClearDeveloperConsole();
		print ("Number of Gold left: " + goldpitNum);
		print("Nearest Distance: "+minDistance.ToString("F2"));
		if (minDistance == 0) {
			collectedGold (minIndex);
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (gameEnd)
			return;
		
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			loc -= xSpeed;
			calculateDistance ();

		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			loc +=xSpeed;
			calculateDistance ();
		}else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			loc -= ySpeed;
			calculateDistance ();
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			loc += ySpeed;
			calculateDistance ();
		}
	}
}
