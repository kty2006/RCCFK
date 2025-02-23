using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

[CreateAssetMenu(fileName = "CardsDataBase", menuName = "Scriptable Objects/CardsDataBase")]
public class CardsDataBase : ScriptableObject
{
    [field: SerializeField] public int MaxCost = 100;
    [field: SerializeField] public int CurrentCost = 0;
    [field: SerializeField] public Card[] CardData { get; set; }
    [field: SerializeField] public List<Card> AllCards { get; set; } = new();
    [field: SerializeField] public List<Card> CardDeck { get; set; } = new();
    [field: SerializeField] public List<Card> remainingCards { get; set; } = new();
    [field: SerializeField] public int AllCardCount { get; set; }
    [field: SerializeField] public int DeckCount { get; set; }

    [ContextMenu("CreateAllCards")]
    public void CreateAllCards()
    {
        int count = 0;
        int index = 0;
        while (count < 50)
        {
            count++;
            if (index == CardData.Length)
                index = 0;
            if (index < CardData.Length)
                AllCards.Add(CardData[index]);
            index++;
        }

    }

    [ContextMenu("CardDeckSet")]
    public void CardDeeckSet()
    {
        CardDeck.Clear();
        for (int i = 0; i < DeckCount; i++)
        {
            if (CurrentCost + AllCards[i].cost <= 100)
            {
                CardDeck.Add(AllCards[i]);
                CurrentCost += AllCards[i].cost;
            }
            else
                break;
        }

    }

    public List<Card> UiGridSet()
    {
        remainingCards = AllCards.ToList();
        bool doublRepeat = false;
        for (int i = 0; i < CardDeck.Count; i++)
        {
            for (int j = 0; j < remainingCards.Count; j++)
            {
                if (remainingCards[j].Sprite() == CardDeck[i].Sprite())
                {
                    doublRepeat = true;
                    remainingCards.RemoveAt(i);
                    break;
                }
            }
            if (doublRepeat)
            { doublRepeat = false; continue; }
        }
        return remainingCards;
    }
    public bool ChangeCard(List<int> Cards)
    {
        if (CardDeck.Count > Cards[1] && remainingCards.Count > Cards[0])
        {
            if (CardDeck.Count > Cards[1])
            {
                if (CurrentCost - CardDeck[Cards[1]].cost + remainingCards[Cards[0]].cost <= MaxCost)
                {
                    CurrentCost -= CardDeck[Cards[1]].cost;
                    CurrentCost += remainingCards[Cards[0]].cost;
                    Card savCard = remainingCards[Cards[0]];
                    remainingCards[Cards[0]] = CardDeck[Cards[1]];
                    CardDeck[Cards[1]] = savCard;
                    return true;
                }
            }
            else
            {
                if (CurrentCost + remainingCards[Cards[0]].cost <= MaxCost)
                {
                    CurrentCost += remainingCards[Cards[0]].cost;
                    CardDeck.Add(remainingCards[Cards[0]]);
                    remainingCards.Remove(remainingCards[Cards[0]]);
                    return true;
                }
            }
        }
        return false;
    }

    public void CardRelease(List<int> Cards)
    {
        if (CardDeck.Count > Cards[0])
        {
            remainingCards.Add(CardDeck[Cards[0]]);
            CurrentCost -= CardDeck[Cards[0]].cost;
            CardDeck.Remove(CardDeck[Cards[0]]);
        }
    }
}