﻿using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Mebiustos.MMD4MecanimFaciem {
    [CustomEditor(typeof(FaciemForMecanim))]
    public class FaciemForMecanimEditor : Editor {
        public override void OnInspectorGUI() {
            //seriObj = serializedObject;
            var casobj = (FaciemForMecanim)target;

            if (casobj.GetComponent<FaciemDatabase>() != null) {
                DrawGUI(casobj);
            }

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
                EditorApplication.MarkSceneDirty();
            }
        }

        /// <summary>
        /// DrawGUI
        /// </summary>
        /// <param name="casobj"></param>
        void DrawGUI(FaciemForMecanim casobj) {
            //EditorGUILayout.LabelField("Official Morph Helper Setting", EditorStyles.boldLabel);
            casobj.UseHelper = EditorGUILayout.Toggle("Use Official Morph Helper", casobj.UseHelper);
            //EditorGUI.BeginDisabledGroup(casobj.UseOfficialMorphHelper == false);
            casobj.OverrideWeight = EditorGUILayout.Toggle("Override Weight", casobj.OverrideWeight);
            //EditorGUI.EndDisabledGroup();

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("(Re)Create Object For Animation")) {
                CreateFaciemObject(casobj);
            }
            //if (GUILayout.Button("Export To MMD4MFace"))
            //    Debug.Log("");
            GUILayout.EndHorizontal();
        }

        void CreateFaciemObject(FaciemForMecanim casobj) {
            GameObject faciem;
            var ctrl = casobj.GetComponentInChildren<FaciemForMecanimRoot>();
            if (ctrl == null) {
                faciem = new GameObject("Faciem");
                faciem.transform.parent = casobj.transform;
                faciem.transform.localPosition = new Vector3(0, 0, 0);
                ctrl = faciem.AddComponent<FaciemForMecanimRoot>();
            } else {
                faciem = ctrl.gameObject;
            }

            var database = casobj.GetComponent<FaciemDatabase>();

            foreach (var weight in ctrl.GetComponentsInChildren<Weight>()) {
                bool found = false;
                foreach (var face in database.FaceDataList) {
                    if (face.FaceGuid == weight.FaceGUID) {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    DestroyImmediate(weight.gameObject);
            }

            foreach (var face in database.FaceDataList) {
                Weight newWeight = null;
                foreach (var weight in ctrl.GetComponentsInChildren<Weight>()) {
                    if (face.FaceGuid == weight.FaceGUID) {
                        newWeight = weight;
                        break;
                    }
                }
                if (!newWeight) {
                    var newgo = new GameObject(face.FaceName);
                    newgo.transform.parent = faciem.transform;
                    newgo.transform.localPosition = new Vector3(0, 0, 0);
                    newWeight = newgo.AddComponent<Weight>();
                } else {
                    //newWeight.gameObject.name = face.FaceName;
                }
                newWeight.FaceGUID = face.FaceGuid;
            }
        }
    }
}
