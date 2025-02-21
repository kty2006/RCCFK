using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{

    [field: SerializeField] public Queue<Card> BattleDeck { get; set; } = new();
    [field: SerializeField] public List<Card> DrowCards { get; set; } = new();
    [field: SerializeField] public List<GameObject> Deck { get; set; } = new();

    public Dictionary<GameObject, Card> FindCardDic { get; set; } = new();
    public void DrowAdd(Card card)
    {
        DrowCards.Add(card);
    }

    #region RandomCardList
    public Card FindCard(Sprite obj)
    {
        for (int i = 0; i < DrowCards.Count; i++)
        {
            if (DrowCards[i].Sprite() == obj)
            {
                return DrowCards[i];
            }
        }
        return null;
    }

    public Card CardGet(int index)
    {
        int num = 0;
        foreach (var card in BattleDeck)
        {
            if (num == index)
                return card;
            ++num;
        }
        return null; //널 반환은 X
    }
    #endregion

    #region Deck
    public void DeckAdd(GameObject card)
    {
        Deck.Add(card);
    }

    public void DeckReMove(GameObject card)
    {
        Deck.Remove(card);
    }

    public void AllDeckReMove()
    {
        for (int i = 0; i < Deck.Count; i++)
        {
            Destroy(Deck[i]);
        }
        Deck.Clear();
    }
    #endregion

    public void SettingDack()//필수 카드 새로 받을때마다 실행시켜줘야함
    {
        BattleDeck = new();
        DrowCards = new();
        Deck = new();
        FindCardDic = new();
    }
}
