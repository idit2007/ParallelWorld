using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Unit : MonoBehaviour {

	public int tileX;
	public int tileY;
    static public int World;
    public TileMap[] map;
    static public TileMap mapStatic;

    public GameObject[] DrawLine;


    public List<Node> currentPath = null;


    static public bool haveway = false;
    int indexTarget = 0;

    int moveSpeed ;


    public float speed = 1f;
    public float cntStep = 0;
    // public int indexTarget = 0;

    void Start()
    {

    }



   void Update() {


        if (currentPath != null) {

            haveway = true;
            int currNode = 0;
            cntStep += Time.deltaTime * speed;
            DrawLine[World].GetComponent<LineRenderer>().SetVertexCount(currentPath.Count);

            while ( currNode < currentPath.Count ) {
                 Vector3 start = map[World].TileCoordToWorldCoord( currentPath[currNode].x, currentPath[currNode].y ) + new Vector3(0, 1f ,0 );

                DrawLine[World].GetComponent<LineRenderer>().SetPosition(currNode, start);

                currNode++;
			}

            if (selectpoint.StartGoway == true)
            {


                Vector3 Endpositon = map[World].TileCoordToWorldCoord(currentPath[currentPath.Count -1].x, currentPath[currentPath.Count - 1].y) + new Vector3(0, 2.1f, 0);
               // DrawLine.GetComponent<LineRenderer>().st = false;
                if (Vector3.Distance(transform.position, Endpositon) > 0.001f)
                {
                    Debug.Log("indexTarget "+ indexTarget);

                    cntStep += Time.deltaTime * speed;
                    Vector3 Goposition = map[World].TileCoordToWorldCoord(currentPath[indexTarget + 1].x, currentPath[indexTarget + 1].y) + new Vector3(0, 2.1f, 0);
                    transform.position = Vector3.MoveTowards(transform.position, Goposition, cntStep);
                    var rotation = Quaternion.LookRotation(Goposition - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
                    if (Vector3.Distance(transform.position, Goposition) < 0.001f)
                    {
                        Debug.Log("indexTarget "+ indexTarget);
                        map[World].selectedUnit.GetComponent<Unit>().tileX = currentPath[indexTarget + 1].x;
                        map[World].selectedUnit.GetComponent<Unit>().tileY = currentPath[indexTarget + 1].y;
                        transform.LookAt(Goposition);
                        indexTarget++;
                            if (Vector3.Distance(transform.position, Endpositon) < 0.01f)
                                {
                                    Debug.Log("aa");
                                        //transform.rotation = targetList[indexTarget - 1].rotation;
                                    selectpoint.StartGoway = false;
                                    selectpoint.removeway = true;


                                }

                        //  Debug.Log("X " + currentPath[currNode].x + "Y " + currentPath[currNode].y);

                    }



                }
          
            }

            if (selectpoint.removeway == true)
            {


                Debug.Log("currentPath.Count  " + currentPath.Count );
                if (currentPath.Count == 1)
                {
                    currentPath.RemoveAt(0);
                    selectpoint.removeway = false;
                    currentPath = null;
                    haveway = false;
                    //DrawLine.GetComponent<LineRenderer>().useWorldSpace = true;
                    indexTarget = 0;
                }
                else 
                    currentPath.RemoveAt(0);
            }

            if (currentPath == null)
            { DrawLine[World].GetComponent<LineRenderer>().SetPosition(0, new Vector3(1, 1, 1));

            }

        }
    }




 

}
