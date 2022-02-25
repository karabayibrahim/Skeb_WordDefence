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
    private float _fullDisntance;
    private float _newDistance;
    private float _fullFlag;
    private float _newFlag;
    private bool KeywordControl = false;
    private Vector2 IntextPos;
    [Header("AnswerPanel")]
    public GameObject AnswerPanel;
    [Header("QuestionPanel")]
    public TMP_Text QuestText;
    [Header("GamePanel")]
    public TMP_Text InText;
    public GameObject GamePanel;
    public TMP_Text LevelText;
    public Button RetryButton;
    public GameObject Wrong;
    public GameObject TrueA;
    public GameObject AnswerText;
    public Image Bar;
    public Image Flag;
    public Image Slice;
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
        LevelText.text = "LEVEL" + " " + (PlayerPrefs.GetInt("LevelIndex")+1).ToString();
        RestartButton.onClick.AddListener(RestartStatus);
        RetryButton.onClick.AddListener(RestartStatus);
        NextButton.onClick.AddListener(NextLevel);
        Finish.FinishAction += WinStatus;
        _fullDisntance = Vector3.Distance(GameManager.Instance.Player.transform.position, GameManager.Instance.Finish.transform.position);
        IntextPos = InText.rectTransform.position;
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
            StringLeght();
            if (GameManager.Instance.GameState==GameState.START&&!KeywordControl)
            {
                Keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
                InText.rectTransform.position = IntextPos;
            }
            //if (Keyboard.status == TouchScreenKeyboard.Status.Done)
            //{
            //    AnswerControl();
            //}
        }
        BarProgress();
    }

    public void KDeselect()
    {
        KeywordControl = true;
    }

    public void AnswerControl()
    {
        string answerdatastring = GameManager.Instance.AnswerDataPack.AnswerDatas[PlayerPrefs.GetInt("LevelIndex")].Answers.ToLower();
        string playerInput = AnswerInput.text.ToLower();
        AnswerCheck(playerInput);
        if (answerdatastring.Contains("" + playerInput + "") && playerInput.Length > 1 && !_answerControl)
        {
            //Debug.Log("Var");
            KeywordControl = true;
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
            //Debug.Log("Yok");
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
        KeywordControl = false;
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
        QuestText.text = GameManager.Instance.QuestionData.Questions[PlayerPrefs.GetInt("LevelIndex")];
    }

    public void FailStatus()
    {
        StartCoroutine(FailTimer());
    }
    public void WinStatus()
    {
        StartCoroutine(WinTimer());
    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("LevelIndex",PlayerPrefs.GetInt("LevelIndex") + 1);
        if (PlayerPrefs.GetInt("LevelIndex") > 17)
        {
            SceneManager.LoadScene("Level" + Random.Range(5, 17));
        }
        else
        {

            SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("LevelIndex"));
        }
    }

    public void StringLeght()
    {
        TextCountText.text = AnswerInput.text.Length.ToString();
    }

    private IEnumerator FailTimer()
    {
        yield return new WaitForSeconds(3f);
        GamePanel.SetActive(false);
        FailPanel.SetActive(true);
    }
    private IEnumerator WinTimer()
    {
        GamePanel.SetActive(false);
        GameManager.Instance.GameState = GameState.WIN;
        yield return new WaitForSeconds(8f);
        WinPanel.SetActive(true);
    }

    private void BarProgress()
    {
        _newDistance= Vector3.Distance(GameManager.Instance.Player.transform.position, GameManager.Instance.Finish.transform.position);
        Bar.fillAmount = Mathf.InverseLerp(0, _fullDisntance, _newDistance);
        Slice.rectTransform.anchorMin = new Vector2(Slice.rectTransform.anchorMin.x, Bar.fillAmount);
        Slice.rectTransform.anchorMax = new Vector2(Slice.rectTransform.anchorMax.x, Bar.fillAmount);
    }
}
