using UnityEngine;

[CreateAssetMenu(fileName = "NewEquipment", menuName = "Equipment/New Equipment")]
public class Equipment : ScriptableObject
{
    /// <summary>
    /// 장비 ID
    /// </summary>
    public int EquipmentID
    {
        get { return equipmentID; }
        private set { equipmentID = value; }
    }
    [SerializeField] private int equipmentID;

    /// <summary>
    /// 장비 이미지
    /// </summary>
    public Sprite EquipmentIcon
    {
        get { return equipmentIcon; }
        private set { equipmentIcon = value; }
    }
    [SerializeField] private Sprite equipmentIcon;

    /// <summary>
    /// 장비 이름
    /// </summary>
    public string EquipmentName
    {
        get { return equipmentName; }
        private set { equipmentName = value; }
    }
    [SerializeField] private string equipmentName;

    /// <summary>
    /// 장비 등급
    /// </summary>
    public Grade EquipmentGrade
    {
        get { return equipmentGrade; }
        private set { equipmentGrade = value; }
    }
    [SerializeField] private Grade equipmentGrade;

    /// <summary>
    /// 장비 타입 
    /// </summary>
    public ItemType EquipmentType
    {
        get { return equipmentType; }
        private set { equipmentType = value; }
    }
    [SerializeField] private ItemType equipmentType;

    /// <summary>
    /// 장비 보유 수량
    /// </summary>
    public int EquipmentStock
    {
        get { return equipmentStock; }
        private set { equipmentStock = value; }
    }
    [SerializeField] private int equipmentStock = 0;

    /// <summary>
    /// 장비 레벨
    /// </summary>
    public int EquipmentLevel
    {
        get { return equipmentLevel; }
        private set { equipmentLevel = value; }
    }
    [SerializeField] private int equipmentLevel;

    /// <summary>
    /// 장비 효과, 임시로 int
    /// </summary>
    public int EquipmentEffect
    {
        get { return equipmentEffect; }
        private set { equipmentEffect = value; }
    }
    [SerializeField] private int equipmentEffect;

    /// <summary>
    /// 장비 특수 효과, 임시로 int
    /// </summary>
    public int EquipmentSkill
    {
        get { return equipmentSkill; }
        private set { equipmentSkill = value; }
    }
    [SerializeField] private int equipmentSkill;

    [field: SerializeField] public States states { get; set; }
}
