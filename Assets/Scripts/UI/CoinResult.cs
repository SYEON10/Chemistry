using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinResult : MonoBehaviour
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject Coin;
    [SerializeField] private TextMeshProUGUI result;
    [SerializeField] private Button button;
    [SerializeField] private Animator animator;
    private bool spinned = false;

    void OnEnable()
    {
        resultPanel.SetActive(false);
    }

    public void ClickButton()
    {
        if (spinned)
        {
            spinned = false;
            Coin.SetActive(false);
        }
        else
        {
            spinned = true;
            StartCoroutine(ShowResult(DialogManager.result));
        }
    }
    
    public IEnumerator ShowResult(bool result)
    {
        button.interactable = false;
        if(result) animator.Play("Win");
        else animator.Play("Lose");
        yield return new WaitForSeconds(1.5f);
        if (result) this.result.text = "성공";
        else this.result.text = "실패";
        resultPanel.SetActive(true);
        button.interactable = true;
    }

}
