using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;
    [SerializeField] private CoinDisplay coinDisplay;

    void Start()
    {
        StartDialogue("크리스_민트_첫만남");
    }

    public void StartDialogue(string filename)
    {
        dialogueRunner.StartDialogue(filename);
    }

    [YarnCommand("ChangeStat")]
    public static void ChangeStat(string statName, int amount)
    {
        GameManager.Instance.data.GetStat(statName).ChangeStat(amount);
    }
    
    [YarnFunction ("TossCoin")]
    public static bool TossCoin(float successRate)
    {
        Debug.Log("Tossing a coin...");
        if (successRate < 0 || successRate > 1)
        {
            Debug.LogError("Invalid success rate: " + successRate);
            return false;
        }

        // TODO: 코인 돌아가는 연출 추가하기, 이때 interaction은 끄기
        
        return Random.value < successRate;
    }

    [YarnFunction("TossCoin1")]
    public static bool TossCoin1(string statName)
    {
        return true;
    }

    [YarnFunction("TossCoin2")]
    public static bool TossCoin2(int ratio1, string statName1, int ratio2, string statName2)
    {
        return true;
    }
}
