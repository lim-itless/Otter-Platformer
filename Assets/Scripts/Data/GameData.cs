using System;
using System.Collections.Generic;

[System.Serializable]
public class GameDataBase
{
    public string Id;
}

[System.Serializable]
public class CharacterData : GameDataBase
{
    public string Name;
    public string SkillList;
    public string UseWeaponId;
    public string BasicCostumeId;
}

[System.Serializable]
public class DialogueGroupData : GameDataBase
{
    public List<string> DialogueIdList;
}

[System.Serializable]
public class DialogueData : GameDataBase
{
    public string CharacterDataId;
    public string Description;
    public string NextDialogueId;
    public List<string> SelectionNameList;
    public List<string> SelectionDialogueIdList;
    public string TexturePath;
    public string VoicePath;
}