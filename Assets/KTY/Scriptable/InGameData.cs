using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{

    [field: SerializeField] public Queue<Card> battleDeck { get; set; } = new();
    [field: SerializeField] public List<Card> drowCards { get; set; } = new();
    [field: SerializeField] public List<GameObject> deckUi { get; set; } = new();

    public void DrowAdd(Card card)
    {
        drowCards.Add(card);
    }

    #region RandomCardList
    public Card FindCard(Sprite obj)
    {
        for (int i = 0; i < drowCards.Count; i++)
        {
            if (drowCards[i].Sprite() == obj)
            {
                Debug.Log(drowCards[i].Sprite());
                return drowCards[i];
            }
        }
        return null;
    }

    public Card CardGet(int index)
    {
        int num = 0;
        foreach (var card in battleDeck)
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
        deckUi.Add(card);
    }


    public void DeckReMove(GameObject card)
    {
        deckUi.Remove(card);
    }

    public void AllDeckReMove()
    {
        for (int i = 0; i < deckUi.Count; i++)
        {
            Destroy(deckUi[i]);
        }
        deckUi.Clear();
    }

    public int DeckAllReMove()//오브젝트풀링으로 고쳐야함
    {
        int count = deckUi.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(deckUi[i]);
        }
        deckUi.Clear();
        return count;
    }
    #endregion

    public void SettingDack()//필수 카드 새로 받을때마다 실행시켜줘야함
    {
        battleDeck = new();
        drowCards = new();
        deckUi = new();
    }
}
