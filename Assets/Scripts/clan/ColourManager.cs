using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager {
	private static int NUMBER_OF_COLOURS = 7;
	bool[] colourAvailable = new bool[NUMBER_OF_COLOURS];
	int coloursAvailable;
	Dictionary<Color,int> colourToInt = new Dictionary<Color,int>();


	public ColourManager(){
		for (int i = 0;i < NUMBER_OF_COLOURS; i++ ) {
			colourAvailable [i] = true;
			colourToInt [toColour (i)] = i;
		}
	}


	public bool colourIsAvailable(){
		return (coloursAvailable < 7);
	}


	//get a new colour, if none available return default red colour.
	public Color getAColour(){
		if (colourIsAvailable()) {
			int i = 0;
			while (i < NUMBER_OF_COLOURS && !colourAvailable [i] ) {
				i++;
			}
			colourAvailable [i] = false;
			coloursAvailable++;
			return toColour (i);
		} else {
			return Color.red;
		}
	}


	public void relenquishColour(Color colour){
		int colourNumber = colourToInt[colour];
		if (colourAvailable[colourNumber]){
			//error, can't release a colour that is not taken
			Debug.Log("Colour that is not taken relenquished");
		} else {
			colourAvailable [colourNumber] = true;
			coloursAvailable--;
		}
	}



	// add new number and colour here and increment NUMBER_OF_COLOURS constant when adding a colour
	Color toColour(int colourNo){
		switch (colourNo) {
		case 0:
			return Color.black;
		case 1: 
			return Color.blue;
		case 2:
			return Color.cyan;
		case 3:
			return Color.grey;
		case 4:
			return Color.green;
		case 5:
			return Color.magenta;
		case 6:
			return Color.yellow;
		default:
			return Color.red;
		}
	}


}
