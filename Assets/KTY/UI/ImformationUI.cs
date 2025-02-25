using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImformationUI : MonoBehaviour, IPointerClickHandler
{
    public Button mountingBtn;
    public Button clearBtn;
    public Text EquipmentName;
    public Text EquipmentRank;
    public Text EquipmentExplain;
    public MyEquipmentData MyEquipmentData;
    public Action equipmentWearFunc { get; set; }
    public Action<List<Equipment>> equipmentTakeOffFunc { get; set; }
    public States States { get; set; }

    public void Awake()
    {
        mountingBtn.onClick.AddListener(() => { equipmentWearFunc?.Invoke(); StatesSum();  });
        clearBtn.onClick.AddListener(() => { equipmentTakeOffFunc?.Invoke(MyEquipmentData.WearEquipments); StatesMinus();  });
        gameObject.SetActive(false);
    }
    public void StatesSum()
    {
        Local.EventHandler.Invoke<States>(EnumType.PlayerAllStateSum, States);
    }

    public void StatesMinus()
    {
        Local.EventHandler.Invoke<States>(EnumType.PlayerAllStateMinus, States);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    public void WearCheck(bool iswear)
    {
        if (iswear)
        {
            mountingBtn.gameObject.SetActive(false);
            clearBtn.gameObject.SetActive(true);
        }
        else
        {
            mountingBtn.gameObject.SetActive(true);
            clearBtn.gameObject.SetActive(false);
        }

    }
}
