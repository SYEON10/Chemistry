using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;
    public LineView lineView;
    public static CoinDisplay coinDisplay;
    public static bool result;
    private List<string> dialogList;
    private int dialogIndex = 0;
    private bool isEndingReady = false;
    private bool hasSeenEnding = false;

    void Awake()
    {
        base.Awake();
        coinDisplay = GetComponentInChildren<CoinDisplay>();
    }

    void Start()
    {
        Init();
        // StartDialogue(dialogList[dialogIndex]);
        StartDialogue("Start");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: 선택지에서도 넘어가짐. 선택지에서는 선택지 눌러야 넘어가게 수정
            lineView.OnContinueClicked();
        }
    }

    void Init()
    {
        List<string> earlyDialog = new List<string>() { };
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

    public void EndDialogue()
    {
        Debug.Log($"End of dialogue, isDialogueRunning= {dialogueRunner.IsDialogueRunning}, IsActive= {dialogueRunner.Dialogue.IsActive}");

        dialogIndex += 1;
        if (dialogIndex >= dialogList.Count)
        {
            Debug.Log("Ending!");
            // TODO: 게임 종료 후 현재 상황에 따라 엔딩씬 띄우기
            hasSeenEnding = true;
            // 조건에 따라 다른 엔딩
            StartCoroutine(StartNewDialogue("GameOver"));
        }

        // dialog 종료 후 다른 yarn으로 넘어가기
        StartCoroutine(StartNewDialogue(dialogList[dialogIndex]));

        // 엔딩 씬 이후에 엔딩 리뷰 씬으로 넘어가기
        if (hasSeenEnding)
        {
            SceneManager.LoadScene("EndingReview");
        }
    }

    private IEnumerator StartNewDialogue(string dialogueName)
    {
        // dialogueRunner.Stop();
        // 대화가 종료될 때까지 기다림
        for (int i = 0; i < 1000; i++)
        {
            if (!dialogueRunner.Dialogue.IsActive) break;
            Debug.Log("Waiting for dialogue to end...");
            yield return null;  // 프레임마다 확인
        }

        // 대화가 종료되면 새로운 대화 시작
        Debug.Log("Starting new dialogue...");
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
            case GameOverType.other:
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
