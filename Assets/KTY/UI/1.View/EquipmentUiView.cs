using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

[DefaultExecutionOrder(0)]
public class EquipmentUiView : MonoBehaviour, IPointerClickHandler
{
    public GameObject Prefab;
    public GameObject EquipmentsGrid;
    public ImformationUI InformationUI;
    public EquipmentUiPresenter EquipmentUiPresenter;
    public List<SelectEquipment> SelectEquipmentss;
    private int equipmentsIndex;
    private Equipment currentEquipment;
    private GameObject clickedObject;
    private List<GameObject> equipmentsObj = new();

    public void Start()
    {
        EquipmentUiPresenter.EquipmentCreatSet();
    }

    public void EquipmentCreatSet(List<Equipment> equipments, List<Equipment> wearequipment)
    {
        for (int i = 0; i < equipments.Count; i++)
        {
            AddEquipmentUi(equipments[i].EquipmentIcon);
        }

        for (int i = 0; i < wearequipment.Count; i++)
        {
            for (int j = 0; j < SelectEquipmentss.Count; j++)
            {
                if (wearequipment[i].EquipmentType == SelectEquipmentss[j].itemTyp)
                {
                    Debug.Log($"{wearequipment[i].EquipmentType} : {SelectEquipmentss[j].itemTyp}");
                    if (SelectEquipmentss[j].TryGetComponent(out Image image))
                    {
                        image.sprite = wearequipment[i].EquipmentIcon;
                    }
                }
            }
        }
    }

    public void AddEquipmentUi(Sprite image)
    {
        GameObject equipmentObject = Instantiate(Prefab, EquipmentsGrid.transform);
        equipmentObject.transform.GetComponent<Image>().sprite = image;
        equipmentsObj.Add(equipmentObject);
    }

    public void OnInformationUi(List<Equipment> wearEquipments, Equipment equipment)
    {
        InformationUI.equipmentWearFunc = EquipmentWear;
        InformationUI.equipmentTakeOffFunc = EquipmentTakeOff;
        InformationUI.gameObject.SetActive(true);
        if (clickedObject.TryGetComponent(out SelectEquipment selectEquipment))
        {
            for (int i = 0; i < SelectEquipmentss.Count; i++)
            {
                for (int j = 0; j < wearEquipments.Count; j++)
                {
                    SelectEquipmentss[i].TryGetComponent(out Image image);
                    if (wearEquipments[j].EquipmentType == SelectEquipmentss[i].itemTyp && SelectEquipmentss[i].gameObject == clickedObject)
                    {
                        currentEquipment = equipment;
                        InformationUI.EquipmentName.text = wearEquipments[j].EquipmentName;
                        InformationUI.EquipmentRank.text = wearEquipments[j].EquipmentGrade.ToString();
                        break;
                    }
                }
            }
            InformationUI.WearCheck(true);
        }
        else
        {
            InformationUI.WearCheck(false);
            currentEquipment = equipment;
            InformationUI.EquipmentName.text = equipment.EquipmentName;
            InformationUI.EquipmentRank.text = equipment.EquipmentGrade.ToString();
            InformationUI.States = equipment.states;
        }
    }

    public void EquipmentWear()
    {
        InformationUI.gameObject.SetActive(false);
        for (int i = 0; i < SelectEquipmentss.Count; i++)
        {
            SelectEquipmentss[i].TryGetComponent(out Image image);
            if (currentEquipment.EquipmentType == SelectEquipmentss[i].itemTyp && image.sprite == null)
            {
                image.sprite = currentEquipment.EquipmentIcon;
                EquipmentUiPresenter.RemoveEquipment(equipmentsIndex);
                Destroy(equipmentsObj[equipmentsIndex]);
                equipmentsObj.RemoveAt(equipmentsIndex);
                break;
            }
        }
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);

    }

    public void EquipmentTakeOff(List<Equipment> wearEquipments)
    {
        InformationUI.gameObject.SetActive(false);
        for (int i = 0; i < SelectEquipmentss.Count; i++)
        {
            for (int j = 0; j < wearEquipments.Count; j++)
            {
                SelectEquipmentss[i].TryGetComponent(out Image image);
                if (wearEquipments[j].EquipmentType == SelectEquipmentss[i].itemTyp && SelectEquipmentss[i].gameObject == clickedObject)
                {
                    image.sprite = null;
                    EquipmentUiPresenter.AddEquipment(wearEquipments[j]);
                    EquipmentUiPresenter.WearEquipmentRemove(j);
                    return;
                }
            }
        }

    }

    public void FindEquipment(Image Equipment)
    {
        Image[] myEquipments = EquipmentsGrid.GetComponentsInChildren<Image>();
        for (int i = 0; i < myEquipments.Length; i++)
        {
            if (myEquipments[i] == Equipment)
            {
                equipmentsIndex = i;
                break;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickedObject = eventData.pointerCurrentRaycast.gameObject;
        if (clickedObject.TryGetComponent(out Image image) && clickedObject.transform.CompareTag("Equipment"))
        {
            FindEquipment(image);
            EquipmentUiPresenter.OnInformationUi(equipmentsIndex);
        }
    }

}
