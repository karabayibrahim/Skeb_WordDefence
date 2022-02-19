using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
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
    void Start()
    {
        AdjustQuestionText();
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible==false&&Keyboard!=null)
        {
            TextCountText.text = AnswerInput.text.Length.ToString();
            if (Keyboard.status==TouchScreenKeyboard.Status.Done)
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
        if (answerdatastring.Contains(""+playerInput+"")&&answerdatastring.Length>0&&!_answerControl)
        {
            Debug.Log("Var");
            OldAnswers.Add(playerInput);
            var textCount = playerInput.Length;
            if (textCount>9)
            {
                textCount = 9;
            }
            GameManager.Instance.FirstSpawn = true;
            GameManager.Instance.Player.AnswerStatus(textCount);
            GameManager.Instance.SpawnManager.TowerSpawn(textCount, GameManager.Instance.Player.transform.position.z + 5f,playerInput);
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
        AnswerPanel.transform.DOScale(1.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
        AnswerPanel.GetComponent<Image>().DOColor(new Color(248f / 255f, 107f / 255f, 107f / 255f), 0.15f).SetLoops(2, LoopType.Yoyo);
        AnswerInput.text = "";
    }

    public void TrueAnswer()
    {
        AnswerPanel.transform.DOScale(1.5f, 0.15f).SetLoops(2, LoopType.Yoyo);
        AnswerPanel.GetComponent<Image>().DOColor(new Color(20f / 255f, 255f / 255f, 0f / 255f), 0.15f).SetLoops(2, LoopType.Yoyo);
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

    private void AdjustQuestionText() 
    {
        QuestText.text = GameManager.Instance.QuestionData.Questions[GameManager.Instance.LevelIndex];
    }
}
