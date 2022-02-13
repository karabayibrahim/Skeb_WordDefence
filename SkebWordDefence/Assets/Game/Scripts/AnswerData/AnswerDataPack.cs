using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnswerDataPack", menuName = "ScriptableObjects/AnswerDataPack", order = 1)]
public class AnswerDataPack :ScriptableObject
{
    public List<AnswerData> AnswerDatas = new List<AnswerData>();
}
