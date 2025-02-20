using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType { Attack, Defense, Recovery, Buff, Special }
public enum CardState { None, Drow }
[System.Serializable]
public class Card
{
    private Sprite sprite;//이미지
    private CardType type;//타입
    public AbillityWrapper Ability;//추가능력

    public Card(Sprite image, CardType type, AbillityWrapper ability)
    {
        this.sprite = image;
        this.type = type;
        this.Ability = ability;
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
public class CardBuild //빌더 패턴
{
    private Sprite sprite;//이미지
    private CardType type;//타입
    private AbillityWrapper ability = new();//추가능력
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
        return new Card(sprite, type, ability);
    }
}
