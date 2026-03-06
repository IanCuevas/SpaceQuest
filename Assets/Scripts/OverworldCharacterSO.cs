using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "OverworldCharacterSO", menuName = "Scriptable Objects/OverworldCharacterSO")]
public class OverworldCharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite portrait;
    public List<DialogTriggers> dialogLines;

}

[Serializable]
public class DialogTriggers
{
    public int dialogID;
    public bool isTriggered;
}


