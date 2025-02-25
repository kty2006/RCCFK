using UnityEngine;

public class EquipmentUiPresenter : MonoBehaviour
{
    public EquipmentUiView EquipmentUiView;
    public MyEquipmentData EquipmentData;

    public void EquipmentCreatSet()
    {
        EquipmentUiView.EquipmentCreatSet(EquipmentData.Equipments, EquipmentData.WearEquipments);
    }

    public void AddEquipment(Equipment equipment)
    {
        EquipmentData.EquipmentAdd(equipment);
        EquipmentUiView.AddEquipmentUi(equipment.EquipmentIcon);
    }

    public void WearEquipmentRemove(int index)
    {
        EquipmentData.WearEquipmentRemove(index);
    }

    public void RemoveEquipment(int index)
    {
        EquipmentData.EquipmentRemove(index);
    }

    public void OnInformationUi(int index)
    {
        EquipmentUiView.OnInformationUi(EquipmentData.WearEquipments,EquipmentData.FindEquipment(index));
    }
}
