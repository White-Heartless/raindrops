using System;
using System.Collections;
using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GameController : MonoBehaviour
{

	[SerializeField]
	private GameObject dropPrefab;
	[SerializeField]
	private UIController uiController;
	
	private float _dropSpeed = 1f;
	private float _difficulty = 1f;
	private int _score = 0;

	private int livesLeft = 3;

	public float dropSpeed{get {return _dropSpeed;}}
	public float difficulty{get {return _difficulty;}}
	public int score{get {return _score;} set {_score = value;}}

	private Vector3 spawnPos = new Vector3(0f,6.0f,0f);

	private float lastSpawnPos = 0f;

	private System.Random rand;

	public List<Raindrop> raindropsList = new List<Raindrop>();
	
	void Start()
	{
		StartCoroutine(SpawnRoutine());
	}

	void TogglePause() 
	{
		if (Time.timeScale == 0f)
			Time.timeScale = 1f;
		else
			Time.timeScale = 0f;
	}

	void SpawnDrop()
	{
		GameObject spawnedObject = Instantiate(dropPrefab, spawnPos, Quaternion.identity);
	}


	//Tries to spawn a Raindrop in a random location, but tries again if it's too close to the location of the last
	//one to prevent excessive overlapping. Also increments difficulty of operations and drop speed.
	private IEnumerator SpawnRoutine()
	{
		while (true)
        {
            do
				spawnPos.x = UnityEngine.Random.Range(-8.0f, 8.0f);
			while (Mathf.Abs(spawnPos.x - lastSpawnPos) < 1.5f);

			lastSpawnPos = spawnPos.x;
			SpawnDrop();
			_difficulty += 0.1f;
			_dropSpeed += 0.05f;
			uiController.UpdateDifficulty(_difficulty);

			yield return new WaitForSeconds(3.0f);
        }
	}

	public void LoseLife()
	{
		if (livesLeft > 1)
		{
			livesLeft--;
			uiController.UpdateLives(livesLeft);
		}	
		else
			GameOver();
	}

	void GameOver()
	{
		uiController.SetGameOver(true);
		TogglePause();
	}
}
