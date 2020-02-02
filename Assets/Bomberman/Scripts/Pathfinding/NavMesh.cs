using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// this class keep track of which grid is walkable and non walkable 
/// also gives the storest path from A to B 
/// this class uses Astar path algorithm 
/// which is inspired by 
/// https://www.redblobgames.com/pathfinding/a-star/introduction.html
/// https://www.youtube.com/watch?v=-L-WgKMFuhE&list=PLFt_AvWsXl0cq5Umv3pMC9SPnKjfp9eGW
/// Thanks
/// </summary>
public class NavMesh : MonoBehaviour
{
    public LayerMask nonWalkableLayerMask;
    public int width;
    public int height;
    public bool debug;

    PathFind.Grid grid;
    List<Vector2> walkablePoints = new List<Vector2>();

    List<PathFind.Point> path = new List<PathFind.Point>();

    public Transform startPoint;
    public Transform endPoint;

    static NavMesh Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GenerateNavMesh();
    }

    void GenerateNavMesh ()
    {
        float[,] tileMap = new float[width, height];
        RaycastHit2D hit;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                hit = Physics2D.Raycast(new Vector3(x + 0.5f, y + 0.5f, 0), Vector2.zero, 3, nonWalkableLayerMask);
                if(hit)
                {
                    tileMap[x, y] = 0;
                }
                else
                {
                    tileMap[x, y] = 1;
                    walkablePoints.Add(new Vector2(x, y));
                }
            }
        }
         grid = new PathFind.Grid(width, height, tileMap);
    }

    private void Update()
    {
        if(!debug)
        {
            return;
        }

        if(Input.GetMouseButtonDown(0))
        {
            GetPath(startPoint.position, endPoint.position);
        }
    }

    public static Vector3[] GetPath(Vector2 start,Vector2 end)
    {
        PathFind.Point startPoint = new PathFind.Point((int)start.x, (int)start.y);
        PathFind.Point endPoint = new PathFind.Point((int)end.x, (int)end.y);

        List<PathFind.Point> path = PathFind.Pathfinding.FindPath(Instance.grid, startPoint, endPoint);
        Vector3[] points = new Vector3[path.Count];

        for (int i = 0; i < path.Count; i++)
        {
            points[i] = new Vector3(path[i].x + 0.5f, path[i].y + 0.5f, 0);
        }
        return points;
    }
    
    private void OnDrawGizmos()
    {
        if(!debug)
        {
            return;
        }

        if(path != null)
        {
            Gizmos.color = Color.red;
            foreach (var item in path)
            {
                Gizmos.DrawSphere(new Vector3(item.x + 0.5f, item.y + 0.5f, 0), 0.2f);
            }
        }
        if(grid != null)
        {
            foreach (PathFind.Node item in grid.nodes)
            {
                Gizmos.color = Color.white;
                if(!item.walkable)
                {
                    Gizmos.color = Color.black;
                }
                Gizmos.DrawSphere(new Vector3(item.gridX + 0.5f, item.gridY + 0.5f, 0), 0.1f);
            }
        }
    }

    public static Vector2 GetRandomPosition()
    {
        int randomIndex = Random.Range(0, Instance.walkablePoints.Count);
        return Instance.walkablePoints[randomIndex];
    }
}
