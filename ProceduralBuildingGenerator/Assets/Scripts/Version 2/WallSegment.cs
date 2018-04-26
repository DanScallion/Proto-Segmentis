using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Segmentis
{
    [System.Serializable]
    public class WallSegment
    {
        [SerializeField] private Vector3 _segmentSize;
        [SerializeField] private Vector3 _segmentPosition;
        [SerializeField] private WallSegmentType _segmentType;
        [SerializeField] private Vector2 _windowSize;
        [SerializeField] private Vector2 _doorSize;
        [SerializeField] private string _segmentName;

        public WallSegment() : this(new Vector3(1, 2.4f, 0.1f), new Vector3(0, 0, 0), WallSegmentType.Plain) { }

        public WallSegment(Vector3 segmentSize, Vector3 segmentPosition, WallSegmentType segmentType) : this(segmentSize, segmentPosition, segmentType, "Segment") { }

        public WallSegment(Vector3 segmentSize, Vector3 segmentPosition, WallSegmentType segmentType, string name)
        {
            this.SegmentSize = segmentSize;
            this.SegmentPosition = segmentPosition;
            this.SegmentType = segmentType;
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentName = name;
        }

        public WallSegment(Vector3 segmentSize, Vector3 segmentPosition, WallSegmentType segmentType, Vector2 objectSize) : this(segmentSize, segmentPosition, segmentType, objectSize, "Segment") { }

        public WallSegment(Vector3 segmentSize, Vector3 segmentPosition, WallSegmentType segmentType, Vector2 objectSize, string name)
        {
            this.SegmentSize = segmentSize;
            this.SegmentPosition = segmentPosition;
            this.SegmentType = segmentType;

            if (segmentType == WallSegmentType.Window)
                this.WindowSize = objectSize;
            else if (segmentType == WallSegmentType.Door)
                this.DoorSize = objectSize;
            else
            {
                this.WindowSize = objectSize;
                this.DoorSize = objectSize;
            }

            this.SegmentName = name;
        }

        public Vector3 SegmentSize
        {
            get
            {
                return _segmentSize;
            }

            set
            {
                if (value.x > 0 && value.y > 0)
                {
                    _segmentSize = value;
                    WindowSize = WindowSize;
                    DoorSize = DoorSize;
                }
                else if(value.x <= 0 && value.y > 0)
                    SegmentSize = new Vector2(1, value.y);
                else if (value.x > 0 && value.y <= 0)
                    SegmentSize = new Vector2(value.x, 2.4f);
                else
                {
                    SegmentSize = new Vector2(1, 2.4f);
                }
            }
        }

        public Vector3 SegmentPosition
        {
            get
            {
                return _segmentPosition;
            }

            set
            {
                _segmentPosition = value;
            }
        }

        public WallSegmentType SegmentType
        {
            get
            {
                return _segmentType;
            }

            set
            {
                _segmentType = value;
            }
        }

        public Vector2 WindowSize
        {
            get
            {
                return _windowSize;
            }

            set
            {
                if (value.x > 0 && value.y > 0)
                    if (value.x < SegmentSize.x && value.y < SegmentSize.y)
                        _windowSize = value;
                    else if (value.x > SegmentSize.x && value.y < SegmentSize.y)
                        _windowSize = new Vector2(SegmentSize.x / 1.1f, value.y);
                    else if (value.x < SegmentSize.x && value.y > SegmentSize.y)
                        _windowSize = new Vector2(value.x, SegmentSize.y / 1.1f);
                    else
                        _windowSize = SegmentSize / 1.1f;
                else
                    WindowSize = new Vector2(1, 1);
            }
        }

        public Vector2 DoorSize
        {
            get
            {
                return _doorSize;
            }

            set
            {
                if (value.x > 0 && value.y > 0)
                    if (value.x < SegmentSize.x && value.y < SegmentSize.y)
                        _doorSize = value;
                    else if (value.x > SegmentSize.x && value.y < SegmentSize.y)
                        _doorSize = new Vector2(SegmentSize.x / 1.1f, value.y);
                    else if (value.x < SegmentSize.x && value.y > SegmentSize.y)
                        _doorSize = new Vector2(value.x, SegmentSize.y / 1.1f);
                    else
                        _doorSize = SegmentSize / 1.1f;
                else
                    DoorSize = new Vector2(1, 2);
            }
        }

        public string SegmentName
        {
            get
            {
                return _segmentName;
            }

            set
            {
                _segmentName = value;
            }
        }

        public Vector3[] CalculateVertices()
        {
            switch (SegmentType)
            {
                default:
                case WallSegmentType.Plain:
                    {
                        Vector3[] p = new Vector3[]
                        {
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //front
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z),
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z),
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z),
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //back
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z),
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z),
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z)
                        };

                        Vector3[] vertices = new Vector3[]
                        {
                        //front
                        p[0],
                        p[1],
                        p[2],
                        p[3],

                        //back
                        p[4],
                        p[5],
                        p[6],
                        p[7],
                        };

                        return vertices;
                    }
                case WallSegmentType.Door:
                    {
                        Vector3[] p = new Vector3[]
                        {
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //0 front
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //1 door
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //2
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //3 door
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //4
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //5 door
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //6
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //7 door
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //8 door hole
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //9 door hole
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //10 door hole
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f)+ SegmentPosition.z), //11 door hole
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //12 back
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //13 door
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //14
                        new Vector3((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //15 door
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //16
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //17 door
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //18
                        new Vector3((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //19 door
                        };

                        Vector3[] vertices = new Vector3[]
                        {
                        //front
                        p[0],
                        p[1],
                        p[2],
                        p[3],
                        p[4],
                        p[5],
                        p[6],
                        p[7],

                        //door right
                        p[7], //8
                        p[8], //9
                        p[9], //10
                        p[5], //11

                        //door top
                        p[5], //12
                        p[9], //13
                        p[10], //14
                        p[3], //15

                        //door left
                        p[3], //16
                        p[10], //17
                        p[11], //18
                        p[1], //19

                        //back
                        p[12], //20
                        p[13], //21
                        p[14], //22
                        p[15], //23
                        p[16], //24
                        p[17], //25
                        p[18], //26
                        p[19], //27
                        };

                        return vertices;
                    }
                case WallSegmentType.Window:
                    {
                        Vector3[] p = new Vector3[]
                        {
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //0 front
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //1 window
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //2
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //3 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //4
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //5 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //6
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //7 window
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //8 windowsill
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //9
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //10
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //11
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //12 back
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //13 window
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //14
                        new Vector3((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //15 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //16
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //17 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //18
                        new Vector3((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //19 window
                        };

                        Vector3[] vertices = new Vector3[]
                        {
                        //front
                        p[0],
                        p[1],
                        p[2],
                        p[3],
                        p[4],
                        p[5],
                        p[6],
                        p[7],
                        
                        //windowsill bottom
                        p[7], //8
                        p[8], //9
                        p[11], //10
                        p[1], //11

                        //windowsill left
                        p[1], //12
                        p[11], //13
                        p[10], //14
                        p[3], //15

                        //windowsill top
                        p[3], //16
                        p[10], //17
                        p[9], //18
                        p[5], //19

                        //windowsill right
                        p[5], //20
                        p[9], //21
                        p[8], //22
                        p[7], //23

                        //back
                        p[12], //24
                        p[13], //25
                        p[14], //26
                        p[15], //27
                        p[16], //28
                        p[17], //29
                        p[18], //30
                        p[19], //31
                        };
                        return vertices;
                    }
                case WallSegmentType.FancyWindow1:
                    {
                        Vector3[] p = new Vector3[]
                        {
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //0 front
                        new Vector3((-WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //1 window
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //2
                        new Vector3((-WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //3 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //4
                        new Vector3((WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //5 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //6
                        new Vector3((WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y, (-SegmentSize.z * 0.5f) + SegmentPosition.z), //7 window
                        new Vector3((SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //8 windowsill
                        new Vector3((SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //9
                        new Vector3((-SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //10
                        new Vector3((-SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //11
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //12 back
                        new Vector3((-SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //13 window
                        new Vector3((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //14
                        new Vector3((-SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //15 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //16
                        new Vector3((SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //17 window
                        new Vector3((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //18
                        new Vector3((SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y, (SegmentSize.z * 0.5f) + SegmentPosition.z), //19 window
                        };

                        Vector3[] vertices = new Vector3[]
                        {
                        //front
                        p[0],
                        p[1],
                        p[2],
                        p[3],
                        p[4],
                        p[5],
                        p[6],
                        p[7],
                        
                        //windowsill bottom
                        p[7], //8
                        p[8], //9
                        p[11], //10
                        p[1], //11

                        //windowsill left
                        p[1], //12
                        p[11], //13
                        p[10], //14
                        p[3], //15

                        //windowsill top
                        p[3], //16
                        p[10], //17
                        p[9], //18
                        p[5], //19

                        //windowsill right
                        p[5], //20
                        p[9], //21
                        p[8], //22
                        p[7], //23

                        //back
                        p[12], //24
                        p[13], //25
                        p[14], //26
                        p[15], //27
                        p[16], //28
                        p[17], //29
                        p[18], //30
                        p[19], //31
                        };
                        return vertices;
                    }
            }
        }

        public int[] CalculateTris()
        {

            switch (SegmentType)
            {
                default:
                case WallSegmentType.Plain:
                    {
                        int[] tris = new int[]
                        {
                        2, 1, 0,    2, 0, 3, //front
                        4, 7, 6,    6, 5, 4, //back
                        };

                        return tris;
                    }
                case WallSegmentType.Door:
                    {
                        int[] tris = new int[]
                        {
                        //front
                        4, 6, 5,
                        6, 7, 5,
                        5, 3, 4,
                        3, 2, 4,
                        2, 3, 1,
                        1, 0, 2,
                        
                        //door right
                        8, 9, 11,
                        9, 10, 11,

                        //door top
                        15, 12, 14,
                        12, 13, 14,

                        //door left
                        19, 17, 18,
                        17, 19, 16,
                        
                        //back
                        22, 21, 23,
                        21, 22, 20,
                        22, 23, 24,
                        23, 25, 24,
                        25, 26, 24,
                        26, 25, 27,

                        };

                        return tris;
                    }
                case WallSegmentType.FancyWindow1:
                case WallSegmentType.Window:
                    {
                        int[] tris =
                            {
                        //front
                        4, 6, 5,
                        4, 5, 3,
                        3, 2, 4,
                        3, 1, 2,
                        1, 0, 2,
                        1, 7, 0,
                        7, 6, 0,
                        7, 5, 6,
                        
                        //windowsill bottom
                        9, 8, 11,
                        11, 10, 9, 
                        
                        //windowsill left
                        14, 13, 12,
                        12, 15, 14,

                        //windowsill top
                        18, 17, 16,
                        16, 19, 18,

                        //windowsill right
                        21, 20, 23,
                        23, 22, 21,
                        
                        //back
                        26, 24, 27,
                        26, 27, 29,
                        29, 28, 26,
                        29, 31, 28,
                        31, 30, 28,
                        31, 25, 30,
                        25, 24, 30,
                        25, 27, 24
                    };

                        return tris;
                    }
            }
        }

        public Vector2[] CalculateUV()
        {
            //Vector3[] vertices = CalculateVertices();

            switch (SegmentType)
            {
                default:
                case WallSegmentType.Plain:
                    {
                        Vector2[] uvs = new Vector2[]
                        {
                        //front
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x , (SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y),
                        //back
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y),
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y)
                        };

                        return uvs;
                    }
                case WallSegmentType.Door:
                    {
                        Vector2[] uvs = new Vector2[]
                        {
                        //front
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //0 front
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //1 door
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //2
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //3 door
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //4
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //5 door
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //6
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //7 door*

                        //door right
                        new Vector2((DoorSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.z + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //7 door
                        new Vector2((DoorSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.z + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //8 door hole
                        new Vector2((DoorSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.z + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //9 door hole
                        new Vector2((DoorSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.z + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //5 door

                        //door top
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //5 door
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //9 door hole y
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //10 door hole y
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //3 door

                        //door left
                        new Vector2((-DoorSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //3 door x
                        new Vector2((-DoorSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //10 door hole
                        new Vector2((-DoorSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //11 door hole
                        new Vector2((-DoorSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //1 door x

                        //back
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //12 back
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //13 door
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //14
                        new Vector2((-DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //15 door
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //16
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + (DoorSize.y) + SegmentPosition.y), //17 door
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //18
                        new Vector2((DoorSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //19 door
                        };

                        return uvs;
                    }
                case WallSegmentType.Window:
                    {
                        Vector2[] uvs = new Vector2[]
                        {
                        //front
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //0 front
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //1 window
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //2
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //3 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //4
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //5 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //6
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //7 window
                        //windowsill bottom
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //7 window 
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //8 windowsill y
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //11 y
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //1 window 
                        //windowsill left
                        new Vector2((-WindowSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //1 window x 
                        new Vector2((-WindowSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //11 
                        new Vector2((-WindowSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //10 
                        new Vector2((-WindowSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //3 window x
                        //windowsill top
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //3 window
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //10 y
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.y), //9 y
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.y), //5 window
                        //windowsill right
                        new Vector2((WindowSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //5 window
                        new Vector2((WindowSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //9 x
                        new Vector2((WindowSize.x * 0.5f) + (SegmentSize.z * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //8 windowsill x
                        new Vector2((WindowSize.x * 0.5f) + (-SegmentSize.z * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //7 window
                        //back
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //12 back
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //13 window
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //14
                        new Vector2((-WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //15 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //16
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (WindowSize.y * 0.5f) + SegmentPosition.y), //17 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //18
                        new Vector2((WindowSize.x * 0.5f) + SegmentPosition.x, (-WindowSize.y * 0.5f) + SegmentPosition.y), //19 window
                        };

                        return uvs;
                    }
                case WallSegmentType.FancyWindow1:
                    {
                        Vector2[] uvs = new Vector2[]
                        {
                        //front
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //0 front
                        new Vector2((-WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y), //1 window
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //2
                        new Vector2((-WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y), //3 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //4
                        new Vector2((WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y), //5 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //6
                        new Vector2((WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y), //7 window

                        //windowsill bottom
                        new Vector2((WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.y), //7 window y
                        new Vector2((SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.y), //8 windowsill 
                        new Vector2((-SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.y), //11 
                        new Vector2((-WindowSize.x * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.y), //1 window y

                        //windowsill left
                        new Vector2((-WindowSize.x * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y), //1 window x 
                        new Vector2((-SegmentSize.x * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y), //11 
                        new Vector2((-SegmentSize.x * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y), //10 
                        new Vector2((-WindowSize.x * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y), //3 window x

                        //windowsill top
                        new Vector2((-WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.y), //3 window
                        new Vector2((-SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.y), //10 y
                        new Vector2((SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.y), //9 y
                        new Vector2((WindowSize.x * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.y), //5 window

                        //windowsill right
                        new Vector2((WindowSize.x * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.x, (WindowSize.y * 0.25f) + SegmentPosition.y), //5 window
                        new Vector2((SegmentSize.x * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y), //9 x
                        new Vector2((SegmentSize.x * 0.25f) + (SegmentSize.z * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y), //8 windowsill x
                        new Vector2((WindowSize.x * 0.25f) + (-SegmentSize.z * 0.25f) + SegmentPosition.x, (-WindowSize.y * 0.25f) + SegmentPosition.y), //7 window

                        //back
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //12 back
                        new Vector2((-SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y), //13 window
                        new Vector2((-SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //14
                        new Vector2((-SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y), //15 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (SegmentSize.y * 0.5f) + SegmentPosition.y), //16
                        new Vector2((SegmentSize.x * 0.25f) + SegmentPosition.x, (SegmentSize.y * 0.25f) + SegmentPosition.y), //17 window
                        new Vector2((SegmentSize.x * 0.5f) + SegmentPosition.x, (-SegmentSize.y * 0.5f) + SegmentPosition.y), //18
                        new Vector2((SegmentSize.x * 0.25f) + SegmentPosition.x, (-SegmentSize.y * 0.25f) + SegmentPosition.y), //19 window
                        };

                        return uvs;
                    }
            }
        }

        /*public void Build()
        {
            //BuildMesh.Main(this);
        }*/
    }
}