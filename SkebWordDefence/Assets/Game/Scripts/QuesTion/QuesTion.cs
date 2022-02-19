using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "QuestionData", menuName = "ScriptableObjects/QuestionData", order = 1)]
public class QuesTion : ScriptableObject
{
    public List<string> Questions = new List<string>();
}
