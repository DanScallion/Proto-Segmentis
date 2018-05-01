using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Segmentis
{
    [System.Serializable]
    public class Building
    {
        [SerializeField] private string _uniqueID;
        [SerializeField] private float _wallHeight = 2.4f;
        [SerializeField] private float _wallThickness = 0.1f;
        [SerializeField] private List<Room> _room = new List<Room>();
        [SerializeField] private string _buildingName;
        [SerializeField] private int _floorNum;
        [SerializeField] private List<int> _roomsPerFloor = new List<int>();
        [SerializeField] private List<Vector3> _points = new List<Vector3>();
        [SerializeField] private bool _debug = true;
        [System.NonSerialized()] private GameObject _buildingGameObject;

        public Building(string buildingName="Building", int floorNum = 1)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.BuildingName = buildingName;
            this.Points = new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(10, 0, 0),
            new Vector3(0, 0, 10),
            new Vector3(10, 0, 10),
            new Vector3(0, 0, -10),
            new Vector3(10, 0, -10),
            }.ToList();
            this.RoomsPerFloor.Add(1);
            this.FloorNum = floorNum;
        }

        public string UniqueID
        {
            get
            {
                return _uniqueID;
            }

            private set
            {
                _uniqueID = value;
            }
        }

        public List<Room> Room
        {
            get
            {
                return _room;
            }

            set
            {
                _room = value;
            }
        }

        public string BuildingName
        {
            get
            {
                return _buildingName;
            }

            set
            {
                _buildingName = value;
            }
        }

        public int FloorNum
        {
            get
            {
                return _floorNum;
            }

            set
            {
                _floorNum = value;
            }
        }

        public List<int> RoomsPerFloor
        {
            get
            {
                return _roomsPerFloor;
            }

            set
            {
                _roomsPerFloor = value;
            }
        }

        public List<Vector3> Points
        {
            get
            {
                return _points;
            }

            set
            {
                _points = value;
            }
        }

        public bool Debug
        {
            get
            {
                return _debug;
            }

            set
            {
                _debug = value;
            }
        }

        public GameObject BuildingGameObject
        {
            get
            {
                return _buildingGameObject;
            }

            set
            {
                _buildingGameObject = value;
            }
        }

        public float WallHeight
        {
            get
            {
                return _wallHeight;
            }

            set
            {
                _wallHeight = value;
            }
        }

        public float WallThickness
        {
            get
            {
                return _wallThickness;
            }

            set
            {
                _wallThickness = value;
            }
        }

        public GameObject Build()
        {
            GameObject obj = new GameObject();
            obj.name = BuildingName;
            BuildingInterface buildingInterface = obj.AddComponent<BuildingInterface>();
            BuildingGameObject = obj;
            buildingInterface.building = this;

            for (int i = 0; i < FloorNum; i++)
            {
                for (int j = 0; j < RoomsPerFloor[i]; j++)
                {
                    Room room = new Room(WallHeight, WallThickness);

                    room.Points.Add(Points[0]);
                    room.WallPointOrder.Add(0);
                    room.Points.Add(Points[1]);
                    room.WallPointOrder.Add(1);
                    room.Points.Add(Points[2]);
                    room.WallPointOrder.Add(2);
                    room.Points.Add(Points[3]);
                    room.WallPointOrder.Add(3);

                    room.CalculateCenterPoint();

                    room.Build(obj);

                    Room.Add(room);
                }
            }

            return obj;
        }
    }
}