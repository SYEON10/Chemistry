using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    public GameData data = new GameData();

    public void InitData()
    {
        data = new GameData();
    }
}
