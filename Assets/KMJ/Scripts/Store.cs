using System;
using UnityEngine;

public class Store : MonoBehaviour
{
    [Header("장비 테이블")]
    [SerializeField] private EquipmentTable normalEquipmentTable;
    [SerializeField] private EquipmentTable advancedEquipmentTable;
    [SerializeField] private EquipmentTable rareEquipmentTable;
    [SerializeField] private EquipmentTable heroEquipmentTable;
    [SerializeField] private EquipmentTable legendaryEquipmentTable;
    [SerializeField] private EquipmentTable mythicEquipmentTable;

    [Header("장비 뽑기 상위 등급 등장 확률")]
    [SerializeField, Range(0.0f, 1.0f)] private float equipmentAdvancedInNormal = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float equipmentRareInAdvanced = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float equipmentHeroInRare = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float equipmentLegendaryInHero = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float equipmentMythicInLegendary = 0.1f;

    [Header("카드 테이블")]
    [SerializeField] private EquipmentTable normalCardTable;
    [SerializeField] private EquipmentTable advancedCardTable;
    [SerializeField] private EquipmentTable rareCardTable;
    [SerializeField] private EquipmentTable heroCardTable;
    [SerializeField] private EquipmentTable legendaryCardTable;
    [SerializeField] private EquipmentTable mythicCardTable;

    [Header("카드 뽑기 상위 등급 등장 확률")]
    [SerializeField, Range(0.0f, 1.0f)] private float cardAdvancedInNormal = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float cardRareInAdvanced = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float cardHeroInRare = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float cardLegendaryInHero = 0.1f;
    [SerializeField, Range(0.0f, 1.0f)] private float cardMythicInLegendary = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region EquipmentPickUp

    public void StoreEquipmentNormalPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardAdvancedInNormal)
        {
            EquipmentPickUp(PickType.NORMAL);
        }
        else
        {
            EquipmentPickUp(PickType.ADVANCED);
        }
    }

    public void StoreEquipmentAdvancedPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardRareInAdvanced)
        {
            EquipmentPickUp(PickType.ADVANCED);
        }
        else
        {
            EquipmentPickUp(PickType.RARE);
        }
    }

    public void StoreEquipmentRarePickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardHeroInRare)
        {
            EquipmentPickUp(PickType.RARE);
        }
        else
        {
            EquipmentPickUp(PickType.HERO);
        }
    }

    public void StoreEquipmentHeroPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardLegendaryInHero)
        {
            EquipmentPickUp(PickType.HERO);
        }
        else
        {
            EquipmentPickUp(PickType.LEGENDARY);
        }
    }

    public void StoreEquipmentLegendaryPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardMythicInLegendary)
        {
            EquipmentPickUp(PickType.LEGENDARY);
        }
        else
        {
            EquipmentPickUp(PickType.MYTHIC);
        }
    }

    public void StoreEquipmentMythicPickUp()
    {
        EquipmentPickUp(PickType.MYTHIC);
    }

    private void EquipmentPickUp(PickType pickType)
    {
        Debug.Log("EquipmentPickUp" + pickType);

        switch (pickType)
        {
            case PickType.NORMAL:
                break;
            case PickType.ADVANCED:
                break;
            case PickType.RARE:
                break;
            case PickType.HERO:
                break;
            case PickType.LEGENDARY:
                break;
            case PickType.MYTHIC:
                break;
            default:
                break;
        }
    }

    #endregion EquipmentPickUp

    #region CardPickUp

    public void StoreCardNormalPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardAdvancedInNormal)
        {
            CardPickUp(PickType.NORMAL);
        }
        else
        {
            CardPickUp(PickType.ADVANCED);
        }
    }

    public void StoreCardAdvancedPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardRareInAdvanced)
        {
            CardPickUp(PickType.ADVANCED);
        }
        else
        {
            CardPickUp(PickType.RARE);
        }
    }

    public void StoreCardRarePickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardHeroInRare)
        {
            CardPickUp(PickType.RARE);
        }
        else
        {
            CardPickUp(PickType.HERO);
        }
    }

    public void StoreCardHeroPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardLegendaryInHero)
        {
            CardPickUp(PickType.HERO);
        }
        else
        {
            CardPickUp(PickType.LEGENDARY);
        }
    }

    public void StoreCardLegendaryPickUp()
    {
        if (UnityEngine.Random.Range(0f, 1f) > cardMythicInLegendary)
        {
            CardPickUp(PickType.LEGENDARY);
        }
        else
        {
            CardPickUp(PickType.MYTHIC);
        }
    }

    public void StoreCardMythicPickUp()
    {
        CardPickUp(PickType.MYTHIC);
    }

    private void CardPickUp(PickType pickType)
    {
        Debug.Log("CardPickUp" + pickType);

        switch (pickType)
        {
            case PickType.NORMAL:
                break;
            case PickType.ADVANCED:
                break;
            case PickType.RARE:
                break;
            case PickType.HERO:
                break;
            case PickType.LEGENDARY:
                break;
            case PickType.MYTHIC:
                break;
            default:
                break;
        }
    }
}

#endregion CardPickUp
