using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUpdater : MonoBehaviour
{
    public CharacterType _type;
    public PlayerStats _playerStats;
    public Dialogue DayDialogueWantedLevel1;
    public Dialogue DayDialogueWantedLevel2;
    public Dialogue DayDialogueWantedLevel3;
    public Dialogue NightDialogueWantedLevel1;
    public Dialogue NightDialogueWantedLevel2;
    public Dialogue NightDialogueWantedLevel3;
    private TimeController _timeController;
    private DialogueTrigger _dialogueTrigger; 
    void Awake()
    {
        _timeController = FindObjectOfType<TimeController>();
        _dialogueTrigger = GetComponent<DialogueTrigger>();
        StartCoroutine(CustomUpdate());
    }

    IEnumerator CustomUpdate()
    {
        switch (_type)
        {
            case CharacterType.Bofia:

                
                
                break;
            
            case CharacterType.Dealer:

                
                
                break;
            
            case CharacterType.Dom:

                
                
                break;
            
            case CharacterType.Guna:

                
                
                break;
            
            case CharacterType.Velha:
                
                
                break;
            
            case CharacterType.SrTono:

                
                break;
        }
        
        yield return new WaitForSeconds(0.1f);
        yield return null;
    }

    public enum CharacterType
    {
        Bofia,
        Velha,
        SrTono,
        Guna,
        Dom,
        Dealer
    }
}
