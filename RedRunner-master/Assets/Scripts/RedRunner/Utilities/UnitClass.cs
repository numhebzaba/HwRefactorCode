using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass 
{
	// Start is called before the first frame update
	public struct Unit
	{
		public string symbol;
		public float magnitude;
		public string format;

		public Unit(string symbol, float magnitude, string format)
		{
			this.symbol = symbol;
			this.magnitude = magnitude;
			this.format = format;
		}
	}
}
