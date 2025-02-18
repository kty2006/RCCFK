using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

[CreateAssetMenu(fileName = "CardsDataBase", menuName = "Scriptable Objects/CardsDataBase")]
public class CardsDataBase : ScriptableObject
{
    [field: SerializeField] public Sprite[] PlayingCards { get; set; }//Ä¸½¶È­**

    [field: SerializeField] public string CardsImageName { get; set; }

    [field: SerializeField] public List<Card> AllCards { get; set; } = new();
    [field: SerializeField] public List<Card> CardDeck { get; set; } = new();
    [field: SerializeField] public List<Card> remainingCards { get; set; } = new();
    [field: SerializeField] public int DeckCount { get; set; }

    [ContextMenu("GetCards")]
    public void GetCardsData()
    {
        PlayingCards = Resources.LoadAll<Sprite>(CardsImageName);
    }

    [ContextMenu("CreateAllCards")]
    public void CreateAllCards()
    {
        for (int i = 0; i < PlayingCards.Length; i++)
        {
            AllCards.Add(new CardBuild().Image(PlayingCards[i]).Type(i).Build());
        }
    }

    [ContextMenu("CardDeckSet")]
    public void CardDeeckSet()
    {
        for (int i = 0; i < DeckCount; i++)
        {
            CardDeck.Add(AllCards[i]);
        }
    }


    public List<Card> UiGridSet()
    {
        remainingCards.Clear();
        bool doublRepeat = false;
        for (int i = 0; i < AllCards.Count; i++)
        {
            for (int j = 0; j < CardDeck.Count; j++)
            {
                if (AllCards[i].Sprite() == CardDeck[j].Sprite())
                {
                    doublRepeat = true;
                    break;
                }
            }
            if (doublRepeat)
            { doublRepeat = false; continue; }
            remainingCards.Add(AllCards[i]);
        }
        return remainingCards;
    }

    public void ChangeCard(List<int> Cards)
    {
        Debug.Log(Cards.Count);
        Card savCard = remainingCards[Cards[0]];
        remainingCards[Cards[0]] = CardDeck[Cards[1]];
        CardDeck[Cards[1]] = savCard;
    }
}