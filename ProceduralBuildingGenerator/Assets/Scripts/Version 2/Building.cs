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
        [SerializeField] private List<Room> _room;
        [SerializeField] private string _buildingName;
        [SerializeField] private int _floorNum;
        [SerializeField] private List<int> _roomsPerFloor;
        [SerializeField] private List<Vector3> _points = new List<Vector3>();
        [SerializeField] private bool _debug = false;
        [System.NonSerialized()] private GameObject _buildingGameObject;

        public Building(string buildingName="Building")
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

        public GameObject Build()
        {
            GameObject obj = new GameObject();
            obj.name = BuildingName;
            BuildingInterface buildingInterface = obj.AddComponent<BuildingInterface>();
            BuildingGameObject = obj;
            buildingInterface.building = this;

            return obj;
        }
    }
}