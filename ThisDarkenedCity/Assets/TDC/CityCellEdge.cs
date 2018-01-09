using UnityEngine;

public class CityCellEdge : MonoBehaviour {
	public CityCell cell, otherCell;
	public CityDirection direction;

	public void Initialize (CityCell Cell, CityCell OtherCell, CityDirection Direction){
		this.cell = Cell;
		this.otherCell = OtherCell;
		this.direction = Direction;
		cell.SetEdge(Direction, this);
		transform.parent = Cell.transform;
		transform.localPosition = Vector3.zero;
		transform.localRotation = direction.ToRotation();
	}
}
