using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCell : MonoBehaviour {

	public IntVector2 coordinates;
	private CityCellEdge[] edges = new CityCellEdge[CityDirections.count];

	public CityCellEdge GetEdge (CityDirection direction) {
		return edges[(int)direction];
	}

	public void SetEdge (CityDirection direction, CityCellEdge edge) {
		edges[(int)direction] = edge;
	}
}
