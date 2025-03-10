using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;
using UnityEditor;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    public CardsDataBase CardsData;
    public InGameData InGameData;
    public GameObject cards;
    public GameObject cardPrefab;
    public float Spaceing;
    public Unit costState;

    private Vector3[] CardsPos = new Vector3[20];
    private int drowCount = 5;
    private int drowIndex = 0;
    private GameObject clickedObject;
    private bool minCostCheck = false;

    public void Start()
    {
        InGameData.SettingDack();
        CardsPosSet();
        CreateCards(CardsData.CardDeck.Count);
        CardDrow(drowCount);
        Local.EventHandler.Register<int>(EnumType.EnemyDie, (enemyState) => { drowIndex = 0; InGameData.AllDeckReMove(); CardDrow(drowCount); });
        Local.EventHandler.Register<EnemyTurnSelect>(EnumType.EnemyTurnSelect, (turnselect) => { InGameData.AllDeckReMove(); CardDrow(drowCount); drowCount = 5; });
        Local.EventHandler.Register<int>(EnumType.CardDrowUp, (count) => { drowCount += count; });
    }

    public void CardDrow(int count)//카드 뽑기
    {
        Card card;
        for (int i = 0; i < count; i++)
        {
            card = CardsData.CardDeck[drowIndex];
            if (drowIndex < CardsData.CardDeck.Count - 1)
            {
                drowIndex++;
            }
            else
            {
                drowIndex = 0;
            }
            GameObject cardObject = Instantiate(cardPrefab, cards.transform);
            cardObject.transform.GetChild(0).GetComponent<Image>().sprite = card.Sprite();
            InGameData.DeckAdd(cardObject);
            InGameData.DrowAdd(card);
            InGameData.FindCardDic.Add(cardObject, card);
            //Debug.Log("카드뽑기");
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

    public async UniTask CardClickAni(GameObject card)//카드 애니메이션 실행
    {
        Vector3 targetPos = new(Screen.width / 2, (Screen.height / 2), 0);
        await SetAni(card.gameObject, targetPos, 1);
        if (clickedObject.TryGetComponent(out Animator animator))
            animator.SetTrigger("Rip");
        targetPos = new Vector3(Screen.width, 0, 0);
        await UniTask.WaitForSeconds(0.9f);
        await SetAni(card.gameObject, targetPos, 0.05f);
    }
    private async UniTask SetAni(GameObject card, Vector3 targetPos, float minScale)//카드 애니메이션 구현
    {
        float time = 0;
        Vector3 pos = card.transform.position;
        while (time < 1)
        {
            card.transform.position = Vector3.Lerp(pos, targetPos, time);
            card.transform.localScale = new(Mathf.Clamp(card.transform.localScale.x - 0.05f, minScale, 1), Mathf.Clamp(card.transform.localScale.y - 0.05f, minScale, 1), 0);

            time += Time.deltaTime;
            await UniTask.Yield();
        }

    }

    public void CreateCards(int count)//배틀에 들어갈 카드들 지정
    {
        InGameData.DrowCards.Clear();
        for (int i = 0; i < count; i++)
        {
            CardsData.CardDeck[i].SelectAction();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickObject = eventData.pointerCurrentRaycast.gameObject;

        Card card;
        if (clickObject.transform.GetChild(0).TryGetComponent(out Image image))
        {
            card = card = InGameData.FindCardDic[clickObject];
            if (costState.UnitStates.Cost - card.cost >= 0)
            {
                costState.UnitStates.Cost -= card.cost;
                this.clickedObject = clickObject;
                CardClickAni(clickedObject).Forget();
                Local.EventHandler.Invoke<Action>(EnumType.PlayerTurnAdd, card.Ability.AbillityFunc);
                InGameData.DeckReMove(clickObject);
                CardsSort();
            }
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
