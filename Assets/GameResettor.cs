using UnityEngine;

public class GameResettor : MonoBehaviour
{
    public static bool needReset = false;

    void Start()
    {
        if (needReset)
        {
            FindAnyObjectByType<DialogManager>().Reset();
            needReset = false;
        }
    }
}
