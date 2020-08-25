//#define DEBUG_FriendshipManager

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriendshipManager : MonoBehaviour
{
    //Static instance of the class
    private static FriendshipManager _friendship;

    private Dictionary<string, CharacterScriptableObject> AllCharactersDictionary;
    private Dictionary<string, int> AllFriendshipLevels;

    // Start is called before the first frame update
    void Start()
    {
        AllCharactersDictionary = new Dictionary<string, CharacterScriptableObject>();
        AllFriendshipLevels = new Dictionary<string, int>();
        GameObject[] allCharacters = GameObject.FindGameObjectsWithTag("Character");
        for(int i = 0; i< allCharacters.Length; i++)
        {
            CharacterScriptableObject character = allCharacters[0].GetComponent<CharacterSO_AttachmentScript>().AssignedCharacterValues;
            string characterName = character.CharacterName.ToLower();

            AllFriendshipLevels.Add(characterName, 0);
            AllCharactersDictionary.Add(characterName, character);

#if DEBUG_FriendshipManager
            Debug.Log("FriendshipManager::Start() Name of character: " + characterName + " is character number " + i + " in the scene");
#endif
        }
    }

    /// <summary>
    /// Very simply, this returns the opening lines of a character when the player starts any interaction at any friendship level.
    /// </summary>
    /// <param name="characterName">Simply the string that is the character's name. Character names are automatically made lowercase. </param>
    /// <returns>Returns the string for the dialogue option that comes next.</returns>
    private string ReturnRandomOpeningDialogueStatement(string characterName)
    {
        characterName = characterName.ToLower();
        System.Random random = new System.Random();
        CharacterScriptableObject character;
        string dialogue;
        if(AllCharactersDictionary.TryGetValue(characterName, out character))
        {
            int friendshipLevel;
            AllFriendshipLevels.TryGetValue(characterName, out friendshipLevel);

#if DEBUG_FriendshipManager
            Debug.Log("Friendship level with character " + characterName + " is " + friendshipLevel);
#endif

            switch (friendshipLevel)
            {
                case 0:
                    dialogue = character.DialogueFriendshipLevel0.ElementAt(random.Next(character.DialogueFriendshipLevel0.Count));
                    break;
                case 1:
                    dialogue = character.DialogueFriendshipLevel1.ElementAt(random.Next(character.DialogueFriendshipLevel1.Count));
                    break;
                case 2:
                    dialogue = character.DialogueFriendshipLevel2.ElementAt(random.Next(character.DialogueFriendshipLevel2.Count));
                    break;
                default://because character friendship levels can be greater than 3 in the logic, default is that friendship is maxed
                    dialogue = character.DialogueFriendshipLevel3.ElementAt(random.Next(character.DialogueFriendshipLevel3.Count));
                    break;
            }
            return dialogue;
        }
        else
        {
            Debug.LogError("FrienshipManager::ReturnRandomOpeningDialogueStatement() -- the dialogue is returning null. Please contact the devs.");
            return "Sorry?";
        }
    }

    /// <summary>
    /// Increases the friendship level as long as it's less than 3
    /// </summary>
    /// <param name="characterName">Name of character player is interacting with</param>
    /// <param name="increaseAmount">Value should either be 1, 2, or 3</param>
    private void IncreaseFriendshipLevel(string characterName, int increaseAmount)
    {
        characterName = characterName.ToLower();
        int friendshipLevel;
        if(AllFriendshipLevels.TryGetValue(characterName, out friendshipLevel))
        {
            if(friendshipLevel < 3)
            {
                AllFriendshipLevels[characterName] = friendshipLevel + increaseAmount;
#if DEBUG_FriendshipManager
                Debug.Log("FriendshipManager::IncreaseFriendshipLevel() -- For the character " + characterName + ", the frienship level is now at " + friendshipLevel);
#endif
            }
            else//friendship level has maxed out, this is where we'd put a handholding method call
            {

            }
        }
    }

    /// <summary>
    /// Returns the heart event dialogue of a character based on a character's name as the input,
    /// and the number of hearts earned by a particular event.
    /// </summary>
    /// <param name="characterName">The name of the character the player is interacting with. </param>
    /// <param name="HeartsEarned">The number of hearts earned by the player. Default is 0, so rejected event. Other values are 3 hearts earned
    /// for the primary event, 2 hearts earned for the secondary, and 1 heart earned for the tertiary.</param>
    /// <returns></returns>
    private string GetHeartEventDialogue(string characterName, int HeartsEarned = 0)
    {
        CharacterScriptableObject character;
        if(AllCharactersDictionary.TryGetValue(characterName, out character))
        {
            switch (HeartsEarned)
            {
                case 3:
                    IncreaseFriendshipLevel(characterName, 3);
                    return character.DialoguePrimaryHeartEvent;
                case 2:
                    IncreaseFriendshipLevel(characterName, 2);
                    return character.DialogueSecondaryHeartEvent;
                case 1:
                    IncreaseFriendshipLevel(characterName, 1);
                    return character.DialogueTertiaryHeartEvent;
                default:
                    return character.DialogueRejectedHeartEvent;
            } 
        }
        else
        {
            Debug.LogError("FriendshipManager::GetPrimaryHeartEventDialogue() -- character not found by name");
            return "Thank you.";
        }
    }

    static private FriendshipManager friendship
    {
        get
        {
            if (_friendship == null)
            {
                Debug.LogError("FriendshipManager:friendship getter - Attempt to get value of friendship before it has been set.");
                return null;
            }
            return _friendship;
        }
        set
        {
            if (_friendship != null)
            {
                Debug.LogError("FriendshipManager:friendship setter - Attempt to set friendship when it has already been set.");
            }
            _friendship = value;
        }
    }


}
