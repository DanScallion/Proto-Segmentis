using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using System.IO;

[ExecuteInEditMode]
public class WallInterface : MonoBehaviour {
    [SerializeField] private string uniqueId;
    public Vector3 wallSize = new Vector3(1, 2.4f, 0.1f);
    public Vector3 wallPosition = new Vector3(0, 0, 0);
    public Vector3 wallRotation = new Vector3(0, 0, 0);
    public Vector2 windowSize = new Vector2(1, 1);
    public Vector2 doorSize = new Vector2(1, 2);
    public float spawnCD = 0.5f;
    public bool bySegment = false;
    public bool mergePlainSegments = false;
    public GameObject createdWall;
    [SerializeField]
    public Segmentis.Wall wallObj;
    public bool debug = false;
    public bool forcePosition = false;
    public bool forceName = false;
    public bool autoRebuild = true;

    public List<WallSegmentType> wallSegments = new List<WallSegmentType>();
    public List<string> materials = new List<string>();

    private Vector3 _onChangeWallSize = Vector3.zero;
    private Vector3 _onChangeWallPosition = Vector3.zero;
    private Vector3 _onChangeWallRotation = Vector3.zero;
    private Vector2 _onChangeWindowSize = Vector3.zero;
    private Vector2 _onChangeDoorSize = Vector3.zero;
    private bool _onChangeBySegment = false;
    private bool _onChangeMergePlainSegments = false;
    
    void Start () {
        ReflectData(wallObj);
        InitializeOnChange();
    }

    void Update () {
        MonitorChange();
    }

    public IEnumerator EveryXseconds()
    {
        foreach (Transform segment in createdWall.transform)
        {
            segment.gameObject.SetActive(true);
            yield return new WaitForSeconds(spawnCD);
        }
    }

    private void InitializeOnChange()
    {
        _onChangeWallSize = wallSize;
        _onChangeWallPosition = wallPosition;
        _onChangeWallRotation = wallRotation;
        _onChangeWindowSize = windowSize;
        _onChangeDoorSize = doorSize;
        _onChangeBySegment = bySegment;
        _onChangeMergePlainSegments = mergePlainSegments;
    }

    private void MonitorChange()
    {
        if(createdWall != null)
        {
            if (wallSize != _onChangeWallSize && autoRebuild)
            {
                wallObj.WallSize = wallSize;
                _onChangeWallSize = wallObj.WallSize;
                Rebuild();
            }

            if (wallPosition != _onChangeWallPosition && autoRebuild)
            {
                wallObj.WallPosition = wallPosition;
                _onChangeWallPosition = wallObj.WallPosition;
                Rebuild();
            }

            if (wallRotation != _onChangeWallRotation && autoRebuild)
            {
                wallObj.WallRotation = wallRotation;
                _onChangeWallRotation = wallObj.WallRotation;
                Rebuild();
            }

            if (windowSize != _onChangeWindowSize && autoRebuild)
            {
                wallObj.WindowSize = windowSize;
                _onChangeWindowSize = wallObj.WindowSize;
                Rebuild();
            }

            if (doorSize != _onChangeDoorSize && autoRebuild)
            {
                wallObj.DoorSize = doorSize;
                _onChangeDoorSize = wallObj.DoorSize;
                Rebuild();
            }

            if(bySegment != _onChangeBySegment && autoRebuild)
                {
                wallObj.BySegment = bySegment;
                _onChangeBySegment = wallObj.BySegment;
                Rebuild();
            }

            if (mergePlainSegments != _onChangeMergePlainSegments && autoRebuild)
            {
                wallObj.MergeSegments = mergePlainSegments;
                _onChangeMergePlainSegments = wallObj.MergeSegments;
                Rebuild();
            }
        }
    }

    public void CheckBools(Segmentis.Wall wall)
    {
        /*if (bySegment)
            wall.BySegment = true;
        else
            wall.BySegment = false;

        if (mergePlainSegments)
            wall.MergeSegments = true;
        else
            wall.MergeSegments = false;*/

        if (debug)
            wall.Debug = true;
        else
            wall.Debug = false;

        if (forcePosition)
            wall.ForcePosition = true;
        else
            wall.ForcePosition = false;

        if (forceName)
            wall.ForceName = true;
        else
            wall.ForceName = false;
    }

    public void Rebuild()
    {
        if (wallObj == null)
            LoadData();

        if (wallObj.WallGameObjects == null)
            wallObj.WallGameObjects = createdWall;

        //wallObj.WallGameObjects = createdWall;

        if (createdWall != null)
        {
            //wallObj.SegmentType = wallObj.SegmentType;

            CheckBools(wallObj);
            wallObj.SegmentType = wallSegments;
            wallObj.Material = materials;

            wallObj.ReBuild();
        }
    }

    public void SegmentsToLeft()
    {
        List<WallSegmentType> tempSegments = new List<WallSegmentType>();

        for (int i = 1; i < wallSegments.Count; i++)
        {
            tempSegments.Add(wallSegments[i]);
        }
        tempSegments.Add(wallSegments.First());
        wallSegments = tempSegments;

        if (autoRebuild)
            Rebuild();
    }

    public void SegmentsToRight()
    {
        List<WallSegmentType> tempSegments = new List<WallSegmentType>();

        tempSegments.Add(wallSegments.Last());
        for(int i = 0; i < wallSegments.Count-1; i++)
        {
            tempSegments.Add(wallSegments[i]);
        }
        wallSegments = tempSegments;

        if(autoRebuild)
            Rebuild();
    }

    public void Build()
    {
        if(wallObj == null)
        {
            wallObj = new Segmentis.Wall(wallObj.WallSize, wallObj.WallRotation, wallObj.WallPosition, "Wall 1", wallObj.WindowSize, wallObj.DoorSize, wallObj.SegmentType.ToList());

            CheckBools(wallObj);

            createdWall = wallObj.Build();

            if (spawnCD > 0 && wallObj.BySegment && createdWall != null && Application.isPlaying)
            {
                foreach (Transform segment in createdWall.transform)
                {
                    segment.gameObject.SetActive(false);
                }

                StartCoroutine("everyXseconds");
            }
        }
        else
        {
            Segmentis.Wall newWall2 = new Segmentis.Wall(wallObj.WallSize, wallObj.WallRotation, wallObj.WallPosition, "Wall 1", wallObj.WindowSize, wallObj.DoorSize, wallObj.SegmentType.ToList());

            newWall2.SegmentType = wallObj.SegmentType;

            CheckBools(newWall2);

            newWall2.WallSize = wallObj.WallSize;
            newWall2.WallPosition = wallObj.WallPosition;
            newWall2.WallRotation = wallObj.WallRotation;
            newWall2.WindowSize = wallObj.WindowSize;
            newWall2.DoorSize = wallObj.DoorSize;

            newWall2.Build();            
        }
    }
#if UNITY_EDITOR
    public void SaveAsPrefab()
    {
        if (createdWall != null)
        {
            Directory.CreateDirectory("Assets/Runtime/" + wallObj.UniqueID +  "/Mesh/");
            Directory.CreateDirectory("Assets/Runtime/" + wallObj.UniqueID +  "/Prefabs/");
            Serializer.Save<Segmentis.Wall>("Assets/Runtime/" + wallObj.UniqueID + "/save.json", wallObj, true);

            if (!wallObj.BySegment)
            {
                DestroyImmediate(createdWall.GetComponent<MeshCollider>(), true);


                if (Application.isPlaying)
                    AssetDatabase.CreateAsset(createdWall.gameObject.GetComponent<MeshFilter>().mesh, "Assets/Runtime/" + wallObj.UniqueID +  "/Mesh/" + createdWall.name + ".asset");
                else
                    AssetDatabase.CreateAsset(createdWall.gameObject.GetComponent<MeshFilter>().sharedMesh, "Assets/Runtime/" + wallObj.UniqueID + "/Mesh/" + createdWall.name + ".asset");

                AssetDatabase.SaveAssets();
                GameObject prefab = PrefabUtility.CreatePrefab("Assets/Runtime/" + wallObj.UniqueID + "/Prefabs/" + createdWall.name + ".prefab", createdWall);
                //DestroyImmediate(prefab.GetComponent<MeshCollider>(), true);
                prefab.AddComponent<MeshCollider>();
            }
            else
            {
                foreach (Transform segment in createdWall.transform)
                {
                    DestroyImmediate(segment.GetComponent<MeshCollider>(), true);

                    if (Application.isPlaying)
                        AssetDatabase.CreateAsset(segment.gameObject.GetComponent<MeshFilter>().mesh, "Assets/Runtime/" + wallObj.UniqueID + "/Mesh/" + segment.name + ".asset");
                    else
                        AssetDatabase.CreateAsset(segment.gameObject.GetComponent<MeshFilter>().sharedMesh, "Assets/Runtime/" + wallObj.UniqueID + "/Mesh/" + segment.name + ".asset");

                    AssetDatabase.SaveAssets();
                    /*GameObject prefab =*/ PrefabUtility.CreatePrefab("Assets/Runtime/" + wallObj.UniqueID + "/Prefabs/" + createdWall.name + ".prefab", createdWall);
                    //DestroyImmediate(segment.gameObject.GetComponent<MeshCollider>(), true);
                    segment.gameObject.AddComponent<MeshCollider>();
                }
            }
                    
        }
    }
#endif
    public void SaveAsData()
    {
        Directory.CreateDirectory("Assets/Runtime/" + wallObj.UniqueID);
        Serializer.Save<Segmentis.Wall>("Assets/Runtime/" + wallObj.UniqueID +  "/save.json", wallObj, true);
    }

    public void LoadData()
    {
        print(wallObj);
        string dir = ("Assets/Runtime/" + uniqueId + "/save.json");
        wallObj = Serializer.Load<Segmentis.Wall>(dir, true);
        wallObj.WallGameObjects = createdWall;
        print(wallObj.UniqueID);
        ReflectData(wallObj);
    }

    public void ReflectData(Segmentis.Wall wall)
    {
        uniqueId = wall.UniqueID;
        wallSize = wall.WallSize;
        wallPosition = wall.WallPosition;
        wallRotation = wall.WallRotation;
        windowSize = wall.WindowSize;
        doorSize = wall.DoorSize;
        bySegment = wall.BySegment;
        mergePlainSegments = wall.MergeSegments;
        debug = wall.Debug;
        forcePosition = wall.ForcePosition;
        forceName = wall.ForceName;
        wallSegments = wall.SegmentType;
        materials = wall.Material;
    }
}
