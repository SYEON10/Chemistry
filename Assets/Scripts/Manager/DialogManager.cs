using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;
    [SerializeField] private OptionsListView dialogueOption;
    [SerializeField] private TextMeshProUGUI stepText;
    public static CoinDisplay coinDisplay;
    public static bool result;
    private List<string> dialogList;
    private int dialogIndex = 0;
    private bool hasSeenEnding = false;

    void Awake()
    {
        base.Awake();
        coinDisplay = GetComponentInChildren<CoinDisplay>();
    }

    void Start()
    {
        Init();
        StartDialogue(dialogList[dialogIndex]);
    }

    void Init()
    {
        List<string> earlyDialog = new List<string>() { "크리스_은채_첫만남", "크리스_민트_첫만남", "은채_민트_첫만남" };
        // List<string> earlyDialog = new List<string>() { "Start" };
        List<string> midDialog = new List<string>() { };
        List<string> lastDialog = new List<string>() { };
        dialogList = new List<string>();
        dialogList.AddRange(Shuffle(earlyDialog));
        dialogList.AddRange(Shuffle(midDialog));
        dialogList.AddRange(Shuffle(lastDialog));
    }

    public static List<string> Shuffle(List<string> values)
    {
        System.Random rand = new System.Random();
        var shuffled = values.OrderBy(_ => rand.Next()).ToList();

        return shuffled;
    }

    public void StartDialogue(string filename)
    {
        dialogueRunner.StartDialogue(filename);
    }

    // Dialogue 종료 시 호출됨
    public void EndDialogue()
    {
        dialogIndex += 1;

        // 엔딩 씬 이후에 엔딩 리뷰 씬으로 넘어가기
        if (hasSeenEnding)
        {
            SceneManager.LoadScene("EndingReview");
            return;
        }

        // 게임 오버 체크
        if (GameManager.Instance.data.GetStat(StatEnum.mov).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.charm).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.mental).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.lvChris).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.lvEun).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.lvMint).value <= 5)
        {
            Debug.Log("Game over");
            hasSeenEnding = true;
            GameOver(GameOverType.confession);
            return;
        }

        if (dialogIndex >= dialogList.Count)
        {
            Debug.Log("Ending!");
            hasSeenEnding = true;
            // TODO: 70, 70, 70 모두 다 이상일 때는 최대값 출력하기
            // 조건에 따라 다른 엔딩
            if (GameManager.Instance.data.GetStat(StatEnum.Chris_Eun).value > 70)
            {
                // 크리스 은채 엔딩
            }
            else if (GameManager.Instance.data.GetStat(StatEnum.Eun_Mint).value > 70)
            {
                // 은채 민트 엔딩
            }
            else if (GameManager.Instance.data.GetStat(StatEnum.Mint_Chris).value > 70)
            {
                // 민트 크리스 엔딩
            }
            else
            {
                // 모두 친구 엔딩
                StartCoroutine(StartNewDialogue("GameOver"));
            }
            return;
        }

        // dialog 종료 후 다른 yarn으로 넘어가기
        StartCoroutine(StartNewDialogue(dialogList[dialogIndex]));
    }

    private IEnumerator StartNewDialogue(string dialogueName)
    {
        
        stepText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // 시간텀 주기
        stepText.gameObject.SetActive(false);
        dialogueRunner.StartDialogue(dialogueName);
    }

    public void GameOver(GameOverType gameOverType)
    {
        // TODO: 게임오버 유형별로 yarn 파일 만들고, 연결하기
        switch (gameOverType)
        {
            case GameOverType.seperation:
                StartCoroutine(StartNewDialogue("GameOver"));
                break;
            case GameOverType.unite:
                StartCoroutine(StartNewDialogue("GameOver"));
                break;
            case GameOverType.confession:
                StartCoroutine(StartNewDialogue("GameOver"));
                break;
            default:
                Debug.LogError("Invalid GameOverType");
                break;
        }
    }

    [YarnCommand("ChangeStat")]
    public static void ChangeStat(string statName, int amount)
    {
        Debug.Log($"{statName} changed {amount}");
        GameManager.Instance.data.GetStat(statName).ChangeStat(amount);
    }

    [YarnCommand("SetSound")]
    public static void SetSound(string source)
    {
        SoundManager.Instance.PlaySFX(source);
    }

    [YarnFunction("TossCoin")]
    public static bool TossCoin(float successRate)
    {
        Debug.Log("Tossing a coin...");
        if (successRate < 0 || successRate > 1)
        {
            Debug.LogError("Invalid success rate: " + successRate);
            return false;
        }

        return UnityEngine.Random.value < successRate;
    }

    [YarnFunction("TossCoin1")]
    public static bool TossCoin1(string statName)
    {
        result = coinDisplay.SpinCoin(statName);
        return result;
    }

    [YarnFunction("TossCoin2")]
    public static bool TossCoin2(int ratio1, string statName1, int ratio2, string statName2)
    {
        result = coinDisplay.SpinCoin(ratio1, statName1, ratio2, statName2);
        return result;
    }
}
