using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kazegames.Utility
{
    [CreateAssetMenu(fileName ="New Level Configure", menuName ="Kazegames/Level Configure")]
    public class LevelConfigure : ScriptableObject
    {
#if UNITY_EDITOR
        public SceneAsset[] startupScenes;
        public SceneAsset[] levelScenes;

        public void Build()
        {
            Debug.Log("build LevelConfigure..");

            int len = levelScenes.Length;

            gameLevels = new GameLevel[len];
            for(int i=0; i<len; i++)
            {
                string name = levelScenes[i].name;
                string path = AssetDatabase.GetAssetPath(levelScenes[i]);
                string guid = AssetDatabase.AssetPathToGUID(path);

                GameLevel level = new GameLevel();
                level.GUID = guid;
                level.Name = name;
                level.BuildIndex = SceneUtility.GetBuildIndexByScenePath(path);

                gameLevels[i] = level;

                Debug.Log($"build level({level.Name}, {level.BuildIndex}): {path}");
            }
        }
#endif

        [System.Serializable]
        public class GameLevel
        {
            [SerializeField] string guid;
            [SerializeField] string name;
            [SerializeField] int buildIndex;

            public string GUID
            {
                get => guid;
                internal set { guid = value; }
            }

            public string Name
            {
                get => name;
                internal set { name = value; }
            }
            
            public int BuildIndex
            {
                get => buildIndex;
                internal set { buildIndex = value; }
            }
        }
        
        public GameLevel[] gameLevels;
    }
}
