using System;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [Header("장비 테이블")]
    [SerializeField] private EquipmentTable[] equipmentTables;

    [Header("장비 뽑기 확률")]
    [SerializeField, Range(0.0f, 1.0f)] private float[] equipmentUpgradeChances;

    [Header("카드 테이블")]
    [SerializeField] private EquipmentTable[] cardTables;

    [Header("카드 뽑기 확률")]
    [SerializeField, Range(0.0f, 1.0f)] private float[] cardUpgradeChances;

    [Header("뽑기 버튼")]
    [SerializeField] private Button[] singleDrawButtons;
    [SerializeField] private Button[] tenDrawButtons;

    [Header("UiPresenter")]
    [SerializeField] private EquipmentUiPresenter equipmentUiPresenter;
    void Start()
    {
        BindButtons();
    }

    private void BindButtons()
    {
        int half = singleDrawButtons.Length / 2;

        for (int i = 0; i < half; i++)
        {
            PickType pickType = (PickType)i;

            singleDrawButtons[i].onClick.AddListener(() => StoreEquipmentPickUp(pickType, 1));
            tenDrawButtons[i].onClick.AddListener(() => StoreEquipmentPickUp(pickType, 10));

            //singleDrawButtons[i + half].onClick.AddListener(() => StoreCardPickUp(pickType, 1));
            //tenDrawButtons[i + half].onClick.AddListener(() => StoreCardPickUp(pickType, 10));
        }
    }

    void Update() { }

    #region EquipmentPickUp

    private void EquipmentPickUpAnimation() { }

    public void StoreEquipmentPickUp(PickType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            StorePickUp(type, equipmentUpgradeChances, EquipmentPickUp);
        }
    }

    private void EquipmentPickUp(PickType pickType)
    {
        int indexByType = (int)pickType;

        if (indexByType < 0 || indexByType >= equipmentTables.Length)
        {
            Debug.LogWarning($"[EquipmentPickUp] Invalid PickType: {pickType}");
            return;
        }

        Debug.Log(indexByType);

        var itemList = equipmentTables[indexByType].items;
        if (itemList == null || itemList.Count == 0)
        {
            Debug.LogWarning($"[EquipmentPickUp] No items available for {pickType}");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, itemList.Count);
        Equipment selectedEquipment = itemList[randomIndex];
        equipmentUiPresenter.AddEquipment(selectedEquipment);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
        Debug.Log($"[Equipment PickUp] Type: {pickType}, Item: {selectedEquipment}");
    }

    #endregion EquipmentPickUp

    #region CardPickUp

    private void CardPickUpAnimation() { }

    public void StoreCardPickUp(PickType type, int count)
    {
        for (int i = 0; i < count; i++)
        {
            StorePickUp(type, cardUpgradeChances, CardPickUp);
        }
    }

    private void CardPickUp(PickType pickType)
    {
        int indexByType = (int)pickType;

        if (indexByType < 0 || indexByType >= cardTables.Length)
        {
            Debug.LogWarning($"[CardPickUp] Invalid PickType: {pickType}");
            return;
        }

        var itemList = cardTables[indexByType].items;
        if (itemList == null || itemList.Count == 0)
        {
            Debug.LogWarning($"[CardPickUp] No items available for {pickType}");
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, itemList.Count);
        Equipment selectedCard = itemList[randomIndex];
        Debug.Log($"[Card PickUp] Type: {pickType}, Item: {selectedCard}");
    }

    #endregion CardPickUp

    #region Common PickUp Logic

    private void StorePickUp(PickType currentType, float[] probabilities, Action<PickType> pickUpMethod)
    {
        int typeIndex = (int)currentType;
        PickType upgradedType = currentType;

        if (typeIndex < probabilities.Length && UnityEngine.Random.Range(0f, 1f) <= probabilities[typeIndex])
        {
            upgradedType = (PickType)(typeIndex + 1);
        }
        pickUpMethod(upgradedType);
    }

    #endregion Common PickUp Logic
}

// using System;
// using UnityEngine;

// public class Store : MonoBehaviour
// {
//     [Header("장비 테이블")]
//     [SerializeField] private EquipmentTable normalEquipmentTable;
//     [SerializeField] private EquipmentTable advancedEquipmentTable;
//     [SerializeField] private EquipmentTable rareEquipmentTable;
//     [SerializeField] private EquipmentTable heroEquipmentTable;
//     [SerializeField] private EquipmentTable legendaryEquipmentTable;
//     [SerializeField] private EquipmentTable mythicEquipmentTable;

//     [Header("장비 뽑기 상위 등급 등장 확률")]
//     [SerializeField, Range(0.0f, 1.0f)] private float equipmentAdvancedInNormal = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float equipmentRareInAdvanced = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float equipmentHeroInRare = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float equipmentLegendaryInHero = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float equipmentMythicInLegendary = 0.1f;

//     [Header("카드 테이블")]
//     [SerializeField] private EquipmentTable normalCardTable;
//     [SerializeField] private EquipmentTable advancedCardTable;
//     [SerializeField] private EquipmentTable rareCardTable;
//     [SerializeField] private EquipmentTable heroCardTable;
//     [SerializeField] private EquipmentTable legendaryCardTable;
//     [SerializeField] private EquipmentTable mythicCardTable;

//     [Header("카드 뽑기 상위 등급 등장 확률")]
//     [SerializeField, Range(0.0f, 1.0f)] private float cardAdvancedInNormal = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float cardRareInAdvanced = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float cardHeroInRare = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float cardLegendaryInHero = 0.1f;
//     [SerializeField, Range(0.0f, 1.0f)] private float cardMythicInLegendary = 0.1f;

//     // Start is called once before the first execution of Update after the MonoBehaviour is created
//     void Start()
//     {

//     }

//     // Update is called once per frame
//     void Update()
//     {

//     }

//     #region GPT

//     private void StorePickUp(PickType currentType, float probability, Func<PickType, bool> pickUpMethod)
//     {
//         PickType nextType = UnityEngine.Random.Range(0f, 1f) > probability ? currentType : (PickType)((int)currentType + 1);
//         pickUpMethod(nextType);
//     }

//     public void StoreEquipmentPickUp(PickType type)
//     {
//         float[] probabilities = { equipmentAdvancedInNormal, equipmentRareInAdvanced, equipmentHeroInRare, equipmentLegendaryInHero, equipmentMythicInLegendary };
//         if ((int)type < probabilities.Length)
//         {
//             // StorePickUp(type, probabilities[(int)type], EquipmentPickUp);
//         }
//         else
//         {
//             EquipmentPickUp(type);
//         }
//     }

//     public void StoreCardPickUp(PickType type)
//     {
//         float[] probabilities = { cardAdvancedInNormal, cardRareInAdvanced, cardHeroInRare, cardLegendaryInHero, cardMythicInLegendary };
//         if ((int)type < probabilities.Length)
//         {
//             // StorePickUp(type, probabilities[(int)type], CardPickUp);
//         }
//         else
//         {
//             CardPickUp(type);
//         }
//     }

//     private PickType GetNextPickType(PickType currentType)
//     {
//         if (Enum.IsDefined(typeof(PickType), (int)currentType + 1))
//         {
//             return (PickType)((int)currentType + 1);
//         }
//         return currentType;  // 최고 등급이면 변화 없음
//     }

//     #endregion GPT

//     #region EquipmentPickUp

//     private void EquipmentPickUpAnination()
//     {

//     }

//     public void StoreEquipmentNormalPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardAdvancedInNormal)
//         {
//             EquipmentPickUp(PickType.NORMAL);
//         }
//         else
//         {
//             EquipmentPickUp(PickType.ADVANCED);
//         }
//     }

//     public void StoreEquipmentAdvancedPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardRareInAdvanced)
//         {
//             EquipmentPickUp(PickType.ADVANCED);
//         }
//         else
//         {
//             EquipmentPickUp(PickType.RARE);
//         }
//     }

//     public void StoreEquipmentRarePickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardHeroInRare)
//         {
//             EquipmentPickUp(PickType.RARE);
//         }
//         else
//         {
//             EquipmentPickUp(PickType.HERO);
//         }
//     }

//     public void StoreEquipmentHeroPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardLegendaryInHero)
//         {
//             EquipmentPickUp(PickType.HERO);
//         }
//         else
//         {
//             EquipmentPickUp(PickType.LEGENDARY);
//         }
//     }

//     public void StoreEquipmentLegendaryPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardMythicInLegendary)
//         {
//             EquipmentPickUp(PickType.LEGENDARY);
//         }
//         else
//         {
//             EquipmentPickUp(PickType.MYTHIC);
//         }
//     }

//     public void StoreEquipmentMythicPickUp()
//     {
//         EquipmentPickUp(PickType.MYTHIC);
//     }

//     private void EquipmentPickUp(PickType pickType)
//     {
//         Debug.Log("EquipmentPickUp" + pickType);

//         switch (pickType)
//         {
//             case PickType.NORMAL:
//                 break;
//             case PickType.ADVANCED:
//                 break;
//             case PickType.RARE:
//                 break;
//             case PickType.HERO:
//                 break;
//             case PickType.LEGENDARY:
//                 break;
//             case PickType.MYTHIC:
//                 break;
//             default:
//                 break;
//         }
//     }

//     #endregion EquipmentPickUp

//     #region CardPickUp

//     private void CardPickUpAnination()
//     {

//     }

//     public void StoreCardNormalPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardAdvancedInNormal)
//         {
//             CardPickUp(PickType.NORMAL);
//         }
//         else
//         {
//             CardPickUp(PickType.ADVANCED);
//         }
//     }

//     public void StoreCardAdvancedPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardRareInAdvanced)
//         {
//             CardPickUp(PickType.ADVANCED);
//         }
//         else
//         {
//             CardPickUp(PickType.RARE);
//         }
//     }

//     public void StoreCardRarePickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardHeroInRare)
//         {
//             CardPickUp(PickType.RARE);
//         }
//         else
//         {
//             CardPickUp(PickType.HERO);
//         }
//     }

//     public void StoreCardHeroPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardLegendaryInHero)
//         {
//             CardPickUp(PickType.HERO);
//         }
//         else
//         {
//             CardPickUp(PickType.LEGENDARY);
//         }
//     }

//     public void StoreCardLegendaryPickUp()
//     {
//         if (UnityEngine.Random.Range(0f, 1f) > cardMythicInLegendary)
//         {
//             CardPickUp(PickType.LEGENDARY);
//         }
//         else
//         {
//             CardPickUp(PickType.MYTHIC);
//         }
//     }

//     public void StoreCardMythicPickUp()
//     {
//         CardPickUp(PickType.MYTHIC);
//     }

//     private void CardPickUp(PickType pickType)
//     {
//         Debug.Log("CardPickUp" + pickType);

//         switch (pickType)
//         {
//             case PickType.NORMAL:
//                 break;
//             case PickType.ADVANCED:
//                 break;
//             case PickType.RARE:
//                 break;
//             case PickType.HERO:
//                 break;
//             case PickType.LEGENDARY:
//                 break;
//             case PickType.MYTHIC:
//                 break;
//             default:
//                 break;
//         }
//     }
// }

// #endregion CardPickUp
