using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{
	[SerializeField]
	private GameController GC;
	[SerializeField]
	private UIController uiController;

	private TMP_InputField inputField;

	void Start()
	{
		inputField = GetComponent<TMP_InputField>();
		inputField.onValueChanged.AddListener(OnInputValueChanged);
		inputField.onEndEdit.AddListener(OnEndEdit);
		inputField.ActivateInputField();
	}
	

	//Parses the user input to an Int. Data validation not required besides making sure the line isn't empty
	//since the input field is locked to only accept numbers. If a solution is detected the field is emptied.
	void OnInputValueChanged(string newValue)
    {
		int? possibleSolution = null;

		if (newValue.Length > 0)
			possibleSolution = int.Parse(newValue);

		for (int i = 0; i < GC.raindropsList.Count; i++)
		{
			if (possibleSolution == GC.raindropsList[i].solution)
			{
				GC.raindropsList[i].DestroyRaindrop(false);
				GC.raindropsList.RemoveAt(i);
				uiController.UpdateScore(100);
				inputField.text = "";
			}
		}
    }

	//Ensures that focus on the input field is never truly lost, and makes it possible to quickly empty the input field by just
	//clicking anywhere on the screen
	void OnEndEdit(string value)
    {
		inputField.text = "";
        inputField.ActivateInputField();
    }
}
