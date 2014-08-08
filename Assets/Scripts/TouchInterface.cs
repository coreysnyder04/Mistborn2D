using UnityEngine;
using System.Collections;

public class TouchInterface : MonoBehaviour {

	public float comfortZoneVerticalSwipe = 1000; // the vertical swipe will have to be inside a 50 pixels horizontal boundary
	public float comfortZoneHorizontalSwipe = 10; // the horizontal swipe will have to be inside a 50 pixels vertical boundary
	public float minSwipeDistance = 14; // the swipe distance will have to be longer than this for it to be considered a swipe
	//the following 4 variables are used in some cases that I don’t want my character to be allowed to move on the board (it’s a board game)
	public float startTime;
	public Vector2 startPos;
	public float maxSwipeTime;

	private Transform player;		// Reference to the player.
	private GameObject playerGO; 

	void Awake ()
	{
		Debug.Log("HERE");
		FindPlayer();
	}

	void Update(){
		if (Input.touchCount >0) {
			Debug.Log("Touch Began");
			Touch touch = Input.touches[0];
			
			switch (touch.phase) { //following are 2 cases
				
			case TouchPhase.Began: //here begins the 1st case
				Debug.Log("Touch Began");
				startPos = touch.position;
				startTime = Time.time;
				
				break; //here ends the 1st case
				
			case TouchPhase.Ended: //here begins the 2nd case
				float swipeTime = Time.time - startTime;
				float swipeDist = (touch.position - startPos).magnitude;
				
				Debug.Log("Touch Ended");
				Debug.Log(Mathf.Abs(touch.position.x - startPos.x));
				Debug.Log(swipeTime);
				Debug.Log(swipeDist);

				if ((Mathf.Abs(touch.position.x - startPos.x))<comfortZoneVerticalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.y - startPos.y)>0)
				{
					/*
					if(player.position.y > 0 )
					{
						player.position.y --;
						player.transform.position = LevelCreator.gridPositionArray[player.position.x,player.position.y];
						CheckMine();
					}
					*/
					playerGO.GetComponent<PlayerController>().Jump();
					
					Debug.Log("UP");
				}
				
				
				if ((Mathf.Abs(touch.position.x - startPos.x))<comfortZoneVerticalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.y - startPos.y)<0)
				{
					
					Debug.Log("down");
				}
				
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.x - startPos.x)<0)
				{
					
					Debug.Log("left");
				}
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.x - startPos.x)>0)
				{
					
					Debug.Log("right");
					
				}
				break; //here ends the 2nd case
			}
		}
	}

	void OnGUI()
	{
		GUI.Box (new Rect (5,5,500,30), "My Var Is: Corey");
	}

	void FindPlayer(){
		playerGO = GameObject.FindGameObjectWithTag("Player");
		if(playerGO){
			player = playerGO.transform;
		}	
	}
}
