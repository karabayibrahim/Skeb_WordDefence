using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public TMP_InputField AnswerInput;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnswerControl()
    {
        string answerdatastring = GameManager.Instance.AnswerData.Answers.ToLower();
        string playerInput = AnswerInput.text.ToLower();
        if (answerdatastring.Contains(""+playerInput+""))
        {
            var textCount = playerInput.Length;
            if (textCount>9)
            {
                textCount = 9;
            }
            GameManager.Instance.Player.AnswerStatus(textCount);
            Debug.Log("Var");
        }
    }
}
