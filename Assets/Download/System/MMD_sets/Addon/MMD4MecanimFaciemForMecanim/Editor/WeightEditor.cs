using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text;

namespace Mebiustos.MMD4MecanimFaciem {
    [CustomEditor(typeof(Weight))]
    public class WeightEditor : Editor {
        public override void OnInspectorGUI() {
            var weight = (Weight)target;

            weight.Value = EditorGUILayout.FloatField("Value", weight.Value);

            EditorGUILayout.Separator();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Create AnimationClip File")) {
                CreateClip();
            }
            GUILayout.EndHorizontal();

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
                EditorApplication.MarkSceneDirty();
            }
        }

        void CreateClip() {
            var weight = (Weight)target;
            var database = weight.GetComponentInParent<FaciemDatabase>();
            
            if (database) {
                    var animclip = new AnimationClip();

                        EditorCurveBinding curveBinding = new EditorCurveBinding();
                        curveBinding.path = this.GetGameObjectPath(database.transform, weight.transform);
                        curveBinding.type = typeof(Weight);
                        curveBinding.propertyName = "Value";

                        AnimationCurve curve = new AnimationCurve();
                        curve.AddKey(0f, 1f);

                        AnimationUtility.SetEditorCurve(animclip, curveBinding, curve);

                    var filepath = new StringBuilder()
                            .Append("Assets/")
                            .Append(database.gameObject.name).Append("_").Append(weight.gameObject.name)
                            .Append(".anim").ToString();
                    filepath = AssetDatabase.GenerateUniqueAssetPath(filepath);

                    AssetDatabase.CreateAsset(animclip, filepath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    var sb = new StringBuilder()
                        .Append("AnimationClipを作成しました。").Append(System.Environment.NewLine)
                        .Append(filepath);
                    EditorUtility.DisplayDialog("Created", sb.ToString(), "OK");
            }
        }

        string GetGameObjectPath(Transform root, Transform transform) {
            StringBuilder path = new StringBuilder().Append(transform.name);
            while (transform.parent != null && transform.parent != root) {
                transform = transform.parent;
                path.Insert(0, "/").Insert(0, transform.name);
            }
            return path.ToString();
        }
    }
}
