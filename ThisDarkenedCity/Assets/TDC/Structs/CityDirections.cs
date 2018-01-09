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

	private static CityDirection[] opposites = {
		CityDirection.South,
		CityDirection.West,
		CityDirection.North,
		CityDirection.East
	};

	private static Quaternion[] rotations = {
		Quaternion.identity,
		Quaternion.Euler(0f, 90f, 0f),
		Quaternion.Euler(0f, 180f, 0f),
		Quaternion.Euler(0f, 270f, 0f)
	};

	public static CityDirection RandomValue {
		get {
			return (CityDirection) Random.Range(0,count);
		} 
	}

	public static CityDirection getOpposite (this CityDirection direction){
		return opposites[ (int) direction ];
	}

	public static IntVector2 ToIntVector2 (this CityDirection direction) {
		return vectors[(int)direction];
	}

	public static Quaternion ToRotation (this CityDirection direction) {
		return rotations[(int)direction];
	}
}
