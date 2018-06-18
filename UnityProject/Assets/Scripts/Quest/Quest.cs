using System;
using System.Collections.Generic;
using UnityEngine;

public enum Quests
{
    Start
}

[Serializable]
public class Quest
{
    public Quests Title;
    public string Description;
    public int RequiredLevel;
    public Reward Reward;
    public List<QuestType> Quests;

    [HideInInspector]
    public bool Done = false;
}