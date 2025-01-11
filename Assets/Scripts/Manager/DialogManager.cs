using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogManager : Singleton<DialogManager>
{
    public DialogueRunner dialogueRunner;
    public LineView lineView;
    public static CoinDisplay coinDisplay;
    public static bool result;
    private bool isDialogueEnd = false;

    void Awake()
    {
        base.Awake();
        coinDisplay = GetComponentInChildren<CoinDisplay>();
    }

    void Start()
    {
        StartDialogue("크리스_민트_첫만남");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: 선택지에서도 넘어가짐. 선택지에서는 선택지 눌러야 넘어가게 수정
            lineView.OnContinueClicked();
        }
    }

    public void StartDialogue(string filename)
    {
        dialogueRunner.StartDialogue(filename);
    }

    public void EndDialogue()
    {
        Debug.Log("End of dialogue");
        // TODO: dialog 종료 후 다른 yarn으로 넘어가기

        // TODO: 현재 상황에 따라 엔딩씬 띄우기
        if (!isDialogueEnd)
        {
            isDialogueEnd = true;
            StartCoroutine(StartNewDialogue("GameOver"));
        }
        else
        {
            // 엔딩 씬에서는 엔딩 리뷰 씬으로 넘어가기
            SceneManager.LoadScene("EndingReview");
        }

        // TODO: 엔딩 씬 이후에 엔딩 리뷰 씬으로 넘어가기
        // SceneManager.LoadScene("EndingReview");
    }

    private IEnumerator StartNewDialogue(string dialogueName)
    {
        // 대화가 종료될 때까지 기다림
        while (dialogueRunner.Dialogue.IsActive)
        {
            Debug.Log("Waiting for dialogue to end...");
            yield return null;  // 프레임마다 확인
        }

        // 대화가 종료되면 새로운 대화 시작
        Debug.Log("Starting new dialogue...");
        dialogueRunner.StartDialogue(dialogueName);
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

        // TODO: 코인 돌아가는 연출 추가하기, 이때 interaction은 끄기

        return Random.value < successRate;
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
