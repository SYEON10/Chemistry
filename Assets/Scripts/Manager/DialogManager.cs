using System.Collections;
using UnityEngine;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;
    [SerializeField] private CoinDisplay coinDisplay;

    void Start()
    {
        StartDialogue("Start");
        dialogueRunner.AddCommandHandler("ChangeStat", (System.Action<string, int>)ChangeStat);
    }

    public void StartDialogue(string filename)
    {
        dialogueRunner.StartDialogue(filename);
    }

    void ChangeStat(string statName, int amount)
    {
        Debug.Log("Changing " + statName + " by " + amount);
        switch (statName)
        {
            case "mov":
                GameManager.Instance.data.stats.mov.ChangeStat(amount);
                break;
            case "charm":
                GameManager.Instance.data.stats.charm.ChangeStat(amount);
                break;
            case "mental":
                GameManager.Instance.data.stats.mental.ChangeStat(amount);
                break;
            case "lvChris":
                GameManager.Instance.data.stats.lvChris.ChangeStat(amount);
                break;
            case "lvEun":
                GameManager.Instance.data.stats.lvEun.ChangeStat(amount);
                break;
            case "lvMint":
                GameManager.Instance.data.stats.lvMint.ChangeStat(amount);
                break;
            case "Chris_Eun":
                GameManager.Instance.data.stats.Chris_Eun.ChangeStat(amount);
                break;
            case "Eun_Mint":
                GameManager.Instance.data.stats.Eun_Mint.ChangeStat(amount);
                break;
            case "Mint_Chris":
                GameManager.Instance.data.stats.Mint_Chris.ChangeStat(amount);
                break;
            default:
                Debug.LogError("Invalid stat name: " + statName);
                break;
        }
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
}
