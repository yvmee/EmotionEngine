using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class QuestState
{
    [SerializeField] private string name;
    [SerializeField] private bool fulfilled;
    
    public string Name => name;
    public bool Fulfilled => fulfilled;
}

public class SideQuest : MonoBehaviour
{
    [SerializeField] private List<QuestState> questStates;
    private Dictionary<string, bool> _questStates = new Dictionary<string, bool>();

    private void Start()
    {
        foreach (var q in questStates)
        {
            _questStates.Add(q.Name, q.Fulfilled);
        }
    }
    
    public void TrySetQuest(string quest)
    {
        if (_questStates.ContainsKey(quest))
            _questStates[quest] = true;
    }

    public bool GetQuestState(string quest)
    {
        _questStates.TryGetValue(quest, out var value);
        return value;
    }
    
    public bool GetQuestStates(string quest)
    {
        _questStates.TryGetValue(quest, out var value);
        return value;
    }
}
