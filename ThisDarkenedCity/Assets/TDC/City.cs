using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City: MonoBehaviour {

	public int sizeX, sizeZ;
	public CityCell cityPrefab;
	public CityPassage passagePrefab;
	public CityWall wallPrefab;
	private CityCell[,] cells;

	//display delay
	public float generationStepDelay;

	public IntVector2 RandomCoordinates {
		get {
			return new IntVector2(Random.Range(0,sizeX),Random.Range(0,sizeZ));
		}
	}

	public CityCell GetCell (IntVector2 coordinates) {
		return cells[coordinates.x, coordinates.z];
	}

	public IEnumerator Generate ()
	{
		WaitForSeconds delay = new WaitForSeconds (generationStepDelay);
		cells = new CityCell[sizeX, sizeZ];

		//second generation algoritm - random walk
		//IntVector2 coordinates = RandomCoordinates;
		List<CityCell> activeCells = new List<CityCell>();
		DoFirstGenerationStep(activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	//returns a new cityCell at the specified point
	public CityCell CreateCell( IntVector2 coordinates){
		CityCell newCell = Instantiate(cityPrefab) as CityCell; 
		cells[coordinates.x,coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "City Cell " + coordinates.x + " " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3(coordinates.x - sizeX * 0.5f + 0.5f, 0f, coordinates.z - sizeZ * 0.5f + 0.5f);
		return newCell;
	}

	//returns true if the specific coordinate is within the specified bounds
	public bool containsCoordinates (IntVector2 coordinate){
		return coordinate.x >=0 && coordinate.x < sizeX && coordinate.z >= 0 && coordinate.z < sizeX;
	}

	//adds the first city cell to a random position
	private void DoFirstGenerationStep (List<CityCell> activeCells) {
		//currently selects random space anywhere on the board
		//activeCells.Add(CreateCell(RandomCoordinates));

		//generate a random cell somewhere in the middle of the board
		int randX = sizeX/2 + Random.Range(-1*(int)sizeX/4,sizeX/4);
		int randZ = sizeZ/2 + Random.Range(-1*(int)sizeZ/4,sizeZ/4);
		IntVector2 randStart = new IntVector2 (randX, randZ);
		activeCells.Add(CreateCell(randStart));
	}

	//builds new citycells out from the starting position
	private void DoNextGenerationStep (List<CityCell> activeCells)
	{
		int currentIndex = activeCells.Count - 1;
		CityCell currentCell = activeCells [currentIndex];
		CityDirection direction = CityDirections.RandomValue;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2 ();
		if (containsCoordinates (coordinates) && GetCell (coordinates) == null) {
			CityCell Neighbor = GetCell (coordinates);
			if (Neighbor == null) {
				Neighbor = CreateCell (coordinates);
				CreatePassage (currentCell, Neighbor, direction);
				activeCells.Add(Neighbor);
			} else {
				CreateWall(currentCell, Neighbor, direction);
				activeCells.RemoveAt(currentIndex);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
			activeCells.RemoveAt(currentIndex);
		}
	}

	//create passage
	private void CreatePassage (CityCell cell, CityCell otherCell, CityDirection direction){
		CityPassage passage = Instantiate(passagePrefab) as CityPassage;
		passage.Initialize(cell, otherCell, direction);
		passage = Instantiate(passagePrefab) as CityPassage;
		passage.Initialize(otherCell, cell, direction.getOpposite());
	}

	//create wall
	private void CreateWall (CityCell cell, CityCell otherCell, CityDirection direction)
	{
		CityWall wall = Instantiate (wallPrefab) as CityWall;
		wall.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate(wallPrefab) as CityWall;
			wall.Initialize( otherCell, cell, direction.getOpposite());
		}
	}
}
