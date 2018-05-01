using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Segmentis
{
    [System.Serializable]
    public class Room
    {
        [SerializeField] private string _uniqueID;
        [SerializeField] private List<Wall> _wall = new List<Wall>();
        [SerializeField] private string _roomName;
        [SerializeField] private List<Vector3> _points = new List<Vector3>();
        [SerializeField] private Vector3 _centerPoint;
        [SerializeField] private List<int> _wallPointOrder = new List<int>();
        [SerializeField] private List<WallSegmentType> _segmentTypeByOrder = new List<WallSegmentType>();
        [SerializeField] private float _wallHeight;
        [SerializeField] private float _wallThickness;

        public Room(float wallHeight=2.4f, float wallThickness=0.1f, string roomName = "Room")
        {
            this.UniqueID = IDgenerator.CreateID();
            this.RoomName = roomName;
            this.WallHeight = wallHeight;
            this.WallThickness = wallThickness;
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

        public List<Wall> Wall
        {
            get
            {
                return _wall;
            }

            set
            {
                _wall = value;
            }
        }

        public string RoomName
        {
            get
            {
                return _roomName;
            }

            set
            {
                _roomName = value;
            }
        }

        public Vector3 CenterPoint
        {
            get
            {
                return _centerPoint;
            }

            private set
            {
                _centerPoint = value;
            }
        }

        public List<int> WallPointOrder
        {
            get
            {
                return _wallPointOrder;
            }

            set
            {
                _wallPointOrder = value;
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

        public List<WallSegmentType> SegmentTypeByOrder
        {
            get
            {
                return _segmentTypeByOrder;
            }

            set
            {
                _segmentTypeByOrder = value;
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

        public void CalculateCenterPoint()
        {
            for(int i = 0; i < Points.Count; i++)
            {
                CenterPoint += Points[i];
            }
            CenterPoint = CenterPoint / Points.Count;
        }

        public void Build(GameObject obj)
        {
            for(int i = 0; i < Points.Count; i++)
            {
                Vector3 wallSize;
                Vector3 wallRotation;

                if (Points[i] != Points.Last())
                {
                    wallSize = new Vector3(Vector3.Distance(Points[i], Points[i+1]), WallHeight , WallThickness);
                    wallRotation = new Vector3(0,0,0);
                }
                else
                {
                    wallSize = new Vector3(Vector3.Distance(Points[i], Points.First()), WallHeight, WallThickness);
                    wallRotation = new Vector3(0, 0, 0);
                }

                Debug.Log(wallSize);
                Wall wall = new Wall(wallSize, wallRotation, Points[i]);
                GameObject wallObj = wall.Build();
                wallObj.transform.parent = obj.transform;
                Wall.Add(wall);

            }
        }

        /*private Vector3 CalculateWallRotation(Vector3 p1Pos, Vector3 p2Pos)
        {
            Vector3 rotation;

            if(p1Pos.x != p2Pos.x)
                rotation = new Vector3(0, 0, 0);


            return rotation;
        }*/
    }
}