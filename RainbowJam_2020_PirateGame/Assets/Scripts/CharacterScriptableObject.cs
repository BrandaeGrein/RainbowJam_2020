using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/CharacterScriptableObject")]
public class CharacterScriptableObject : ScriptableObject
{
    public string CharacterName;

    [Tooltip("These are the starting dialogue options when the player begins an interaction with an NPC.")]
    public List<string> DialogueFriendshipLevel0;
    public List<string> DialogueFriendshipLevel1;
    public List<string> DialogueFriendshipLevel2;
    public List<string> DialogueFriendshipLevel3;

    [Tooltip("These are dialogue reactions after the player has chosen a particular interaction")]
    public string DialoguePrimaryHeartEvent;
    public string DialogueSecondaryHeartEvent;
    public string DialogueTertiaryHeartEvent;
    public string DialogueRejectedHeartEvent;

    [Tooltip("This is the conversation trigger that determines if the player is in conversing distance to this character.")]
    public ConversationTrigger conversationTrigger;
}
