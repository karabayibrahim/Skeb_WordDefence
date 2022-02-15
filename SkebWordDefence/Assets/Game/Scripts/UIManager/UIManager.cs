using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TMP_InputField AnswerInput;
    public TouchScreenKeyboard Keyboard;
    public List<string> OldAnswers = new List<string>();

    private bool _answerControl = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchScreenKeyboard.visible==false&&Keyboard!=null)
        {
            if (Keyboard.status==TouchScreenKeyboard.Status.Done)
            {
                AnswerControl();
            }
        }
    }

    public void AnswerControl()
    {
        string answerdatastring = GameManager.Instance.AnswerData.Answers.ToLower();
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
            AnswerInput.text = "";
        }
        else
        {
            Debug.Log("Yok");
        }
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
}
