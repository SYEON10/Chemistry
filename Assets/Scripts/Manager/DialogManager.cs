using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogManager : MonoBehaviour
{
    public DialogueRunner dialogueRunner;
    [SerializeField] private OptionsListView dialogueOption;
    [SerializeField] private Image stepImage;
    [SerializeField] private Image endingImage;
    [SerializeField] private PortraitDisplay portraitDisplay;
    public static CoinDisplay coinDisplay;
    public static bool result;
    private List<string> dialogList;
    private int dialogIndex = 0;
    private bool hasSeenEnding = false;
    private string fullText;

    void Awake()
    {
        coinDisplay = GetComponentInChildren<CoinDisplay>();
    }

    void Start()
    {
        Init();
        dialogIndex = 0;
        StartDialogue(dialogList[dialogIndex]);
    }

    void Init()
    {
        List<string> earlyDialog = new List<string>() { "크리스_은채_첫번째", "크리스_민트_첫번째", "은채_민트_첫번째" };
        List<string> midDialog = new List<string>() { "크리스_은채_두번째", "크리스_은채_세번째",
            "크리스_민트_두번째", "크리스_민트_세번째", "은채_민트_두번째", "은채_민트_세번째"}; // 중간거는 6개인데, 3~4개정도만 내보내기
        List<string> lastDialog = new List<string>() { "크리스_은채_네번째", "크리스_민트_네번째", "은채_민트_네번째" };
        dialogList = new List<string>();
        dialogList.AddRange(Shuffle(earlyDialog));
        List<string> temp = Shuffle(midDialog);
        dialogList.Add(temp[0]);
        dialogList.Add(temp[1]);
        dialogList.Add(temp[2]);
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
            GameResettor.needReset = true;
            hasSeenEnding = false;
            return;
        }

        // 게임 오버 체크
        if (GameManager.Instance.data.GetStat(StatEnum.lvChris).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.lvEun).value <= 5
            | GameManager.Instance.data.GetStat(StatEnum.lvMint).value <= 5)
        {
            hasSeenEnding = true;
            GameOver(GameOverType.seperation);
            return;
        }
        else if (GameManager.Instance.data.GetStat(StatEnum.charm).value <= 5)
        {
            hasSeenEnding = true;
            GameOver(GameOverType.confession);
            return;
        }

        if (dialogIndex >= dialogList.Count)
        {
            Debug.Log("Ending!");
            hasSeenEnding = true;
            // 조건에 따라 다른 엔딩
            int Chris_Eun = GameManager.Instance.data.GetStat(StatEnum.Chris_Eun).value;
            int Eun_Mint = GameManager.Instance.data.GetStat(StatEnum.Eun_Mint).value;
            int Mint_Chris = GameManager.Instance.data.GetStat(StatEnum.Mint_Chris).value;

            // 70 이상인 값과 해당 엔딩을 리스트로 저장
            var endingCandidates = new List<(int value, string ending)>
{
    (Chris_Eun, "크리스_은채_엔딩"),
    (Eun_Mint, "은채_민트_엔딩"),
    (Mint_Chris, "민트_크리스_엔딩")
}.Where(x => x.value > 70) // 70 이상인 경우만 필터링
             .ToList();

            // 70 이상인 값이 하나도 없으면 아무런 엔딩도 트리거하지 않음
            if (endingCandidates.Count == 0)
            {
                StartCoroutine(StartNewDialogue_Ending("모두_친구_엔딩"));
            }
            else
            {
                // 가장 큰 값을 가진 엔딩 선택
                var bestEnding = endingCandidates.OrderByDescending(x => x.value).First();
                StartCoroutine(StartNewDialogue_Ending(bestEnding.ending));
            }

            // if (Chris_Eun > 70)
            // {
            //     // 크리스 은채 엔딩
            //     StartCoroutine(StartNewDialogue_Ending("크리스_은채_엔딩"));
            // }
            // else if (Eun_Mint > 70)
            // {
            //     // 은채 민트 엔딩
            //     StartCoroutine(StartNewDialogue_Ending("은채_민트_엔딩"));
            // }
            // else if (Mint_Chris > 70)
            // {
            //     // 민트 크리스 엔딩
            //     StartCoroutine(StartNewDialogue_Ending("민트_크리스_엔딩"));
            // }
            // else
            // {
            //     // 모두 친구 엔딩
            //     StartCoroutine(StartNewDialogue_Ending("모두_친구_엔딩"));
            // }
            return;
        }

        // dialog 종료 후 다른 yarn으로 넘어가기
        StartCoroutine(StartNewDialogue(dialogList[dialogIndex]));
    }

    private IEnumerator StartNewDialogue(string dialogueName)
    {
        portraitDisplay.SetPortrait();
        stepImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f); // 시간텀 주기
        stepImage.gameObject.SetActive(false);
        dialogueRunner.StartDialogue(dialogueName);
    }

    private IEnumerator StartNewDialogue_Ending(string dialogueName)
    {
        portraitDisplay.SetPortrait();
        endingImage.gameObject.SetActive(true);

        TextMeshProUGUI textMeshProUGUI = endingImage.GetComponentInChildren<TextMeshProUGUI>();
        fullText = "친구들과 며칠간 연락이 끊겼다. \n그러던 어느날...";
        yield return StartCoroutine(TypeText(textMeshProUGUI));

        yield return new WaitForSeconds(1f); // 시간텀 주기
        endingImage.gameObject.SetActive(false);
        dialogueRunner.StartDialogue(dialogueName);
    }

    private IEnumerator TypeText(TextMeshProUGUI targetText)
    {
        targetText.text = "";
        foreach (char c in fullText)
        {
            targetText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void GameOver(GameOverType gameOverType)
    {
        switch (gameOverType)
        {
            case GameOverType.seperation:
                StartCoroutine(StartNewDialogue_Ending("게임오버1"));
                break;
            case GameOverType.confession:
                StartCoroutine(StartNewDialogue_Ending("게임오버2"));
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
