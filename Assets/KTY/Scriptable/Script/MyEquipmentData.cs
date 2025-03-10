using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyEquipmentData", menuName = "Scriptable Objects/MyEquipmentData")]
public class MyEquipmentData : ScriptableObject
{
    public List<Equipment> Equipments = new();
    public List<Equipment> WearEquipments = new();

    public void EquipmentAdd(Equipment equipment)
    {
        Equipments.Add(equipment);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public void WearEquipmentRemove(int index)
    {
        WearEquipments.RemoveAt(index);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public void EquipmentRemove(int index)
    {
        WearEquipments.Add(Equipments[index]);
        Equipments.Remove(Equipments[index]);
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public Equipment FindEquipment(int index)
    {
        if (index <= Equipments.Count)
        {
            Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
            return Equipments[index];
        }
        return Equipments[0];
    }
}
