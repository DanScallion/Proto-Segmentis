using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInterface : MonoBehaviour {
    public Segmentis.Building building;

	void Start () {

	}
	
	void Update () {
		
	}

    void OnDrawGizmosSelected()
    {
        if (building != null && building.Debug)
        {
            foreach (Vector3 point in building.Points)
            {
                //Gizmos.DrawIcon(point, "Point " + building.Points.FindIndex(x => x == point), true);
                Gizmos.DrawSphere(point, 0.5f);
            }

            foreach (Segmentis.Room room in building.Room)
            {
                Gizmos.DrawCube(room.CenterPoint, Vector3.one/2);
            }
        }
    }

    public void Build()
    {
        building.Build();
    }
}
