using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
   fileName = "New Dialogue",
   menuName = "ScriptableObjects/Dialogue",
   order = 0
   )]

public class DialogueHolder : ScriptableObject
{
   public List<string> dialogues;
}
