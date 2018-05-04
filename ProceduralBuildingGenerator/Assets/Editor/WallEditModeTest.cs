using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class WallEditModeTest {

	[Test]
	public void WallEditModeTestSimplePasses() {
		
	}

    [UnityTest]
    public IEnumerator CheckWallBuildEditor()
    {
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
        Vector2 window = new Vector2(1,1);
        Vector2 door = new Vector2(2,1);

        GameObject wall = new Segmentis.Wall(size, position , rotation, name, window, door, segmentType).Build();

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
        Assert.AreEqual(segmentType, wallObj.SegmentType);

        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckWallRebuildEditor()
    {
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
        Vector2 window = new Vector2(1, 1);
        Vector2 door = new Vector2(2, 1);

        GameObject wall = new Segmentis.Wall(size, position, rotation, name, window, door, segmentType).Build();
        WallInterface wallInterface = wall.GetComponent<WallInterface>();
        Segmentis.Wall wallObj = wallInterface.wallObj;

        Assert.IsNotNull(wall);
        Assert.AreEqual(name, wall.name);
        Assert.AreEqual(position, wallObj.WallPosition);
        Assert.AreEqual(rotation, wallObj.WallRotation);
        Assert.AreEqual(size, wallObj.WallSize);
        Assert.AreEqual(window, wallObj.WindowSize);
        Assert.AreEqual(door, wallObj.DoorSize);
        Assert.AreEqual(false, wallObj.ForceName);
        Assert.AreEqual(true, wallObj.ForcePosition);
        Assert.AreEqual(segmentType, wallObj.SegmentType);

        List<WallSegmentType> newSegmentType = new WallSegmentType[] {
            WallSegmentType.Window,
            WallSegmentType.Plain,
            WallSegmentType.Window,
            WallSegmentType.Door,
            WallSegmentType.Window,
            WallSegmentType.Plain,
            WallSegmentType.Window,
        }.ToList();
        Vector3 newSize = new Vector3(24, 2.4f, 0.2f);
        Vector3 newRotation = new Vector3(0, 90, 0);
        Vector3 newPosition = new Vector3(25, 2.4f, 25);
        string newName = "Wall";
        Vector2 newWindow = new Vector2(0.5f, 0.5f);
        Vector2 newDoor = new Vector2(1.8f, 1.5f);

        wallObj.WallSize = newSize;
        wallObj.WallRotation = newRotation;
        wallObj.WallName = newName;
        wallObj.SegmentType = newSegmentType;
        wallObj.WindowSize = newWindow;
        wallObj.DoorSize = newDoor;
        wallObj.WallPosition = newPosition;
        wallObj.SegmentType = newSegmentType;

        wallObj.ReBuild();

        Assert.IsNotNull(wall);
        Assert.AreEqual(name, wall.name);
        Assert.AreEqual(newPosition, wallObj.WallPosition);
        Assert.AreEqual(newRotation, wallObj.WallRotation);
        Assert.AreEqual(newSize, wallObj.WallSize);
        Assert.AreEqual(newWindow, wallObj.WindowSize);
        Assert.AreEqual(newDoor, wallObj.DoorSize);
        Assert.AreEqual(false, wallObj.ForceName);
        Assert.AreEqual(true, wallObj.ForcePosition);
        Assert.AreEqual(newSegmentType, wallObj.SegmentType);

        yield return null;
    }
 }
