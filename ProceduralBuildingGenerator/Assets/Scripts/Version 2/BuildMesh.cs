using System;
using System.Collections;
using System.Collections.Generic;
//using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

namespace Segmentis
{
    public static class BuildMesh
    {

        static void Main(string[] args)
        {

        }

        /*public static GameObject Main(WallSegment obj)
        {
            return BuildMeshHelper(obj.CalculateVertices(), obj.CalculateTris(), obj.CalculateUV(), obj.SegmentName);
        }

        public static void Main(WallSegment[] obj)
        {
            foreach(WallSegment wallSegment in obj)
            {
                BuildMeshHelper(wallSegment.CalculateVertices(), wallSegment.CalculateTris(), wallSegment.CalculateUV(), wallSegment.SegmentName);
            }        
        }*/

        public static GameObject Main(Segmentis.Wall obj)
        {
            var created = BuildMeshHelper(obj);
            var script = created.AddComponent<WallInterface>();
            created.AddComponent<MeshCollider>();
            script.createdWall = created;
            script.wallObj = obj;
            return created;
        }

        public static void ReBuild(Segmentis.Wall wall)
        {
            if (!wall.BySegment)
            {
                GameObject objToEdit = wall.WallGameObjects;
                MeshFilter wallMeshFilter;
                MeshRenderer objSegmentMeshRenderer;

                if (objToEdit.transform.childCount == 0 && objToEdit.GetComponent<MeshFilter>())
                {
                    wallMeshFilter = objToEdit.GetComponent<MeshFilter>();
                    objSegmentMeshRenderer = objToEdit.GetComponent<MeshRenderer>();
                }
                else
                {
                    var tempList = objToEdit.transform.Cast<Transform>().ToList();

                    foreach (Transform child in tempList)
                    {
                        UnityEngine.Object.DestroyImmediate(child.gameObject);
                    }

                    wallMeshFilter = objToEdit.AddComponent<MeshFilter>();
                    objSegmentMeshRenderer = objToEdit.AddComponent<MeshRenderer>();
                    //objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/brick_modern_0099_02_tiled_s", typeof(Material));
                    //objSegmentMeshRenderer.material = (Material)Resources.Load(wall.Material.First(), typeof(Material));
                }

                if (wall.CalculatedMaterial == null || wall.CalculatedMaterial.Count == 0)
                {
                    objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/default", typeof(Material));
                }
                else
                {
                    objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + wall.CalculatedMaterial.FirstOrDefault(), typeof(Material));
                }

                Mesh mesh = new Mesh();
                wallMeshFilter.mesh = mesh;
                mesh.vertices = wall.CalculatedVertices.ToArray();
                mesh.triangles = wall.CalculatedTris.ToArray();
                //mesh.uv = GenerateUV(obj.CalculatedVertices.ToArray(), obj.CalculatedTris.ToArray());
                mesh.uv = wall.CalculatedUV.ToArray();
                mesh.RecalculateNormals();
                UnityEngine.Object.DestroyImmediate(objToEdit.GetComponent<MeshCollider>());
                objToEdit.AddComponent<MeshCollider>();
                //objToEdit.transform.Rotate(wall.WallRotation.x, wall.WallRotation.y, wall.WallRotation.z);
                objToEdit.transform.rotation = Quaternion.Euler(wall.WallRotation.x, wall.WallRotation.y, wall.WallRotation.z);

                //ReBuildHelper(obj.CalculatedVertices.ToArray(), obj.CalculatedTris.ToArray(), obj.CalculatedUV.ToArray(), obj.WallRotation);

                if (wall.ForceName)
                    objToEdit.name = wall.WallName;

                if (wall.ForcePosition)
                    objToEdit.transform.position = Vector3.zero;
            }
            else
            {
                GameObject objToEdit = wall.WallGameObjects;

                if (objToEdit.GetComponent<MeshFilter>())
                {
                    UnityEngine.Object.DestroyImmediate(objToEdit.GetComponent<MeshFilter>());
                    UnityEngine.Object.DestroyImmediate(objToEdit.GetComponent<MeshCollider>());
                    UnityEngine.Object.DestroyImmediate(objToEdit.GetComponent<MeshRenderer>());
                }

                if (wall.ForceName)
                    objToEdit.name = wall.WallName;

                if (wall.ForcePosition)
                    objToEdit.transform.position = Vector3.zero;

                var tempList = objToEdit.transform.Cast<Transform>().ToList();

                foreach (Transform child in tempList)
                {
                    UnityEngine.Object.DestroyImmediate(child.gameObject);
                }

                int i = 0;
                foreach (Segmentis.WallSegment segment in wall.CalculatedWallSegments)
                {
                    GameObject objTemp = BuildMeshHelper(segment.CalculateVertices(), segment.CalculateTris(), segment.CalculateUV(), wall.WallRotation, segment.SegmentName);
                    objTemp.transform.parent = objToEdit.transform;
                    objTemp.AddComponent<MeshCollider>();

                    MeshRenderer objSegmentMeshRenderer = objTemp.GetComponent<MeshRenderer>();
                    
                    if (wall.CalculatedMaterial == null || wall.CalculatedMaterial.Count == 0)
                    {
                        objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/default", typeof(Material));
                    }
                    else
                    {
                        if (wall.Material.Count < wall.CalculatedWallSegments.Count)
                            objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + wall.CalculatedMaterial.FirstOrDefault(), typeof(Material));
                        else
                            objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + wall.CalculatedMaterial[i], typeof(Material));
                    }

                    i++;
                }
            }
        }

        /*private static Mesh ReBuildHelper(Vector3[] vertices, int[] tris, Vector2[] uv, Vector3 rotation)
        {
            //UnityEngine.Object.Destroy(objToEdit.gameObject.GetComponent<MeshFilter>());



        }*/

        /*public static void Main(Wall[] obj)
        {
            foreach(Wall wall in obj)
            {
                BuildMeshHelper(wall.CalculatedVertices.ToArray(), wall.CalculatedTris.ToArray(), wall.WallRotation, wall.WallName);
            }
        }*/

        /*public static async void Main(Wall obj, float spawnCD)
        {     
            foreach (WallSegment segment in obj.CalculatedWallSegments)
            {
                BuildMeshHelper(segment.CalculateVertices(), segment.CalculateTris(), segment.CalculateUV(), obj.WallRotation, obj.WallName + " " + segment.SegmentName);
                await Task.Delay(TimeSpan.FromSeconds(spawnCD));
            }
        }

        public static async void Main(Wall[] obj, float spawnCD)
        {
            foreach (Wall wall in obj)
            {
                foreach (WallSegment segment in wall.CalculatedWallSegments)
                {
                    BuildMeshHelper(segment.CalculateVertices(), segment.CalculateTris(), segment.CalculateUV(), wall.WallRotation, wall.WallName + " " + segment.SegmentName);
                    await Task.Delay(TimeSpan.FromSeconds(spawnCD));
                    //System.Threading.Thread.Sleep(1000);
                }
            }
        }*/

        /*public static GameObject[] Main(Wall obj, bool bySegment)
        {
            List<GameObject> objs = new List<GameObject>();
            foreach (WallSegment segment in obj.CalculatedWallSegments)
            {

                objs.Add(BuildMeshHelper(segment.CalculateVertices(), segment.CalculateTris(), segment.CalculateUV(), obj.WallRotation, obj.WallName + " " + segment.SegmentName));
            }
            return objs.ToArray();
        }*/


        public static void Main(Vector3[] vertices, int[] tris, Vector2[] uvs, float scalling = 1f)
        {
            BuildMeshHelper(vertices, tris, uvs);
        }

        public static void Main(Vector3[] vertices, int[] tris, float scalling = 1f)
        {
            BuildMeshHelper(vertices, tris, GenerateUV(vertices, tris));
        }

        //uncommenct if .NET 3
        /*private static void BuildMeshHelper(Vector3[] vertices, int[] tris, Vector2[] uvs)
        {
            BuildMeshHelper(vertices, tris, uvs, "New Game Object");
        }*/


        private static GameObject BuildMeshHelper(Vector3[] vertices, int[] tris, Vector2[] uvs, string name = "New Game Object")
        {
            return BuildMeshHelper(vertices, tris, uvs, new Vector3(0, 0, 0), name);
        }

        private static GameObject BuildMeshHelper(Segmentis.Wall obj)
        {
            if (!obj.BySegment)
            {
                GameObject created = BuildMeshHelper(obj.CalculatedVertices.ToArray(), obj.CalculatedTris.ToArray(), obj.CalculatedUV.ToArray(), obj.WallRotation, obj.WallName);
                //var script = created.AddComponent<SegmentisWall>();

                if(obj.CalculatedMaterial == null || obj.CalculatedMaterial.Count == 0)
                {
                    MeshRenderer objSegmentMeshRenderer = created.GetComponent<MeshRenderer>();
                    objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/default", typeof(Material));
                }
                else
                {
                    MeshRenderer objSegmentMeshRenderer = created.GetComponent<MeshRenderer>();
                    objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + obj.CalculatedMaterial.FirstOrDefault(), typeof(Material));
                }

                return created;
            }
            else
            {
                GameObject wall = new GameObject();
                wall.name = obj.WallName;
                //var script = wall.AddComponent<SegmentisWall>();

                int i = 0;
                foreach (Segmentis.WallSegment segment in obj.CalculatedWallSegments)
                {
                    GameObject objTemp = BuildMeshHelper(segment.CalculateVertices(), segment.CalculateTris(), segment.CalculateUV(), obj.WallRotation, segment.SegmentName);
                    objTemp.transform.parent = wall.transform;
                    MeshRenderer objSegmentMeshRenderer = objTemp.GetComponent<MeshRenderer>();

                    if (obj.CalculatedMaterial == null || obj.CalculatedMaterial.Count == 0)
                    {
                        objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/default", typeof(Material));
                    }
                    else
                    {
                        if (obj.CalculatedMaterial.Count < obj.CalculatedWallSegments.Count)
                            objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + obj.CalculatedMaterial.FirstOrDefault(), typeof(Material));
                        else
                            objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/" + obj.CalculatedMaterial[i], typeof(Material));
                    }

                    i++;
                }
                return wall;
            }
        }

        private static GameObject BuildMeshHelper(Vector3[] vertices, int[] tris, Vector2[] uvs, Vector3 rotation, string name = "New Game Object")
        {
            GameObject obj = new GameObject();
            MeshFilter wallMeshFilter = obj.gameObject.AddComponent<MeshFilter>();
            Mesh mesh = new Mesh();
            wallMeshFilter.mesh = mesh;
            mesh.vertices = vertices;
            mesh.triangles = tris;
            //MeshRenderer objSegmentMeshRenderer = obj.gameObject.AddComponent<MeshRenderer>();
            obj.gameObject.AddComponent<MeshRenderer>();
            //objSegmentMeshRenderer.material = (Material)Resources.Load("Materials/brick_modern_0099_02_tiled_s", typeof(Material));
            mesh.uv = uvs;
            //mesh.uv = GenerateUV(vertices, tris);
            mesh.RecalculateNormals();
            //obj.AddComponent<MeshCollider>();
            obj.name = name;
            //obj.transform.Rotate(rotation.x, rotation.y, rotation.z);
            obj.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);

            return obj;
        }

        private static GameObject BuildMeshHelper(Vector3[] vertices, int[] tris, Vector3 rotation, string name = "New Game Object")
        {
            return BuildMeshHelper(vertices, tris, GenerateUV(vertices, tris), rotation, name);
        }

        public static Vector2[] GenerateUV(Vector3[] vertices, int[] tris, float scaling = 1)
        {
            Vector2[] uvs = new Vector2[vertices.Length];

            for (int index = 0; index < tris.Length; index += 3)
            {
                Vector3 v1 = vertices[tris[index]];
                Vector3 v2 = vertices[tris[index + 1]];
                Vector3 v3 = vertices[tris[index + 2]];
                Vector3 normal = Vector3.Cross(v3 - v1, v2 - v1);
                Quaternion rotation = Quaternion.Inverse(Quaternion.LookRotation(normal));
                uvs[tris[index]] = (Vector2)(rotation * v1) * scaling;
                uvs[tris[index + 1]] = (Vector2)(rotation * v2) * scaling;
                uvs[tris[index + 2]] = (Vector2)(rotation * v3) * scaling;
            }

            return uvs;
        }
    }
}