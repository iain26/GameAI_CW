using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour {

    public List<Vector2> traversablePoints = new List<Vector2>();
    public GameObject grid;

    public Vector2 startPoint;
    public Vector2 endPoint;

    Dictionary<Vector2, Vector2> parents = new Dictionary<Vector2, Vector2>();
    Dictionary<Vector2, float> g = new Dictionary<Vector2, float>();
    Dictionary<Vector2, float> h = new Dictionary<Vector2, float>();
    Dictionary<Vector2, float> f = new Dictionary<Vector2, float>();

    public enum PathfindSearch { dStar, aStar};

    public PathfindSearch pathfindSearch = PathfindSearch.aStar;
    
    public List<Vector2> pathCheck = new List<Vector2>();

    // Use this for initialization
    void Start () {
        int iGridCells = grid.transform.childCount;
        for (int count = 0; count < iGridCells; count++)
        {
            if (!grid.transform.GetChild(count).gameObject.activeInHierarchy)
                traversablePoints.Add(grid.transform.GetChild(count).position);
        }
        startPoint = traversablePoints[0];
        endPoint = traversablePoints[5];

        switch (pathfindSearch)
        {
            case PathfindSearch.aStar:
                pathCheck = AStarSearch(startPoint, endPoint);
                break;
            case PathfindSearch.dStar:
                break;
        }

        //print(path)
    }

    protected List<Vector2> BackTrackPath(Vector2 end)
    {
        List<Vector2> path = new List<Vector2>();
        if (end != null)
        {
            path.Add(end);

            while (parents[end] != startPoint)
            {
                path.Add(parents[end]);
                end = parents[end];
            }

            // Reverse the path so the start node is at index 0
            path.Reverse();
        }
        return path;
    }


    List<Vector2> CheckNeighbouringPoints(Vector2 pointToCheck)
    {
        List<Vector2> neighbours = new List<Vector2>();
        for (int y = -1; y < 2; y++)
        {
            for (int x = -1; x < 2; x++)
            {
                if (traversablePoints.Contains((pointToCheck + new Vector2(1.5f*x, 1f*y))))
                {
                    if(x == 0 && y == 0)
                    {
                        continue;
                    }
                    neighbours.Add(pointToCheck + new Vector2(1.5f * x, 1f * y));
                }
            }
        }
        return neighbours;
    }

    List<Vector2> AStarSearch(Vector2 start, Vector2 end)
    {
        List<Vector2> openList = new List<Vector2>();
        List<Vector2> closedList = new List<Vector2>();

        openList.Add(start);

        while (openList.Count > 0)
        {

            openList.Sort();
            Vector2 current = openList[0];

            openList.Remove(current);
            closedList.Add(current);

            if (current == end)
            {
                return BackTrackPath(end);
            }

            List<Vector2> traversableNeighbours = new List<Vector2>();
            traversableNeighbours = CheckNeighbouringPoints(current);

            int numNeighbours = traversableNeighbours.Count;

            float fConsistent = float.MaxValue;
            int fIndex = 0;
            for (int neighbourCurrentIndex = 0; neighbourCurrentIndex < numNeighbours; ++neighbourCurrentIndex)
            {
                Vector2 neighbourCurrent = traversableNeighbours[neighbourCurrentIndex];
                if (closedList.Contains(neighbourCurrent))
                {
                    continue;
                }
                //cost is distance as terrain is standard
                float gCostToNeigbour = Vector2.Distance(current, neighbourCurrent);
                float hEstCostToEnd = Vector2.Distance(neighbourCurrent, end);
                if (fConsistent < gCostToNeigbour + hEstCostToEnd || !(openList.Contains(neighbourCurrent)))
                {
                    fConsistent = gCostToNeigbour + hEstCostToEnd;

                    //    neighbourCurrent.g = g;
                    //    connectedNode.f = g;
                    //    connectedNode.h = EuclideanDistanceHeuristic(connectedNode.x, connectedNode.y, endNode.x, endNode.y);
                    //    connectedNode.f += connectedNode.h;

                    fIndex++;
                    //}
                    //float fConsistent = gCostToNeigbour + hEstCostToEnd;
                    //if (f <= (connectedNode.f) || !(openList.Contains(connectedNode)))
                    //{
                    //    connectedNode.g = g;
                    //    connectedNode.f = g;
                    //    connectedNode.h = EuclideanDistanceHeuristic(connectedNode.x, connectedNode.y, endNode.x, endNode.y);
                    //    connectedNode.f += connectedNode.h;
                    //    //finish Euclidean heuristic here^
                }
                if (!(openList.Contains(traversableNeighbours[fIndex])))
                {
                    //connectedNode.parent = current;
                    //parents[neighbourCurrent] = current;
                    parents.Add(neighbourCurrent, current);
                    openList.Add(neighbourCurrent);
                }
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        //for (int y = 1; y < 16; y++)
        //{
        //    for (int x = 1; x < 16; x++)
        //    {
        //        GameObject.Find("Cell x : " + x.ToString() + " y: " + y.ToString()).gameObject.transform.localPosition = new Vector3(1.5f * (x - 8), 1 * (y - 8));
        //    }
        //}
    }

    private float EuclideanDistanceHeuristic(float currentX, float currentY, float targetX, float targetY)
    {

        float xDist = Mathf.Abs(currentX - targetX);
        float yDist = Mathf.Abs(currentY - targetY);

        return 10 * Mathf.Sqrt((xDist * xDist + yDist * yDist) * (xDist * xDist + yDist * yDist));
    }
}
