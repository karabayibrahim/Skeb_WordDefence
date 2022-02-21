using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public TMP_InputField AnswerInput;
    public TouchScreenKeyboard Keyboard;
    public List<string> OldAnswers = new List<string>();
    public TextMeshProUGUI TextCountText;
    private bool _answerControl = false;
    [Header("AnswerPanel")]
    public GameObject AnswerPanel;
    [Header("QuestionPanel")]
    public TMP_Text QuestText;
    [Header("GamePanel")]
    public GameObject GamePanel;
    public TMP_Text LevelText;
    public Button RetryButton;
    public GameObject Wrong;
    public GameObject TrueA;
    public GameObject AnswerText;
    [Header("FailPanel")]
    public GameObject FailPanel;
    public Button RestartButton;
    [Header("WinPanel")]
    public GameObject WinPanel;
    public Button NextButton;

    private Tween TrueTween;
    private Tween WrongTween;
    private Tween TrueColor;
    private Tween WrongColor;
    void Start()
    {
        AdjustQuestionText();
        LevelText.text = "LEVEL" + " " + SceneManager.GetActiveScene().buildIndex.ToString();
        RestartButton.onClick.AddListener(RestartStatus);
        RetryButton.onClick.AddListener(RestartStatus);
        Finish.FinishAction += WinStatus;
    }

    private void OnDisable()
    {
        RestartButton.onClick.RemoveListener(RestartStatus);
        RetryButton.onClick.RemoveListener(RestartStatus);
        Finish.FinishAction -= WinStatus;
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible == false && Keyboard != null)
        {
            TextCountText.text = AnswerInput.text.Length.ToString();
            if (Keyboard.status == TouchScreenKeyboard.Status.Done)
            {
                AnswerControl();
            }
        }
    }

    public void AnswerControl()
    {
        string answerdatastring = GameManager.Instance.AnswerDataPack.AnswerDatas[GameManager.Instance.LevelIndex].Answers.ToLower();
        string playerInput = AnswerInput.text.ToLower();
        AnswerCheck(playerInput);
        if (answerdatastring.Contains(" " + playerInput + " ") && playerInput.Length > 1 && !_answerControl)
        {
            Debug.Log("Var");
            OldAnswers.Add(playerInput);
            var textCount = playerInput.Length;
            if (textCount > 9)
            {
                textCount = 9;
            }
            GameManager.Instance.FirstSpawn = true;
            //GameManager.Instance.Player.AnswerStatus(textCount);
            GameManager.Instance.SpawnManager.TowerSpawn(textCount, GameManager.Instance.Player.transform.position.z + 5f, playerInput);
            TrueAnswer();
            AnswerInput.text = "";
        }
        else
        {
            WrongAnswer();
            Debug.Log("Yok");
        }
    }

    public void WrongAnswer()
    {
        TrueTween.Kill();
        TrueColor.Kill();
        Wrong.SetActive(true);
        AnswerText.SetActive(false);
        WrongTween=AnswerPanel.transform.DOScale(1.5f, 0.3f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            Wrong.SetActive(false);
            AnswerText.SetActive(true);
        });

        WrongColor=AnswerPanel.GetComponent<Image>().DOColor(new Color(248f / 255f, 107f / 255f, 107f / 255f), 0.3f).SetLoops(2, LoopType.Yoyo);
        AnswerInput.text = "";
    }

    public void TrueAnswer()
    {
        WrongTween.Kill();
        WrongColor.Kill();
        TrueA.SetActive(true);
        AnswerText.SetActive(false);
        TrueTween=AnswerPanel.transform.DOScale(1.5f, 0.3f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            TrueA.SetActive(false);
            AnswerText.SetActive(true);
        });
        TrueColor=AnswerPanel.GetComponent<Image>().DOColor(new Color(58f / 255f, 255f / 255f, 19f / 255f), 0.3f).SetLoops(2, LoopType.Yoyo);
    }

    public void OpenKeyboard()
    {
        Keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }

    private void AnswerCheck(string _playerInput)
    {
        foreach (var item in OldAnswers)
        {
            if (item == _playerInput)
            {
                _answerControl = true;
                break;
            }
            else
            {
                _answerControl = false;
            }
        }
    }

    private void RestartStatus()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void AdjustQuestionText()
    {
        QuestText.text = GameManager.Instance.QuestionData.Questions[GameManager.Instance.LevelIndex];
    }

    public void FailStatus()
    {
        StartCoroutine(FailTimer());
    }
    public void WinStatus()
    {
        StartCoroutine(WinTimer());
    }

    private IEnumerator FailTimer()
    {
        yield return new WaitForSeconds(3f);
        GamePanel.SetActive(false);
        FailPanel.SetActive(true);
    }
    private IEnumerator WinTimer()
    {
        GameManager.Instance.GameState = GameState.WIN;
        yield return new WaitForSeconds(3f);
        GamePanel.SetActive(false);
        WinPanel.SetActive(true);
    }
}
