using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WallPlayModeTest {

	[Test]
	public void NewPlayModeTestSimplePasses() {
		
	}

	[UnityTest]
	public IEnumerator CheckWallBuildRuntime() {
        Camera camera = new Camera();

        List<WallSegmentType> segmentType = new WallSegmentType[] {
            WallSegmentType.Window,
            WallSegmentType.Plain,
            WallSegmentType.Door,
            WallSegmentType.Plain,
            WallSegmentType.Window,
        }.ToList();
        Vector3 position = Vector3.zero;
        Vector3 rotation = Vector3.zero;
        Vector3 size = new Vector3(12, 2.4f, 0.1f);
        string name = "Test Wall";
        Vector2 window = new Vector2();
        Vector2 door = new Vector2();

        GameObject wall = new Segmentis.Wall(size, position, rotation, name, window, door, segmentType).Build();

        Assert.IsNotNull(wall);
        Assert.AreEqual(name, wall.name);

        WallInterface wallInterface = wall.GetComponent<WallInterface>();
        Segmentis.Wall wallObj = wallInterface.wallObj;

        Assert.AreEqual(position, wallObj.WallPosition);
        Assert.AreEqual(rotation, wallObj.WallRotation);
        Assert.AreEqual(size, wallObj.WallSize);
        Assert.AreEqual(window, wallObj.WindowSize);
        Assert.AreEqual(door, wallObj.DoorSize);
        Assert.AreEqual(false, wallObj.ForceName);
        Assert.AreEqual(true, wallObj.ForcePosition);


        yield return null;
    }
}
