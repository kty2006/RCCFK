using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;
    public float Spaceing;
    private Vector3[] CardsPos = new Vector3[8];
    private int cardIndex;

    public void Start()
    {
        InGameData.SettingDack();
        CardsPosSet();
        CreateCards(CardsData.CardDeck.Count);
        CardDrow(CardsPos.Length);
        Local.EventHandler.Register<UnitDead>(EnumType.EnemyDie, (unitDead) => { InGameData.AllDeckReMove(); CreateCards(CardsData.CardDeck.Count); CardDrow(CardsPos.Length); });
    }

    public void CardDrow(int count)//카드 뽑기
    {
        Card card;
        for (int i = 0; i < count; i++)
        {
            card = InGameData.BattleDeck.Dequeue();
            GameObject cardObject = Instantiate(cardPrefab, cards.transform);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite();
            InGameData.DeckAdd(cardObject);
            InGameData.DrowAdd(card);
        }
        CardsSort();
    }

    public void CardsSort()//카드 정렬
    {
        for (int i = 0; i < InGameData.Deck.Count; i++)
        {
            InGameData.Deck[i].transform.position = CardsPos[i];
        }
    }

    public void CardsPosSet()//카드 위치 조정
    {
        for (int i = 0; i < CardsPos.Length; i++)
        {
            CardsPos[i] = new(cards.transform.position.x + (Spaceing * i), cards.transform.position.y);
        }
    }

    public async UniTask CardAni(GameObject card)//카드 애니메이션 실행
    {
        Vector3 targetPos = new(Screen.width / 2, (Screen.height / 2), 0);
        await SetAni(card.gameObject, targetPos, 1);
        targetPos = new Vector3(Screen.width, 0, 0);
        await UniTask.WaitForSeconds(0.5f);
        await SetAni(card.gameObject, targetPos, 0.1f);
    }

    private async UniTask SetAni(GameObject card, Vector3 targetPos, float minScale)//카드 애니메이션 구현
    {
        float time = 0;
        Vector3 pos = card.transform.position;
        while (time < 1)
        {
            card.transform.position = Vector3.Lerp(pos, targetPos, time);
            card.transform.localScale = new(Mathf.Clamp(card.transform.localScale.x - 0.01f, minScale, 1), Mathf.Clamp(card.transform.localScale.y - 0.01f, minScale, 1), 0);

            time += Time.deltaTime;
            await UniTask.Yield();
        }

    }

    public void CreateCards(int count)//배틀에 들어갈 카드들 지정
    {
        InGameData.BattleDeck.Clear();
        InGameData.DrowCards.Clear();
        for (int i = 0; i < count; i++)
        {
            CardsData.CardDeck[i].SelectAction();
            InGameData.BattleDeck.Enqueue(CardsData.CardDeck[i]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Card card;
        if (clickedObject.transform.GetChild(0).TryGetComponent(out Image image))
        {
            card = InGameData.FindCard(image.sprite);
            CardAni(clickedObject).Forget();
            Local.EventHandler.Invoke<Action>(EnumType.PlayerTurnAdd, card.Ability.AbillityFunc);
            InGameData.DeckReMove(clickedObject);
            CardDrow(1);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        eventData.pointerEnter.transform.localScale = Vector3.one * 1.1f;//하드코딩***
    }
}
