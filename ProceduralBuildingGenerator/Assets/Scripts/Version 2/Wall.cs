using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Segmentis
{
    [System.Serializable]
    public class Wall
    {
        [SerializeField] private string _uniqueID;
        [SerializeField] private Vector3 _wallSize;
        [SerializeField] private Vector3 _wallPosition;
        [SerializeField] private Vector3 _wallRotation;
        [SerializeField] private string _wallName;
        [SerializeField] private Vector2 _windowSize;
        [SerializeField] private Vector2 _doorSize;
        [SerializeField] private List<WallSegmentType> _segmentType = new List<WallSegmentType>();
        [SerializeField] private bool _mergeSegments = true;
        [SerializeField] private bool _debug = false;
        [SerializeField] private List<Vector3> _calculatedVertices;
        [SerializeField] private List<int> _calculatedTris;
        [SerializeField] private List<Vector2> _calculatedUV;
        [SerializeField] private List<WallSegment> _calculatedWallSegment;
        [SerializeField] private List<WallSegmentType> _calculatedSegmentType;
        [System.NonSerialized()] private GameObject _wallGameObjects;
        [SerializeField] private bool _forcePosition = true;
        [SerializeField] private bool _forceName = false;
        [SerializeField] private bool _bySegment = false;
        [SerializeField] private List<string> _material;
        [SerializeField] private List<string> _calculatedMaterial;

        public Wall()
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = new Vector3(1, 2.4f, 0.1f);
            this.WallPosition = new Vector3(0, 0, 0);
            this.WallRotation = new Vector3(0, 0, 0);
            this.WallName = "Wall";
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType.Add(WallSegmentType.Plain);
        }

        public Wall(Vector3 wallSize)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = new Vector3(0, 0, 0);
            this.WallRotation = new Vector3(0, 0, 0);
            this.WallName = "Wall";
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType.Add(WallSegmentType.Plain);
        }

        public Wall(Vector3 wallSize, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = new Vector3(0, 0, 0);
            this.WallRotation = new Vector3(0, 0, 0);
            this.WallName = "Wall";
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType = segmentType;
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = "Wall";
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType.Add(WallSegmentType.Plain);
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = "Wall";
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType = segmentType;
        }

        public Wall(Vector3 wallSize, Vector2 windowSize, Vector2 doorSize, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = new Vector3(0, 0, 0);
            this.WallRotation = new Vector3(0, 0, 0);
            this.WallName = "Wall";
            this.WindowSize = windowSize;
            this.DoorSize = doorSize;
            this.SegmentType = segmentType;
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, string wallName)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = wallName;
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType.Add(WallSegmentType.Plain);
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, string wallName, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = wallName;
            this.WindowSize = new Vector2(1, 1);
            this.DoorSize = new Vector2(0.9144f, 2.032f);
            this.SegmentType = segmentType;
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, string wallName, Vector2 windowSize, Vector2 doorSize)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = wallName;
            this.WindowSize = windowSize;
            this.DoorSize = doorSize;
            this.SegmentType.Add(WallSegmentType.Plain);
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, Vector2 windowSize, Vector2 doorSize, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = "Wall";
            this.WindowSize = windowSize;
            this.DoorSize = doorSize;
            this.SegmentType = segmentType;
        }

        public Wall(Vector3 wallSize, Vector3 wallRotation, Vector3 wallPosition, string wallName, Vector2 windowSize, Vector2 doorSize, List<WallSegmentType> segmentType)
        {
            this.UniqueID = IDgenerator.CreateID();
            this.WallSize = wallSize;
            this.WallPosition = wallPosition;
            this.WallRotation = wallRotation;
            this.WallName = wallName;
            this.WindowSize = windowSize;
            this.DoorSize = doorSize;
            this.SegmentType = segmentType;
        }

        public Vector3 WallSize
        {
            get
            {
                return _wallSize;
            }

            set
            {
                if (value.x > 0 && value.y > 0 && value.z > 0)
                    _wallSize = value;
                else if (value.x <= 0 && value.y > 0 && value.z > 0)
                    WallSize = new Vector3(1, value.y, value.z);
                else if (value.x > 0 && value.y <= 0 && value.z > 0)
                    WallSize = new Vector3(value.x, 0.4f, value.z);
                else if (value.x > 0 && value.y > 0 && value.z <= 0)
                    WallSize = new Vector3(value.x, value.y, 0.1f);
                else
                    WallSize = new Vector3(1, 2.4f, 0.1f);
            }
        }

        public Vector3 WallPosition
        {
            get
            {
                return _wallPosition;
            }

            set
            {
                _wallPosition = value;
            }
        }

        public Vector3 WallRotation
        {
            get
            {
                return _wallRotation;
            }

            set
            {
                _wallRotation = value;
            }
        }

        public string WallName
        {
            get
            {
                return _wallName;
            }

            set
            {
                _wallName = value;
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
                _windowSize = value;
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
                _doorSize = value;
            }
        }

        public List<WallSegmentType> SegmentType
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

        public bool MergeSegments
        {
            get
            {
                return _mergeSegments;
            }

            set
            {
                _mergeSegments = value;
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

        public List<Vector3> CalculatedVertices
        {
            get
            {
                if (!IsBuilt())
                    CalculateWall();

                return _calculatedVertices;
            }

            private set
            {
                _calculatedVertices = value;
            }
        }

        public List<int> CalculatedTris
        {
            get
            {
                if (!IsBuilt())
                    CalculateWall();

                return _calculatedTris;
            }

            private set
            {
                _calculatedTris = value;
            }
        }

        public List<WallSegment> CalculatedWallSegments
        {
            get
            {
                return _calculatedWallSegment;
            }

            private set
            {
                _calculatedWallSegment = value;
            }
        }

        public GameObject WallGameObjects
        {
            get
            {
                return _wallGameObjects;
            }

            set
            {
                _wallGameObjects = value;
            }
        }

        public bool ForcePosition
        {
            get
            {
                return _forcePosition;
            }

            set
            {
                _forcePosition = value;
            }
        }

        public bool ForceName
        {
            get
            {
                return _forceName;
            }

            set
            {
                _forceName = value;
            }
        }

        public bool BySegment
        {
            get
            {
                return _bySegment;
            }

            set
            {
                _bySegment = value;
            }
        }

        public List<Vector2> CalculatedUV
        {
            get
            {
                return _calculatedUV;
            }

            private set
            {
                _calculatedUV = value;
            }
        }

        public List<WallSegmentType> CalculatedSegmentType
        {
            get
            {
                return _calculatedSegmentType;
            }

            private set
            {
                _calculatedSegmentType = value;
            }
        }

        public List<string> Material
        {
            get
            {
                return _material;
            }

            set
            {
                _material = value;
            }
        }

        public List<string> CalculatedMaterial
        {
            get
            {
                return _calculatedMaterial;
            }

            private set
            {
                _calculatedMaterial = value;
            }
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

        private List<Vector3> CalculateWallVertices(List<WallSegment> calculatedWallSegments)
        {
            List<Vector3> vertices = new List<Vector3>();

            foreach (WallSegment wallSegment in calculatedWallSegments)
            {
                vertices.AddRange(wallSegment.CalculateVertices());
            }

            return vertices;
        }

        private List<int> CalculateWallTris(List<WallSegment> calculatedWallSegments)
        {
            List<int> tris = new List<int>();
            int numOfVertices = 0;
            List<WallSegment> wallVertices = calculatedWallSegments.ToList();

            foreach (WallSegment segment in wallVertices)
            {
                if (tris.Count == 0)
                {
                    tris.AddRange(segment.CalculateTris());
                    numOfVertices += segment.CalculateVertices().Length;
                }
                else
                {
                    List<int> tempTris = segment.CalculateTris().Select(n => n + numOfVertices).ToList();
                    numOfVertices += segment.CalculateVertices().Length;
                    tris.AddRange(tempTris);
                }
            }

            /*int[] tris = new int[]
            {

            };

            int sumOfVertices = 0;

            for (int i = 0; i < CalculateWall().Length; i++)
            {
                if(i == 0)
                {
                    tris = CalculateWall()[i].CalculateTris();
                }
                else
                {
                    sumOfVertices += CalculateWall()[i - 1].CalculateVertices().Length;
                    int[] tempTris = CalculateWall()[i].CalculateTris().Select(n => n + sumOfVertices).ToArray();

                    tris = tris.Concat(tempTris).ToArray();
                }
            }*/

            return tris;
        }

        private List<Vector2> CalculateWallUV(List<WallSegment> calculatedWallSegments)
        {
            /*Vector2[] uv = new Vector2[0]
            {

            };*/
            List<Vector2> uv = new List<Vector2>();

            foreach (WallSegment wallSegment in calculatedWallSegments)
            {
                uv.AddRange(wallSegment.CalculateUV());
            }

            return uv;

            /*if(CalculatedWallSegments == null || CalculatedWallSegments.Count() == 0)
                CalculateWall();*/

            //return BuildMesh.GenerateUV(CalculatedVertices.ToArray(), CalculatedTris.ToArray());

        }

        private void CalculateWall()
        {
            List<WallSegment> wallSegments = new List<WallSegment>();

            float[] segmentWidth = CalculateSegmentWidth(WallSize.x, SegmentType);

            float sumOfWidth = 0;

            for (int i = 0; i < CalculatedSegmentType.Count(); i++)
            {
                switch (CalculatedSegmentType[i])
                {
                    default:
                    case WallSegmentType.Plain:
                        {
                            WallSegment segment = new WallSegment(new Vector3(segmentWidth[i], WallSize.y, WallSize.z),
                                                  new Vector3(WallPosition.x + sumOfWidth + (segmentWidth[i] / 2), WallPosition.y + (WallSize.y / 2), WallPosition.z),
                                                  CalculatedSegmentType[i], "Segment " + (i + 1) + " " + CalculatedSegmentType[i] as string);
                            wallSegments.Add(segment);
                            sumOfWidth += segmentWidth[i];
                        }
                        break;
                    case WallSegmentType.Door:
                        {

                            WallSegment segment = new WallSegment(new Vector3(segmentWidth[i], WallSize.y, WallSize.z),
                                                                  new Vector3(WallPosition.x + sumOfWidth + (segmentWidth[i] / 2), WallPosition.y + (WallSize.y / 2), WallPosition.z),
                                                                  CalculatedSegmentType[i], DoorSize, "Segment " + (i + 1) + " " + CalculatedSegmentType[i] as string);
                            wallSegments.Add(segment);
                            sumOfWidth += segmentWidth[i];
                        }
                        break;
                    case WallSegmentType.FancyWindow1:
                    case WallSegmentType.Window:
                        {

                            WallSegment segment = new WallSegment(new Vector3(segmentWidth[i], WallSize.y, WallSize.z),
                                                  new Vector3(WallPosition.x + sumOfWidth + (segmentWidth[i] / 2), WallPosition.y + (WallSize.y / 2), WallPosition.z),
                                                  CalculatedSegmentType[i], WindowSize, "Segment " + (i + 1) + " " + CalculatedSegmentType[i] as string);
                            wallSegments.Add(segment);
                            sumOfWidth += segmentWidth[i];
                        }
                        break;
                }
            }

            if (Debug)
            {
                string msg = "";
                foreach (float segment in segmentWidth)
                {
                    msg += segment + ", ";
                }
                msg += "======================" + segmentWidth.Count();
                UnityEngine.Debug.Log(msg);
            }

            CalculatedWallSegments = wallSegments;
            CalculatedVertices = CalculateWallVertices(CalculatedWallSegments);
            CalculatedTris = CalculateWallTris(CalculatedWallSegments);
            CalculatedUV = CalculateWallUV(CalculatedWallSegments);
        }

        private float[] CalculateSegmentWidth(float wallWidth, List<WallSegmentType> wallSegments)
        {
            float[] segmentWidth = new float[wallSegments.Count()];

            for (int i = 0; i < wallSegments.Count(); i++)
            {
                segmentWidth[i] = wallWidth / wallSegments.Count();
            }


            if (MergeSegments)
            {
                float[] mergedPlainSegments = MergePlainSegments(wallSegments, segmentWidth);
                UpdateSegmentsType();
                return mergedPlainSegments;
            }
            else
            {
                CalculatedSegmentType = SegmentType;
                //CalculatedSegmentType.Clear();
                //CalculatedSegmentType.AddRange(SegmentType);
                CalculatedMaterial = Material;
                //CalculatedMaterial.Clear();
                //CalculatedMaterial.AddRange(Material);

                if (Debug)
                {
                    string msg = "";
                    foreach (WallSegmentType segment in CalculatedSegmentType)
                    {
                        msg += segment + ", ";
                    }
                    msg += "======================" + CalculatedSegmentType.Count();
                    UnityEngine.Debug.Log(msg);

                    string msg2 = "";
                    foreach (string mat in CalculatedMaterial)
                    {
                        msg2 += mat + ", ";
                    }
                    msg2 += "======================" + CalculatedMaterial.Count();
                    UnityEngine.Debug.Log(msg2);
                }

                return segmentWidth;
            }
        }

        private float[] MergePlainSegments(List<WallSegmentType> wallSegments, float[] segmentSize)
        {
            float[] mergedSize = new float[segmentSize.Length];

            /*List<WallSegmentType> mergedPlainSegments = new List<WallSegmentType>();
            List<float> merged = new List<float>();
            foreach (WallSegmentType element in wallSegments)
            {
                if (mergedPlainSegments.Count == 0 || element != WallSegmentType.Plain || mergedPlainSegments.Last() != element)
                {
                    mergedPlainSegments.Add(element);
                }

            }*/

            for (int i = 0; i < wallSegments.Count(); i++)
            {
                if (wallSegments[i] != WallSegmentType.Plain || i == 0)
                {
                    mergedSize[i] = segmentSize[i];
                }
                else
                {
                    if (wallSegments[i - 1] == WallSegmentType.Plain)
                    {
                        mergedSize[i] = mergedSize[i - 1] + segmentSize[i];
                        mergedSize[i - 1] = 0;
                    }
                    else
                        mergedSize[i] = segmentSize[i];
                }
            }

            return GetRidOfZeros(mergedSize);
        }

        private void UpdateSegmentsType()
        {
            List<WallSegmentType> mergedPlainSegments = new List<WallSegmentType>();
            List<string> mergedMaterialSegments = new List<string>();

            foreach (WallSegmentType element in SegmentType)
            {
                if (mergedPlainSegments.Count == 0 || element != WallSegmentType.Plain || mergedPlainSegments.Last() != element)
                {
                    mergedPlainSegments.Add(element);

                    if (Material != null && Material.Count > 0)
                        if (Material.Count == SegmentType.Count)
                            mergedMaterialSegments.Add(Material[SegmentType.FindIndex(x => x == element)]);
                        else
                            if (mergedMaterialSegments.Count == 0)
                                mergedMaterialSegments.Add(Material.First());
                }
            }

            CalculatedSegmentType = mergedPlainSegments;
            CalculatedMaterial = mergedMaterialSegments;

            if (Debug)
            {
                string msg = "";
                foreach (WallSegmentType segment in CalculatedSegmentType)
                {
                    msg += segment + ", ";
                }
                msg += "======================" + mergedPlainSegments.Count();
                UnityEngine.Debug.Log(msg);

                string msg2 = "";
                foreach (string mat in CalculatedMaterial)
                {
                    msg2 += mat + ", ";
                }
                msg2 += "======================" + CalculatedMaterial.Count();
                UnityEngine.Debug.Log(msg2);
            }
        }

        private float[] GetRidOfZeros(float[] before)
        {
            //float[] after = new float[before.Length];
            /*List<float> after = new List<float>();

            foreach (float num in before)
            {
                if (after.Count == 0 || num == 0 || after.Last() != num)
                    after.Add(num);
            }
            return after.ToArray()
             */

            return before.Where(i => i != 0).ToArray();
        }

        public bool IsBuilt()
        {
            if (CalculatedWallSegments == null || CalculatedWallSegments.Count() == 0)
                return false;
            else
                return true;
        }

        public GameObject Build()
        {
            if (!IsBuilt())
            {
                CalculateWall();
                WallGameObjects = BuildMesh.Main(this);
            }
            return WallGameObjects;
        }

        public void ReBuild()
        {
            CalculateWall();

            BuildMesh.ReBuild(this);
        }
    }
}