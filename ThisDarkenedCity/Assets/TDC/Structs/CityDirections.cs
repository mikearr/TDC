using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CityDirections {

	public const int count = 4;

	private static IntVector2[] vectors = {
		new IntVector2(0, 1),
		new IntVector2(1, 0),
		new IntVector2(0, -1),
		new IntVector2(-1, 0)
	};

	public static CityDirection RandomValue {
		get {
			return (CityDirection) Random.Range(0,count);
		} 
	}

	public static IntVector2 ToIntVector2 (this CityDirection direction) {
		return vectors[(int)direction];
	}
}
