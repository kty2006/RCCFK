using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentTable", menuName = "Table/Equipment Table")]
public class EquipmentTable : ScriptableObject
{
    public List<Equipment> items = new List<Equipment>();
}
