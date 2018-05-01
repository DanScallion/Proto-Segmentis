using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Segmentis {
    [CustomEditor(typeof(BuildingInterface))]
    public class BuildingEditor : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            BuildingInterface script = (BuildingInterface)target;

            if (GUILayout.Button("Build"))
            {
                //script.Build();
            }
        }

    }
}
