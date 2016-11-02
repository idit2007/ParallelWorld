using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ZombieNormalMovement : MonoBehaviour {

    public int tileX;
    public int tileY;
    public int World;
    public TileMap[] map;

    public int SeenRadius;

    public int PlaytileX;
    public int PlaytileY;
    public int PlayWorld;


    Node[,] graph;
    List<Node> currentPath = null;

    public float speed;


    void Start()
    {
        GeneratePathfindingGraph();

    }

    bool wite;

    void FixedUpdate()
    {

        PlaytileX = map[World].selectedUnit.GetComponent<Unit>().tileX;
        PlaytileY = map[World].selectedUnit.GetComponent<Unit>().tileY;

        PlayWorld = Unit.World;


        if (((((transform.position.x - (map[World].StartPosition.transform.position.x)) / 2) * 10) % 10) > 5)
            tileX = (int)((transform.position.x - (map[World].StartPosition.transform.position.x)) / 2) + 1;

        else
            tileX = (int)((transform.position.x - (map[World].StartPosition.transform.position.x)) / 2);


        if (((((transform.position.z - (map[World].StartPosition.transform.position.z)) / 2) * 10) % 10) > 5)
            tileY = (int)((transform.position.z - (map[World].StartPosition.transform.position.z)) / 2) + 1;
        else
            tileY = (int)((transform.position.z - (map[World].StartPosition.transform.position.z)) / 2);



        if (PlayWorld == World)
        {

            if (Math.Sqrt(Math.Pow(Convert.ToDouble(tileX - PlaytileX), 2) + Math.Pow(Convert.ToDouble(tileY - PlaytileY), 2)) < SeenRadius && TurnController.Instance.playerMovemnet)
            {
                GeneratePathTo(tileX, tileY);
                Debug.Log("seen");
                if (this.currentPath!=null && this.currentPath.Count >1)
                {

                    Vector3 Endpositon = map[World].TileCoordToWorldCoord(currentPath[1].x, currentPath[1].y) + new Vector3(0, 1f, 0);
                    if (Vector3.Distance(transform.position, Endpositon) > 0.001f)
                    {
                        Vector3 GopositionPre = map[World].TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y) + new Vector3(0, 1f, 0);
                        transform.position = Vector3.MoveTowards(transform.position, Endpositon, speed);
                        var rotation = Quaternion.LookRotation(Endpositon - GopositionPre);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
                        
                    }


                   

                }
                if(this.currentPath != null)
                    this.currentPath.Clear();
            }
            else
            {
                Debug.Log("no seen");
            }





        }




    }


    void GeneratePathfindingGraph()
    {
        // Initialize the array
        graph = new Node[map[World].mapSizeX, map[World].mapSizeY];

        // Initialize a Node for each spot in the array
        for (int x = 0; x < map[World].mapSizeX; x++)
        {
            for (int y = 0; y < map[World].mapSizeY; y++)
            {
                graph[x, y] = new Node();
                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }

        // Now that all the nodes exist, calculate their neighbours
        // Now that all the nodes exist, calculate their neighbours
        for (int x = 0; x < map[World].mapSizeX; x++)
        {
            for (int y = 0; y < map[World].mapSizeY; y++)
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
                    if (y < map[World].mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x - 1, y + 1]);
                }

                // Try Right
                if (x < map[World].mapSizeX - 1)
                {
                    graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x + 1, y - 1]);
                    if (y < map[World].mapSizeY - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y + 1]);
                }

                // Try straight up and down
                if (y > 0)
                    graph[x, y].neighbours.Add(graph[x, y - 1]);
                if (y < map[World].mapSizeY - 1)
                    graph[x, y].neighbours.Add(graph[x, y + 1]);
            }
        }
    }

    public void GeneratePathTo(int x, int y)
    {
        // Clear out our unit's old path.
        //        selectedUnit.GetComponent<Unit>().currentPath = null;
        if (map[World].UnitCanEnterTile(x, y) == false)
        {
            // We probably clicked on a mountain or something, so just quit out.
            return;
        }
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        // Setup the "Q" -- the list of nodes we haven't checked yet.
        List<Node> unvisited = new List<Node>();

        Node source = graph[x, y];

        Node target = graph[
                            PlaytileX,
                            PlaytileY
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
                float alt = dist[u] + map[World].CostToEnterTile(u.x, u.y, v.x, v.y);
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

        this.currentPath = currentPath;

    }
}
