using System;

public enum QuestTypes
{
    Kill, Collect
}

[Serializable]
public class QuestType
{
    public QuestTypes Type;
    public string Description;
    public int QuestTarget;
}