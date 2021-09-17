using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Kazegames.Utility;
using System.Collections.Generic;

namespace Kazegames.Editor
{
    public class LevelConfigureBuilder : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }

        [MenuItem("Kazegames/Build Levels")]
        private static void BuildLevels()
        {
            LevelConfigure config = Resources.Load<LevelConfigure>("Level Configure");

            config.Build();
        }

        public void OnPreprocessBuild(BuildReport report)
        {
            if (EditorUtility.DisplayDialog("Did you use LevelConfigure ?", "", "Yes", "No"))
            {
                LevelConfigure config = Resources.Load<LevelConfigure>("Level Configure");

                if(config == null)
                {
                    throw new System.NullReferenceException("Not found the LevelConfigure file, you should create a LevelConfigure file named \"Level Configure\" in UNITY Resources folder");
                }

                // change building scenes
                Debug.Log("change building scenes..");

                List<EditorBuildSettingsScene> buildingList = new List<EditorBuildSettingsScene>();

                if(config.startupScenes != null && config.startupScenes.Length > 0)
                {
                    foreach(var s in config.startupScenes)
                    {
                        string path = AssetDatabase.GetAssetPath(s);
                        string guid = AssetDatabase.AssetPathToGUID(path);

                        buildingList.Add(new EditorBuildSettingsScene(guid, true));

                        Debug.Log($"added scene({s.name}): {path}");
                    }
                }

                if(config.levelScenes != null && config.levelScenes.Length > 0)
                {
                    foreach (var s in config.levelScenes)
                    {
                        string path = AssetDatabase.GetAssetPath(s);
                        string guid = AssetDatabase.AssetPathToGUID(path);

                        buildingList.Add(new EditorBuildSettingsScene(guid, true));

                        Debug.Log($"added scene({s.name}): {path}");
                    }
                }

                EditorBuildSettings.scenes = buildingList.ToArray();

                // build level configure
                config.Build();
            }
        }
    }
}
