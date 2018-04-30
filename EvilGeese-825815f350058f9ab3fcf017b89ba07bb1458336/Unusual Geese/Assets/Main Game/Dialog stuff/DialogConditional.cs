﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
// used to determine whether a dialog option should be shown

/// <summary>
/// [EXTENSIONS] - Added money as a comparable type
/// </summary>
public class DialogConditional {
	public enum comparableTypes{
		variable,
		item,
		constant,
		money
	}
	public enum comparisonTypes{
		_true, // always true
		_false, // always false
		eq, // equal
		lt, // less than
		gt, // greater than
		le, // less than or equal
		ge 	// greater than or equal
	}
	public comparableTypes rightSideComparable;
	public comparableTypes leftSideComparable;
	public comparisonTypes ownComparison;
	public string rightSide; // either the value of the right side constant or the name of the right side variable
	public string leftSide; // same as above exept for the left side
	public InventoryItems.itemTypes rightItemType;
	public InventoryItems.itemTypes leftItemType;

	public DialogConditional(){
		ownComparison = comparisonTypes._true;
		rightSideComparable = comparableTypes.constant;
		leftSideComparable = comparableTypes.constant;
	}

	public bool evaluate(){
		string rightSideValue = "";
		string leftSideValue = "";
		int leftSideInt = 0;
		int rightSideInt = 0;

		switch (ownComparison) {
		case comparisonTypes._true:
			return true;
		case comparisonTypes._false:
			return false;
		}

		GameStateManager state = GameStateManager.getGameStateManager ();
		switch (rightSideComparable) {
		case comparableTypes.constant:
			rightSideValue = rightSide;
			break;
		case comparableTypes.variable:
			rightSideValue = state.getGameVar (rightSide);
			break;
		case comparableTypes.item:
			rightSideValue = state.getItem (rightItemType).ToString ();
			break;
		}
			
		switch (leftSideComparable) {
		case comparableTypes.constant:
			leftSideValue = leftSide;
			break;
		case comparableTypes.variable:
			leftSideValue = state.getGameVar (leftSide);
			break;
		case comparableTypes.item:
			leftSideValue = state.getItem (leftItemType).ToString ();
			break;
		// [CHANGE] Allow money to be selected as a left-side comparable type
		case comparableTypes.money:
			leftSideInt = state.money;
			break;
		}

		if (ownComparison == comparisonTypes.eq) {
			return leftSideValue == rightSideValue;
		}


		try{
			rightSideInt = int.Parse(rightSideValue);
		}catch (FormatException){
			Debug.LogError ("the right side value: \"" + rightSideValue + "\" is being used in an integer comparison but cannot be converted to an int");
			return false;
		}
		// [EXTENSION] if not money (which is already an int), then convert leftSideValue to an int
		if (leftSideComparable != comparableTypes.money) {
			try {
				leftSideInt = int.Parse (leftSideValue);
			} catch (FormatException) {
				Debug.LogError ("the left side value: \"" + leftSideValue + "\" is being used in an integer comparison but cannot be converted to an int");
				return false;
			}
		}

		switch (ownComparison) {
		case comparisonTypes.lt:
			return leftSideInt < rightSideInt;
		case comparisonTypes.gt:
			return leftSideInt > rightSideInt;
		case comparisonTypes.le:
			return leftSideInt <= rightSideInt;
		case comparisonTypes.ge:
			return leftSideInt >= rightSideInt;
		}

		throw (new Exception("unknown error"));


	}

}
