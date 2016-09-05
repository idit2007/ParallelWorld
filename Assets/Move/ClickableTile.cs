using UnityEngine;
using System.Collections;

public class ClickableTile : MonoBehaviour {

    public GameObject field;
    public int tileX;
	public int tileY;
	public TileMap map;

	void OnMouseUp() {

        if(Teleportion.TeleportButtonStatic.interactable == true && selectpoint.StartGoway == false)
        {
            map.GeneratePathTo(tileX, tileY);
           
            if (selectpoint.x == tileX && selectpoint.y == tileY && selectpoint.StartGoway == false)
                        selectpoint.StartGoway = true;

            Debug.Log ("Click!");
            Debug.Log("tileX "+ tileX + " tileY "+ tileY+ " selectpoint.x " + selectpoint.x + " selectpoint.y  " + selectpoint.y);
            selectpoint.x= tileX;
            selectpoint.y = tileY;
        }

        Debug.Log("2 " +" selectpoint.x " + selectpoint.x + " selectpoint.y  " + selectpoint.y);

    }

    
}
