using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Kazegames.Utility;
using System.Collections.Generic;

namespace Kazegames.Editor
{
    [CustomEditor(typeof(LevelConfigure))]
    public class LevelConfigureBuilder : UnityEditor.Editor, IPreprocessBuildWithReport
    {
        SerializedProperty _spStartupScenes;
        SerializedProperty _spLevelScenes;
        SerializedProperty _spGameLevels;

        public int callbackOrder { get { return 0; } }

        void OnEnable()
        {
            _spStartupScenes = serializedObject.FindProperty("startupScenes");
            _spLevelScenes = serializedObject.FindProperty("levelScenes");
            _spGameLevels = serializedObject.FindProperty("gameLevels");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_spStartupScenes);
            EditorGUILayout.PropertyField(_spLevelScenes);
            
            GUI.enabled = false;
            EditorGUILayout.PropertyField(_spGameLevels);
            GUI.enabled = true;

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space(20);

            if(GUILayout.Button("Build Levels"))
            {
                LevelConfigure config = Resources.Load<LevelConfigure>("Level Configure");

                if (config == null)
                {
                    if (EditorUtility.DisplayDialog("Error", "Not found the LevelConfigure file," +
                        "\nYou should create a LevelConfigure file named \"Level Configure\" in UNITY Resources folder", "Cancel"))
                    {
                        return;
                    }
                }

                BuildLevels(config);
            }
        }

        private void BuildLevels(LevelConfigure config)
        {
            // change building scenes
            Debug.LogWarning("change building scenes..");

            List<EditorBuildSettingsScene> buildingList = new List<EditorBuildSettingsScene>();

            if (config.startupScenes != null && config.startupScenes.Length > 0)
            {
                foreach (var s in config.startupScenes)
                {
                    string path = AssetDatabase.GetAssetPath(s);
                    string guid = AssetDatabase.AssetPathToGUID(path);

                    buildingList.Add(new EditorBuildSettingsScene(path, true));

                    Debug.Log($"added scene({s.name}): {path}");
                }
            }

            if (config.levelScenes != null && config.levelScenes.Length > 0)
            {
                foreach (var s in config.levelScenes)
                {
                    string path = AssetDatabase.GetAssetPath(s);
                    string guid = AssetDatabase.AssetPathToGUID(path);

                    buildingList.Add(new EditorBuildSettingsScene(path, true));

                    Debug.Log($"added scene({s.name}): {path}");
                }
            }

            EditorBuildSettings.scenes = buildingList.ToArray();

            // build level configure
            config.Build();

            EditorUtility.SetDirty(config);
            AssetDatabase.SaveAssets();
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUtility.DisplayDialog("Level Configure Builder", "Did you use LevelConfigure?", "Yes", "No"))
            {
                LevelConfigure config = Resources.Load<LevelConfigure>("Level Configure");

                if (config == null)
                {
                    throw new System.NullReferenceException("Not found the LevelConfigure file, you should create a LevelConfigure file named \"Level Configure\" in UNITY Resources folder");
                }

                BuildLevels(config);
            }
        }
    }
}
