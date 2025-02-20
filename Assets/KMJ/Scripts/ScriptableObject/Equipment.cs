using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment/New Equipment")]
public class Equipment : ScriptableObject
{
    /// <summary>
    /// 장비 ID
    /// </summary>
    public int EquipmentID
    {
        get { return equipmentID;}
        private set { equipmentID = value; }
    }
    [SerializeField] private int equipmentID;
    /// <summary>
    /// 장비 이미지
    /// </summary>
    [SerializeField] private Sprite equipmentIcon;
    /// <summary>
    /// 장비 이름
    /// </summary>
    [SerializeField] private string equipmentName;
    /// <summary>
    /// 장비 등급
    /// </summary>
    [SerializeField] private Grade equipmentGrade;
    /// <summary>
    /// 장비 타입 
    /// </summary>
    [SerializeField] private ItemType equipmentType;
    /// <summary>
    /// 장비 보유 수량
    /// </summary>
    [SerializeField] private int equipmentStock = 0;
    /// <summary>
    /// 장비 레벨
    /// </summary>
    [SerializeField] private int equipmentLevel;
    /// <summary>
    /// 장비 효과
    /// </summary>
    [SerializeField] private int equipmentEffect;
    /// <summary>
    /// 장비 특수 효과
    /// </summary>
    [SerializeField] private int equipmentSkill;
}
