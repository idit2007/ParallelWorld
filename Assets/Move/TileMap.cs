using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public GameObject StartPosition;
    public int WorldMap;
    public TileType[] tileTypes;
    public Vector4[] wall;
    public Vector4[] wallS;
    public GameObject[] ZombieNormal;

    int[,,] tiles;
    Node[,] graph;


    public int mapSizeX = 15;
    public int mapSizeY = 19;



    void Start()
    {

        // Setup the selectedUnit's variable



        GenerateMapData();
        GeneratePathfindingGraph();
        GenerateMapVisual();
    }
    void Update()
    {
        if (WorldMap == Unit.World)
        {
            if (((((selectedUnit.transform.position.x - (StartPosition.transform.position.x)) / 2) * 10) % 10) > 5)
                selectedUnit.GetComponent<Unit>().tileX = (int)((selectedUnit.transform.position.x - (StartPosition.transform.position.x)) / 2) + 1;

            else
                selectedUnit.GetComponent<Unit>().tileX = (int)((selectedUnit.transform.position.x - (StartPosition.transform.position.x)) / 2);


            if (((((selectedUnit.transform.position.z - (StartPosition.transform.position.z)) / 2) * 10) % 10) > 5)
                selectedUnit.GetComponent<Unit>().tileY = (int)((selectedUnit.transform.position.z - (StartPosition.transform.position.z)) / 2) + 1;
            else
                selectedUnit.GetComponent<Unit>().tileY = (int)((selectedUnit.transform.position.z - (StartPosition.transform.position.z)) / 2);
        }
        else
        {

        }




    }






    void GenerateMapData()
    {
        // Allocate our map tiles
        tiles = new int[mapSizeX, mapSizeY, 2];

        int x, y;

        // Initialize our map tiles to be grass
        for (x = 0; x < mapSizeX; x++)
        {
            for (y = 0; y < mapSizeY; y++)
            {
                tiles[x, y, WorldMap] = 0;

            }
        }



        for (int i = 0; i < wallS.Length; i++)
        {
            for (int WallX = (int)(wallS[i].z); WallX < wallS[i].x; WallX++)
                for (int WallY = (int)(wallS[i].w); WallY < wallS[i].y; WallY++)
                    tiles[WallX, WallY, WorldMap] = 2;
        }

        for (int i = 0; i < wall.Length; i++)
        {
            for (int WallX = (int)(wall[i].z); WallX < wall[i].x; WallX++)
                for (int WallY = (int)(wall[i].w); WallY < wall[i].y; WallY++)
                    tiles[WallX, WallY, WorldMap] = 1;
        }




    }

    public float CostToEnterTile(int sourceX, int sourceY, int targetX, int targetY)
    {

        TileType tt = tileTypes[tiles[targetX, targetY, WorldMap]];

        if (UnitCanEnterTile(targetX, targetY) == false)
            return Mathf.Infinity;

        float cost = tt.movementCost;

        if (sourceX != targetX && sourceY != targetY)
        {
            // We are moving diagonally!  Fudge the cost for tie-breaking
            // Purely a cosmetic thing!
            cost += 0.001f;
        }

        return cost;

    }

    void GeneratePathfindingGraph()
    {
        // Initialize the array
        graph = new Node[mapSizeX, mapSizeY];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {

                // This is the 4-way connection version:
                /*				if(x > 0)
                                    graph[x,y].neighbours.Add( graph[x-1, y] );
                                if(x < mapSizeX-1)
                                    graph[x,y].neighbours.Add( graph[x+1, y] );
                                if(y > 0)
                                    graph[x,y].neighbours.Add( graph[x, y-1] );
                                if(y < mapSizeY-1)
                                    graph[x,y].neighbours.Add( graph[x, y+1] );
                */

                // This is the 8-way connection version (allows diagonal movement)
                // Try left
                if (x > 0)
                {
                    graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }
    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y, WorldMap]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, StartPosition.transform.position + new Vector3(2 * x, 0, 2 * y), Quaternion.identity);

                if (WorldMap == 0)
                {
                    GameObject environemt = GameObject.Find("Map1");
                    Transform environemtParrent = environemt.GetComponent<Transform>();
                    go.transform.parent = environemtParrent.transform;
                }
                else
                {
                    GameObject environemt = GameObject.Find("Map2");
                    Transform environemtParrent = environemt.GetComponent<Transform>();
                    go.transform.parent = environemtParrent.transform;
                }

                ClickableTile ct = go.GetComponent<ClickableTile>();
                /*
                                GameObject go = (GameObject)Instantiate( tt.tileVisualPrefab, StartPosition.transform.position +new Vector3(2*x, 0, 2 * y), Quaternion.identity );
                                GameObject environemt = GameObject.Find ("Environment");
                                Transform environemtParrent = environemt.GetComponent<Transform> ();
                                go.transform.parent = environemtParrent.transform;
                                ClickableTile ct = go.GetComponent<ClickableTile>();
                */

                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return StartPosition.transform.position + new Vector3(2 * x, 0, 2 * y);

    }

    public bool UnitCanEnterTile(int x, int y)
    {

        // We could test the unit's walk/hover/fly type against various
        // terrain flags here to see if they are allowed to enter the tile.

        return tileTypes[tiles[x, y, WorldMap]].isWalkable;
    }

    public void GeneratePathTo(int x, int y)
    {
        // Clear out our unit's old path.
        //        selectedUnit.GetComponent<Unit>().currentPath = null;

        if (UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[
                                 x, y
                            ];

        Node target = graph[
                            selectedUnit.GetComponent<Unit>().tileX,
                            selectedUnit.GetComponent<Unit>().tileY
                            ];

        dist[source] = 0;
        prev[source] = null;

        // Initialize everything to have INFINITY distance, since
        // we don't know any better right now. Also, it's possible
        // that some nodes CAN'T be reached from the source,
        // which would make INFINITY a reasonable value
        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }

            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            // "u" is going to be the unvisited node with the smallest distance.
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;  // Exit the while loop!
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbours)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + CostToEnterTile(u.x, u.y, v.x, v.y);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        // If we get there, the either we found the shortest route
        // to our target, or there is no route at ALL to our target.

        if (prev[target] == null)
        {
            // No route between our target and the source
            return;
        }

        List<Node> currentPath = new List<Node>();

        Node curr = target;

        // Step through the "prev" chain and add it to our path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        // Right now, currentPath describes a route from out target to our source
        // So we need to invert it!

        currentPath.Reverse();

        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

}
