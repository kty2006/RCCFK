using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Attack, Defense, Recovery, Buff, Special }
public enum CardState { None, Drow }
[System.Serializable]
public class Card
{
    //특수카드같은경우 특수능력 실행 클래스를 만들고  종류를 enum으로 만들어 특수카드 능력 실행시 Card클래스에 상태만 특수능력 클래스 함수에 넘겨주어 타입에 따라 능력실행되게 
    [SerializeField] private Sprite sprite;//이미지
    [SerializeField] private CardType type;//타입
    public string name { get; set; }
    public AbillityWrapper Ability;//추가능력

    public Card(Sprite image, CardType type, AbillityWrapper ability, string name)
    {
        this.sprite = image;
        this.type = type;
        this.Ability = ability;
        this.name = name;
    }

    public Sprite Sprite()
    {
        return sprite;
    }

    public CardType Type()
    {
        return type;
    }

    public void SelectAction()
    {

        switch (type)
        {
            case <= CardType.Attack:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerAttack, Ability);
                break;
            case <= CardType.Defense:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerDefense, Ability);
                break;
            case <= CardType.Recovery:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerRecovery, Ability);
                break;
            case <= CardType.Buff:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.PlayerBuff, Ability);
                break;
        }
    }

}

[System.Serializable]
public class CardBuild
{
    [SerializeField] private Sprite sprite;//이미지
    [SerializeField] private CardType type;//타입
    [SerializeField] private AbillityWrapper ability = new();//추가능력
    [SerializeField] private string name;
    public CardBuild Image(Sprite sprite)
    {
        this.sprite = sprite;
        return this;
    }



    public CardBuild Type(int number)
    {
        switch (number)
        {
            case <= 12:
                type = CardType.Attack;
                break;
            case <= 25:
                type = CardType.Defense;
                ability.AbilityStates = number;
                break;
            case <= 38:
                type = CardType.Recovery;
                ability.AbilityStates = number;
                break;
            case <= 51:
                type = CardType.Buff;
                ability.AbilityStates = number;
                break;
        }
        return this;
    }

    public Card Build()
    {
        return new Card(sprite, type, ability, name);
    }
}
