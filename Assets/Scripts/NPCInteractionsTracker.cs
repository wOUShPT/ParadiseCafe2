using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionsTracker : MonoBehaviour
{
    public CharacterType Bofia;
    public CharacterType Velha;
    public CharacterType Guna;
    public CharacterType Adriano;
    public CharacterType SrTono;
    public CharacterType Dealer;
    public CharacterType Dom;
}

public struct CharacterType
{
    public int numberOfInteractions;
    public int numberOfRapeInteractions;
    public int numberOfStealInteractions;
}
