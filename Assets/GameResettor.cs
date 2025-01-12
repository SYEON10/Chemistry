using UnityEngine;

public class GameResettor : MonoBehaviour
{
    public static bool needReset = false;

    void Start()
    {
        // Debug.Log($"needReset: {needReset}");
        // if (needReset)
        // {
        //     // FindAnyObjectByType<DialogManager>().Reset();
        //     needReset = false;
        // }
    }
}
