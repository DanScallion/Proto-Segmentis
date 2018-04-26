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
        [SerializeField] private List<Wall> _wall;
        [SerializeField] private string _roomName;
        [SerializeField] private float _centerPoint;

        public Room(string roomName="Room")
        {
            this.UniqueID = IDgenerator.CreateID();
            this.RoomName = roomName;
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

        public float CenterPoint
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
    }


}