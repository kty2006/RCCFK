using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    public CardsDataBase CardsDataBase;
    public MyEquipmentData EquipmentData;
    public void Awake()
    {
        Application.targetFrameRate = 60;

        Local.EventHandler.Register<DataSave>(EnumType.SaveData, (datasave) => { datasave.MyDeck = CardsDataBase.CardDeck; Local.Json.ReadJson(); });
        Local.EventHandler.Register<DataSave>(EnumType.SaveData, (datasave) => { datasave.Stage = Local.Stage; Local.Json.ReadJson(); });
        Local.EventHandler.Register<DataSave>(EnumType.SaveData, (datasave) => { datasave.Equipments = EquipmentData.Equipments; Local.Json.ReadJson(); });
        Local.EventHandler.Register<DataSave>(EnumType.SaveData, (datasave) => { datasave.SelectEquipments = EquipmentData.WearEquipments; Local.Json.ReadJson(); });

        string filePath = Path.Combine(Application.persistentDataPath, "Data.json");
        string jsonContent;
        if (!File.Exists(filePath))
        {
            Local.Json.ReadJson();
        }
        else
        {
            jsonContent = File.ReadAllText(filePath);
            // 파일 내용이 비어 있는지 확인
            if (string.IsNullOrEmpty(jsonContent))
            {
                Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
            }
            else if (!string.IsNullOrEmpty(jsonContent))
            {
                Local.Json.LoadJson();
                CardsDataBase.CardDeck = Local.DataSave.MyDeck;
                Local.Stage = Local.DataSave.Stage;
                EquipmentData.Equipments = Local.DataSave.Equipments;
                EquipmentData.WearEquipments = Local.DataSave.SelectEquipments;
                Local.EventHandler.Invoke<int>(EnumType.LoadData, 1);
                Local.Json.ReadJson();
            }
        }
    }
}
