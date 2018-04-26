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
        if(building != null)
            foreach (Vector3 point in building.Points)
            {
                //Gizmos.DrawIcon(point, "Point " + building.Points.FindIndex(x => x == point), true);
                if(building.Debug)
                    Gizmos.DrawSphere(point, 0.5f);
            }
    }
}
