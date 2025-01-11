using UnityEditor;
using UnityEditor.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameData data = new GameData();

    [InitializeOnLoad]
    public class EditorStartInit
    {
        static EditorStartInit()
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }
    }
}
