using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Segmentis {
    [CustomEditor(typeof(WallInterface))]
    public class WallEditor : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            WallInterface script = (WallInterface)target;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("<<<"))
            {
                script.SegmentsToLeft();
            }

            if (GUILayout.Button(">>>"))
            {
                script.SegmentsToRight();
            }
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Create Duplicate/Build"))
            {
                script.Build();
            }

            if (GUILayout.Button("Rebuild"))
            {
                script.Rebuild();
            }

            if (GUILayout.Button("Save as Prefab"))
            {
                script.SaveAsPrefab();
            }

            if (GUILayout.Button("Save as Data"))
            {
                script.SaveAsData();
            }

            if (GUILayout.Button("Load Data"))
            {
                script.LoadData();
                //script.ReflectData();
            }
        }

    }
}