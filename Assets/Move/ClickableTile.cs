using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public GameObject field;
    public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseUp() {


        Debug.Log ("Click!");
        if (selectpoint.x == tileX && selectpoint.y == tileY)
            selectpoint.StartGoway = true;
        //Unit.MoveNextTile();


        Debug.Log("tileX "+ tileX + " tileY "+ tileY);
        map.GeneratePathTo(tileX, tileY);
        selectpoint.x= tileX;
        selectpoint.y = tileY;


    }

    
}
