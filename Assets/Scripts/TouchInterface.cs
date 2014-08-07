using UnityEngine;
using System.Collections;

public class TouchInterface : MonoBehaviour {

	public float comfortZoneVerticalSwipe = 50; // the vertical swipe will have to be inside a 50 pixels horizontal boundary
	public float comfortZoneHorizontalSwipe = 50; // the horizontal swipe will have to be inside a 50 pixels vertical boundary
	public float minSwipeDistance = 14; // the swipe distance will have to be longer than this for it to be considered a swipe
	//the following 4 variables are used in some cases that I don’t want my character to be allowed to move on the board (it’s a board game)
	public float startTime;
	public Vector2 startPos;
	public float maxSwipeTime;

	private Transform player;		// Reference to the player.
	private GameObject playerGO; 

	void Awake ()
	{
		FindPlayer();
	}

	void Update(){
		if (Input.touchCount >0) {
			Touch touch = Input.touches[0];
			
			switch (touch.phase) { //following are 2 cases
				
			case TouchPhase.Began: //here begins the 1st case
				startPos = touch.position;
				startTime = Time.time;
				
				break; //here ends the 1st case
				
			case TouchPhase.Ended: //here begins the 2nd case
				float swipeTime = Time.time - startTime;
				float swipeDist = (touch.position - startPos).magnitude;
				
				
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
					
					print("up");
				}
				
				
				if ((Mathf.Abs(touch.position.x - startPos.x))<comfortZoneVerticalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.y - startPos.y)<0)
				{
					
					print("down");
				}
				
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.x - startPos.x)<0)
				{
					
					print("left");
				}
				
				if ((Mathf.Abs(touch.position.y - startPos.y))<comfortZoneHorizontalSwipe && (swipeTime < maxSwipeTime) && (swipeDist > minSwipeDistance) && Mathf.Sign(touch.position.x - startPos.x)>0)
				{
					
					print("right");
					
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
