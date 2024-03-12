using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raindrop : MonoBehaviour
{
	private float speed;
	private float difficulty;

	private int term1;
	private int term2;
	public int solution;

	[SerializeField]
	private GameObject childTerm1, childTerm2, childOperator;
	private TextMesh tm1, tm2, tmOp;

	private System.Random rand;

	private GameController GC;

	void Start()
	{
		GC = GameObject.FindObjectOfType<GameController>();
		GC.raindropsList.Add(this);
		
		rand = new System.Random();

		tm1 = childTerm1.GetComponent<TextMesh>();
		tm2 = childTerm2.GetComponent<TextMesh>();
		tmOp = childOperator.GetComponent<TextMesh>();

		speed = GC.dropSpeed;
		difficulty = GC.difficulty;

		//determines what kind of operation will be generated
		switch (rand.Next(0,4))
		{
			case 0:
				GenerateAddition();
				break;
			case 1:
				GenerateSubtraction();
				break;
			case 2:
				GenerateMultiplication();
				break;
			case 3:
				GenerateDivision();
				break;
			default:
				GenerateAddition();
				break;
		}
	}

	void GenerateAddition()
	{
		term1 = rand.Next(1, (int)((difficulty * 2) + 10));
		term2 = rand.Next(1, (int)((difficulty * 2) + 10));
		solution = term1 + term2;

		tm1.text = term1.ToString();
		tm2.text = term2.ToString();
		tmOp.text = "+";
	}

	//will always result in a subtraction a-b where a>=b
	void GenerateSubtraction()
	{
		int _a;
		int _b;

		_a = rand.Next(1, (int)((difficulty * 2) + 10));
		_b = rand.Next(1, (int)((difficulty * 2) + 10));

		term1 = (_a <= _b) ? _b : _a;
		term2 = (_a <= _b) ? _a : _b;
		solution = term1 - term2;

		tm1.text = term1.ToString();
		tm2.text = term2.ToString();
		tmOp.text = "-";
	}

	void GenerateMultiplication()
	{
		term1 = rand.Next(1, (int)((difficulty) + 5));
		term2 = rand.Next(1, (int)((difficulty) + 5));
		solution = term1 * term2;

		tm1.text = term1.ToString();
		tm2.text = term2.ToString();
		tmOp.text = "x";
	}

	//will always result in a division where a is a multiple of b
	void GenerateDivision()
	{
		term2 = rand.Next(1, (int)((difficulty) + 5));
		term1 = term2 * rand.Next(1, (int)((difficulty) + 5));
		solution = term1 / term2;

		tm1.text = term1.ToString();
		tm2.text = term2.ToString();
		tmOp.text = "/";
	}

	//destroys the gameobject and loses a life / increments score depending on reason for destruction
	public void DestroyRaindrop(bool isFailed)
	{
		if (isFailed)
			GC.LoseLife();
		else
			GC.score += 100;
		Destroy(gameObject);
	}

	void Update()
	{
		transform.Translate(Vector3.down * speed * Time.deltaTime);
		if (transform.position.y <= -4.3f)
			DestroyRaindrop(true);
	}
}
