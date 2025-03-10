using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Json
{
    [ContextMenu("ReadJson")]
    public void ReadJson()
    {
        string jsonData = JsonUtility.ToJson(Local.DataSave);
        string path = Path.Combine(Application.persistentDataPath, "Data.json");
        if (!File.Exists(path))
        {
            using (File.Create(path)) { } // 파일 스트림을 닫기 위해 using 사용
        }
        else
            File.WriteAllText(path, jsonData);
    }
    [ContextMenu("LoadJson")]
    public void LoadJson()
    {
        string path = Path.Combine(Application.persistentDataPath, "Data.json");
        string jsonData = File.ReadAllText(path);
        Local.DataSave = JsonUtility.FromJson<DataSave>(jsonData);
    }

}
