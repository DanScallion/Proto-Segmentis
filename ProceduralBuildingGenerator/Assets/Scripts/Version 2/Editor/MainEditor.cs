using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Segmentis
{
    [InitializeOnLoad]
    public class MainEditor : EditorWindow
    {
        [MenuItem("Tools/Segmentis")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(MainEditor), false, "Segmentis");
        }

        void OnGUI()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (GUILayout.Button("Create Wall"))
            {
                CreateWall();
            }

            if (GUILayout.Button("Create Building"))
            {
                CreateBuilding();
            }
        }

        private void CreateWall()
        {
            new Segmentis.Wall().Build();
        }

        private void CreateBuilding()
        {
            new Building().Build();
        }
    }
}
