using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
	[SerializeField]
	private TMP_Text difficultyText, scoreText, gameOverText;
	[SerializeField]
	private TMP_InputField inputField;
	[SerializeField]
	private GameController GC;

	[SerializeField]
	private GameObject[] lifeSprites;

	public void UpdateLives(int _livesLeft)
	{
		for (int i = 0; i < lifeSprites.Length; i++)
        {
            lifeSprites[i].SetActive(i < _livesLeft);
        }
	}

	public void UpdateScore(int _toAdd)
	{
		scoreText.text = "Score: " + GC.score.ToString();
	}

    public void UpdateDifficulty(float _difficulty)
    {
        difficultyText.text = "Difficulty :" + Math.Round(_difficulty,1);
    }

	public void SetGameOver(bool _toggle)
	{
		gameOverText.gameObject.SetActive(_toggle);
	}
}
