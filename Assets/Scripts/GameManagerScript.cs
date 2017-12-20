using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public Transform Chaser;
    private Vector2 chaserPointLocation;

    List<Vector2> accessiblePoints = new List<Vector2>();

	// Use this for initialization
	void Start () {
        accessiblePoints = PathfindingScript.traversablePoints;
	}

    Vector2 GetNearestPoint(Vector2 pos)
    {
        float y = Mathf.Round(pos.y);
        float rounded = Mathf.Round(pos.x / 1.5f);
        float x  = rounded * 1.5f;
        //float x = Mathf.Clamp(pos.x, float min = )
        return new Vector2(x , y);
    }
	

    void SetChaserGridPos()
    {
        Vector2 chaserCheckV = GetNearestPoint(Chaser.position);
        if (accessiblePoints.Contains(chaserCheckV))
            chaserPointLocation = GetNearestPoint(Chaser.position);
    }

    public Vector2 GetChaserGridPos()
    {
        return chaserPointLocation;
    }

	// Update is called once per frame
	void Update () {
        SetChaserGridPos();
    }
}
