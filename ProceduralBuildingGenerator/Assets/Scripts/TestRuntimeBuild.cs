using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestRuntimeBuild : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List <WallSegmentType> segmentType = new WallSegmentType[] {
            WallSegmentType.Window,
            WallSegmentType.Plain,
            WallSegmentType.Door,
            WallSegmentType.Plain,
            WallSegmentType.Window,
        }.ToList();


        new Segmentis.Wall(new Vector3(12, 2.4f, 0.1f), Vector3.zero, Vector3.zero, segmentType).Build();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
