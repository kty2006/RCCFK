using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;
using Cysharp.Threading.Tasks;

public class CardManager : MonoBehaviour, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    //이 클래스에 MVP패턴을 안쓴이유 ui조작을 입력조작 만 하기 때문에 이 조작은 Presenter가 Model과 연결시켜줄부분이 없어 CardUi는 Mvp패턴을 사용하지 않았다.
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
        CreateCards(CardsData.PlayingCards.Length);
        CardDrow(CardsPos.Length);
    }

    public void CardDrow(int count)//오브젝트풀링으로 고쳐야함 //카드데이터 내에서 남아있는 카드를 뽑는 함수
    {
        int currentCount = count;
        for (int i = 0; i < currentCount; i++)
        {
            if (InGameData.CardDBContains(cardIndex))
            {
                GameObject cardObject = Instantiate(cardPrefab, cards.transform);
                cardObject.transform.GetChild(0).GetComponent<Image>().sprite = CardsData.PlayingCards[cardIndex]; //캡슐화 해야함***
                InGameData.CardsAdd(new CardBuild().Image(cardObject.GetComponent<Image>()).Type(cardIndex).Build());
                InGameData.CardDBReMove(cardIndex);
                InGameData.DeckAdd(cardObject);
                cardIndex += 1;
            }
            else
            {
                Mathf.Clamp(currentCount++, 1, 10);//하드코딩
            }
        }
        CardsSort();
    }

    public void CardsSort()
    {
        for (int i = 0; i < InGameData.deckUi.Count; i++)
        {
            InGameData.deckUi[i].transform.position = CardsPos[i];
        }
    }

    public void CardsPosSet()
    {
        for (int i = 0; i < CardsPos.Length; i++)
        {
            CardsPos[i] = new(cards.transform.position.x + (Spaceing * i), cards.transform.position.y);

        }
    }

    public async UniTask CardAni(GameObject card)
    {
        Vector3 targetPos = new(Screen.width / 2, (Screen.height / 2), 0);
        await SetAni(card, targetPos, 1);
        targetPos = new Vector3(Screen.width, 0, 0);
        await UniTask.WaitForSeconds(0.5f);
        await SetAni(card, targetPos, 0.1f);
    }

    private async UniTask SetAni(GameObject card, Vector3 targetPos, float minScale)
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

    public void CreateCards(int count)//카드 개수 데이터 저장
    {
        for (int i = 0; i < count; i++)
        {
            InGameData.CardDBAdd(i);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        Card card;
        if (clickedObject.TryGetComponent(out Image image))
        {
            card = InGameData.FindCard(image);
            if (card.State() == CardState.Drow)
            {
                //clickedObject.transform.localPosition += Vector3.up * 100;//하드코딩***
                CardAni(clickedObject).Forget();
                Local.EventHandler.Invoke<Action>(EnumType.PlayerTurnAdd, card.Ability.AbillityFunc);
                InGameData.DeckUiReMove(clickedObject);
                CardDrow(1);

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
