using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InGameData", menuName = "Scriptable Objects/InGameData")]
public class InGameData : ScriptableObject
{

    [SerializeField] private List<Card> randomCardList = new();
    [field: SerializeField] public List<GameObject> deckUi { get; set; } = new();
    [SerializeField] private Dictionary<int, int> cardDB = new();

    #region RandomCardList
    public Card FindCard(Image obj)
    {
        foreach (var card in randomCardList)
        {
            if (card.Image().Equals(obj))
            {
                return card;
            }
        }
        return null;
    }

    public void CardsAdd(Card card)
    {
        randomCardList.Add(card);
    }

    public Card CardGet(int index)
    {
        return randomCardList[index];
    }
    #endregion

    #region Deck
    public void DeckAdd(GameObject cardUI)
    {
        deckUi.Add(cardUI);
    }


    public InGameData DeckUiReMove(GameObject card)
    {
        deckUi.Remove(card);
        return this;
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

    #region CardDB
    public Dictionary<int, int> CardDBGet()
    {
        return cardDB;
    }

    public void CardDBAdd(int num)
    {
        cardDB.Add(num, 1);

    }

    public void CardDBReMove(int num)
    {
        if (cardDB[num] > 0)
            cardDB[num] -= 1;
    }
    public bool CardDBContains(int num)
    {
        if (cardDB[num] == 0) return false;
        return true;
    }
    #endregion


    public void SettingDack()
    {
        randomCardList = new();
        deckUi = new();
    }
}
