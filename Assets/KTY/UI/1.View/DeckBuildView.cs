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
    private GameObject selectCard;
    private List<int> cards = new();
    private List<GameObject> cardsObj = new();
    private List<GameObject> myDeck = new();
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
        if (cardGrid.name == MyDeckGrid.name)
        {
            for (int i = 0; i < cardList.Count; i++)
            {
                cardObject = cardGrid.transform.GetChild(i).gameObject;
                cardObject.transform.GetChild(0).GetComponent<Image>().sprite = cardList[i].Sprite();
            }
        }
        else
            foreach (Card card in cardList)
            {
                cardObject = Instantiate(CardPrefab, cardGrid.transform);
                cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite();
            }
    }

    public void CardChange(int index)
    {

        Sprite saveCard = cardsObj[0].transform.GetChild(0).GetComponent<Image>().sprite;
        cardsObj[0].transform.GetChild(0).TryGetComponent(out Image image);
        cardsObj[1].transform.GetChild(0).TryGetComponent(out Image image1);
        if (image1.sprite != null)
        {
            image.sprite = image1.sprite;
            image1.sprite = saveCard;
        }
        else if (image1.sprite == null)
        {
            Debug.Log("실행");
            MyDeckGrid.transform.GetChild(index).transform.GetChild(0).TryGetComponent(out Image myCard);
            Destroy(cardsObj[0]);
            myCard.sprite = saveCard;
        }
        Debug.Log(image1.sprite);
        cards.Clear();
        cardsObj.Clear();
    }

    public void CardRelease(GameObject releaseCard, int index)
    {
        GameObject cardObject;
        releaseCard.transform.GetChild(0).TryGetComponent(out Image myCard);
        cardObject = Instantiate(CardPrefab, RemainingCardGrid.transform);
        cardObject.transform.GetChild(0).GetComponent<Image>().sprite = myCard.sprite;
        bool isCheck = false;
        for (int i = 0; i <= index; i++)
        {
            MyDeckGrid.transform.GetChild(i).GetChild(0).TryGetComponent(out Image card);
            MyDeckGrid.transform.GetChild(i + 1).GetChild(0).TryGetComponent(out Image nextcard);
            if (releaseCard.transform == MyDeckGrid.transform.GetChild(i).transform)
            {
                isCheck = true;
                card.sprite = nextcard.sprite;

            }
            else if (isCheck)
            {
                card.sprite = nextcard.sprite;
            }
        }
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

    public void OnPointerClick(PointerEventData eventData) //수정필요* 문자열 비교 최적화
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        GameObject parentObject = null;
        Action<List<int>> changeFunc = null;
        Action<GameObject> releaseFunc = null;
        if (clickedObject.TryGetComponent(out Image image) && clickedObject.transform.CompareTag("Card"))
        {
            if (cards.Count == 0 && clickedObject.transform.parent.name == RemainingCardGrid.name)
            {
                parentObject = RemainingCardGrid;
                CardSizeSet(clickedObject, Vector3.one * 1.1f);
            }
            else if (cards.Count == 0 && clickedObject.transform.parent.name == MyDeckGrid.name)
            {
                parentObject = MyDeckGrid;
            }
            else if (cards.Count == 1 && cardsObj[0].transform.parent.name == RemainingCardGrid.name)
            {
                if (clickedObject.transform.parent.name == RemainingCardGrid.name)
                {
                    cards.Clear();
                    cardsObj.Clear();
                    parentObject = RemainingCardGrid;
                    CardSizeSet(clickedObject, Vector3.one);
                }
                else if (clickedObject.transform.parent.name == MyDeckGrid.name)
                {
                    parentObject = MyDeckGrid;
                    CardSizeSet(clickedObject, Vector3.one);
                    changeFunc = null;
                    changeFunc = DeckBuildPresenter.ChangeCard;
                    Debug.Log("실행");
                }
            }
            else if (cards.Count == 1 && cardsObj[0].transform.parent.name == MyDeckGrid.name)
            {
                if (clickedObject.transform.parent.name == MyDeckGrid.name)
                {
                    parentObject = MyDeckGrid;
                    changeFunc = null;
                    changeFunc = DeckBuildPresenter.CardRelease;
                    releaseFunc = DeckBuildPresenter.CardRelease;
                }
                else
                {
                    cards.Clear();
                    cardsObj.Clear();
                }
            }
            if (parentObject != null)
            {
                FindCard(image, parentObject);
                cardsObj.Add(clickedObject);
                changeFunc?.Invoke(cards);
                releaseFunc?.Invoke(clickedObject);
                changeFunc = null;
            }

        }
    }
}


