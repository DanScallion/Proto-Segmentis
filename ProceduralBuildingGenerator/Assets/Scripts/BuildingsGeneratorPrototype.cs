using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsGeneratorPrototype : MonoBehaviour {
    public GameObject wallModel;
    public GameObject doorModel;
    public GameObject windowModel;
    public GameObject floorModel;
    public int floorNum = 0;

    public float wallHeightMin = 0.00f;
    public float wallHeighMax = 0.00f;
    public float wallWidthMin = 0.00f;
    public float wallWidthMax = 0.00f;

    /*public float doorHeightMin = 0.00f;
    public float doorHeighMax = 0.00f;
    public float doorWidthMin = 0.00f;
    public float doorWidthMax = 0.00f;

    public float windowHeightMin = 0.00f;
    public float windowHeighMax = 0.00f;
    public float windowWidthMin = 0.00f;
    public float windowWidthMax = 0.00f;*/

    // Use this for initialization
    void Start () {
        /*for(int i = 1; i <= 20; i++)
        {
            CreateWall(new Vector3(Random.Range(wallWidthMin, wallWidthMax), Random.Range(wallHeightMin, wallHeighMax), 0.1f), new Vector3(0, 0, i*2), ("Generated Wall " + i));
        }*/
        CreateBuilding(new Vector3(0,0,0) , "Building 1", floorNum);
    }
	
	// Update is called once per frame
	void Update () {

		
	}

    public GameObject CreateWall(Vector3 size, Vector3 position, Vector3 rotation, string name)
    {
        GameObject wall = Instantiate(wallModel, new Vector3(0, 0, 0), Quaternion.Euler(rotation.x, rotation.y, rotation.z));
        wall.name = name;
        wall.transform.localScale = size;
        wall.transform.position = position;

        return wall;
    }

    public GameObject CreateDoor(Vector3 size, Vector3 position, string name)
    {
        GameObject door = Instantiate(doorModel, new Vector3(0, 0, 0), Quaternion.identity);
        door.name = name;
        door.transform.localScale = size;
        door.transform.position = position;

        return door;
    }

    public GameObject CreateWindow(Vector3 size, Vector3 position, string name)
    {
        GameObject window = Instantiate(windowModel, new Vector3(0, 0, 0), Quaternion.identity);
        window.name = name;
        window.transform.localScale = size;
        window.transform.position = position;

        return window;
    }

    public ArrayList CreateRoom(Vector3 position, string name)
    {
        float ww = Mathf.Round(Random.Range(wallWidthMin, wallWidthMax));
        float wh = Mathf.Round(Random.Range(wallHeightMin, wallHeighMax));

        //GameObject[] room = new GameObject[9];
        ArrayList room = new ArrayList();


        /*room[0] = CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(0, 0, 0), new Vector3(0, 0, 0), ("Generated Wall 1"));
        room[1] = CreateWall(new Vector3(ww, wh, 0.1f), new Vector3((ww / 2) - 0.05f, 0, ww / 2), new Vector3(0, 90, 0), ("Generated Wall 2"));
        room[2] = CreateWall(new Vector3(ww, wh, 0.1f), new Vector3((-ww / 2) + 0.05f, 0, ww / 2), new Vector3(0, 90, 0), ("Generated Wall 3"));
        room[3] = CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(0, 0, ww), new Vector3(0, 0, 0), ("Generated Wall 4"));
        room[4] = CreateDoor(new Vector3(1 , wh * 0.75f, 0.11f), new Vector3(0, -wh * 0.125f, 0), "Entrance Door");
        room[5] = CreateWindow(new Vector3(wh * 0.35f, wh * 0.35f, 0.12f), new Vector3(Random.Range(((-ww / 2) + 0.05f) + (wh * 0.35f), ((ww / 2) - 0.05f) - (wh * 0.35f)), Random.Range(0 + (wh * 0.35f), (wh / 2) - (wh * 0.35f)), 0), "Window 1");
        print((wh/2) - (wh * 0.35f));
        room[6] = CreateFloor(new Vector3(ww - 0.01f, 0.1f, ww - 0.01f), new Vector3(0,-wh/2,ww/2), "Generated Floor 1");
        room[7] = CreateFloor(new Vector3(ww - 0.01f, 0.1f, ww - 0.01f), new Vector3(0, wh / 2, ww / 2), "Generated Ceiling 1");*/

        room.Add(CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(position.x + 0, position.y + 0, position.z +0), new Vector3(0, 0, 0), ("Generated Wall 1")));
        room.Add(CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(position.x + (ww / 2) - 0.05f, position.y + 0, position.z + (ww / 2)), new Vector3(0, 90, 0), ("Generated Wall 2")));
        room.Add(CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(position.x + (-ww / 2) + 0.05f, position.y + 0, position.z + (ww / 2)), new Vector3(0, 90, 0), ("Generated Wall 3")));
        room.Add(CreateWall(new Vector3(ww, wh, 0.1f), new Vector3(position.x + 0, position.y + 0, position.z + ww), new Vector3(0, 0, 0), ("Generated Wall 4")));
        room.Add(CreateFloor(new Vector3(ww - 0.01f, 0.1f, ww - 0.01f), new Vector3(position.x + 0, position.y + (-wh / 2), position.z + (ww / 2)), "Generated Floor 1"));
        room.Add(CreateFloor(new Vector3(ww - 0.01f, 0.1f, ww - 0.01f), new Vector3(position.x + 0, position.y + (wh / 2), position.z + (ww / 2)), "Generated Ceiling 1"));

        return room;
    }

    public ArrayList CreateFloor(Vector3 position, string name)
    {
        ArrayList floor = new ArrayList();
        floor.Add(CreateRoom(new Vector3(0, position.y, 0), "Room 1"));
        floor.Add(CreateRoom(new Vector3(10, position.y, 0), "Room 2"));
        //floor.Add(CreateCeiling(new Vector3(0,0,0), new Vector3(0, 0, 0), "floor"));

        return floor;
    }

    public ArrayList CreateBuilding(Vector3 position, string name, int floorNum)
    {
        ArrayList building = new ArrayList();
        int i = 0;
        
        while(i < floorNum)
        {
           building.Add(CreateFloor(new Vector3(0,0 + (i*3),0), "Floor " + floorNum));
           
           i++;
        }            
        
        return building;
    }
    
    public GameObject CreateFloor(Vector3 size, Vector3 position, string name)
    {
        GameObject floor = Instantiate(floorModel, new Vector3(0, 0, 0), Quaternion.identity);
        floor.name = name;
        floor.transform.localScale = size;
        floor.transform.position = position;

        return floor;
    }

    public GameObject CreateCeiling(Vector3 size, Vector3 position, string name)
    {
        GameObject ceiling = Instantiate(floorModel, new Vector3(0, 0, 0), Quaternion.identity);
        ceiling.name = name;
        ceiling.transform.localScale = size;
        ceiling.transform.position = position;

        return ceiling;
    }

}
