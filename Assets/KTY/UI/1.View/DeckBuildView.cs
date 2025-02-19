using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

public class DeckBuildView : MonoBehaviour, IPointerClickHandler
{
    public GameObject MyDeckGrid;
    public GameObject RemainingCardGrid;
    public GameObject CardPrefab;
    public DeckBuildPresenter DeckBuildPresenter;
    private List<int> cards = new();
    private List<GameObject> cardsObj = new();
    private GameObject selectCard;
    public void Start()
    {
        DeckBuildPresenter.UiGridSet();
    }

    public void DeckUiView(List<Card> myDeckData, List<Card> remainingCards)
    {
        CardCreate(myDeckData, MyDeckGrid);
        CardCreate(remainingCards, RemainingCardGrid);
    }

    public void CardCreate(List<Card> cardList, GameObject cardGrid)
    {
        GameObject cardObject;
        foreach (Card card in cardList)
        {
            cardObject = Instantiate(CardPrefab, cardGrid.transform);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite();
        }
    }

    public void CardChange(List<int> cardList)
    {
        Sprite saveCard = cardsObj[0].transform.GetChild(0).GetComponent<Image>().sprite;
        cardsObj[0].transform.GetChild(0).GetComponent<Image>().sprite = cardsObj[1].transform.GetChild(0).GetComponent<Image>().sprite;
        cardsObj[1].transform.GetChild(0).GetComponent<Image>().sprite = saveCard;
        cards.Clear();
        cardsObj.Clear();

    }

    public void FindCard(Image card, GameObject ParentObj)
    {
        Image[] myDecks = ParentObj.GetComponentsInChildren<Image>();
        for (int i = 0; i < myDecks.Length; i++)
        {
            if (myDecks[i] == card)
            {
                cards.Add(i / 2);
                break;
            }
        }
    }

    public void CardSizeSet(GameObject clickedObject, Vector3 size)
    {
        if (selectCard != null) selectCard.transform.localScale = Vector3.one;
        selectCard = clickedObject;
        selectCard.transform.localScale = size;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        GameObject parentObject = null;
        Action<List<int>> changeFunc = null;
        if (clickedObject.TryGetComponent(out Image image) && clickedObject.transform.CompareTag("Card"))
        {
            if (cards.Count == 0 && clickedObject.transform.parent.name == RemainingCardGrid.name)
            {
                parentObject = RemainingCardGrid;
                CardSizeSet(clickedObject, Vector3.one * 1.3f);
            }
            else if (cards.Count == 1)
            {
                if (clickedObject.transform.parent.name == RemainingCardGrid.name)
                {
                    cards.Clear();
                    cardsObj.Clear();
                    parentObject = RemainingCardGrid;
                    CardSizeSet(clickedObject, Vector3.one * 1.3f);
                }
                else if (clickedObject.transform.parent.name == MyDeckGrid.name)
                {
                    parentObject = MyDeckGrid;
                    changeFunc = DeckBuildPresenter.ChangeCard;
                    CardSizeSet(clickedObject, Vector3.one);
                }
            }
            if (parentObject != null)
            {
                FindCard(image, parentObject);
                cardsObj.Add(clickedObject);
                changeFunc?.Invoke(cards);
                parentObject = null;
                changeFunc = null;
            }

        }
    }
}


