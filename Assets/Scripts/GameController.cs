using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public int currentSize;
	public int maxSize;
	public GameObject snakePrefab;
	public int xBound;
	public int yBound;
	public int score;
	public GameObject foodPrefab;
	public GameObject currentFood;
	public Snake head;
	public Snake tail;
	public Vector2 nextPos;
	public int NESW;
	public Text scoreText;


	// Use this for initialization
	void OnEnable()
	{
		Snake.hit += hit;
	}

	void OnDisable()
	{
		Snake.hit -= hit;
	}
	void Start () {

		InvokeRepeating ("TimeInvoke", 0, 0.5f);
		FoodFunction ();
	}
	
	// Update is called once per frame
	void Update () {
	
		ComChangeD ();
	}

	void TimeInvoke () {
		Movement ();
		StartCoroutine (checkVisible ());

		if (currentSize >= maxSize) {
			TailFunction ();
		} 
			else
		{
			currentSize++;
		}
	}

	void Movement () {

		GameObject temp;
		nextPos = head.transform.position;

		switch (NESW) {

		case 0:
			nextPos = new Vector2 (nextPos.x, nextPos.y + 1);
				break;
		case 1:
			nextPos = new Vector2 (nextPos.x + 1, nextPos.y);
				break;
		case 2:
			nextPos = new Vector2 (nextPos.x, nextPos.y - 1);
				break;
		case 3:
			nextPos = new Vector2 (nextPos.x - 1, nextPos.y);
				break;
		default:
				break;
		}

		temp = (GameObject)Instantiate (snakePrefab, nextPos, transform.rotation);
		head.setNext (temp.GetComponent<Snake> ());
		head = temp.GetComponent<Snake> ();

		return;
	}

	void ComChangeD ()
	{
		if (NESW != 2 && Input.GetKeyDown (KeyCode.W)) {

			NESW = 0;
		}

		if (NESW != 0 && Input.GetKeyDown (KeyCode.S)) {

			NESW = 2;
		}

		if (NESW != 1 && Input.GetKeyDown (KeyCode.A)) {

			NESW = 3;
		}

		if (NESW != 3 && Input.GetKeyDown (KeyCode.D)) {

			NESW = 1;
		}
	


	}

	void TailFunction() 
	{
		Snake tempSnake = tail;
		tail = tail.getNext ();
		tempSnake.removeTail ();


	}

	void FoodFunction ()
	{
		int xPos = Random.Range (-xBound, xBound);
		int yPos = Random.Range (-yBound, yBound);

		currentFood = (GameObject)Instantiate (foodPrefab, new Vector2 (xPos, yPos), transform.rotation);
		StartCoroutine (checkRender (currentFood));
	}

	IEnumerator checkRender(GameObject IN)
	{
		yield return new WaitForEndOfFrame ();

		if (IN.GetComponent<Renderer> ().isVisible == false)
		{
			if (IN.tag == "Food")
			{
				Destroy (IN);
				FoodFunction ();
			}
		
		}
	}

	void hit(string whatwasSent)
	{
		if (whatwasSent == "Food") {
			
			FoodFunction ();
			maxSize++;
			score++;
			scoreText.text = score.ToString ();
			int temp = PlayerPrefs.GetInt ("HighScore");
			if (score > temp) {

				Debug.Log ("It should refresh");
				PlayerPrefs.SetInt ("HighScore", score);
			}
		
		}

		if (whatwasSent == "Snake") {
		
			CancelInvoke ("TimeInvoke");
			Exit ();
		}
	}

	public void Exit()
	{
		SceneManager.LoadScene (0);
	
	}


	public void Wrap()
	{
		if (NESW == 0) {
			head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y - 1));
		}
		else if (NESW == 1) {
			head.transform.position = new Vector2(-(head.transform.position.x - 1), head.transform.position.y);
		}
		else if (NESW == 2) {
			head.transform.position = new Vector2(head.transform.position.x, -(head.transform.position.y + 1));
		}
		else if (NESW == 3) {
			head.transform.position = new Vector2(-(head.transform.position.x + 1), head.transform.position.y);
		}
	
	
	
	}

	
	IEnumerator checkVisible ()
	{
		
		yield return new WaitForEndOfFrame ();
		if (head.GetComponent<Renderer> ().isVisible == false) {
						
			Wrap ();
							
							}

	}


}
