using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[Serializable]
public class DataSave
{
    public List<Card> MyDeck;

    public List<Equipment> Equipments;

    public List<Equipment> SelectEquipments;

    public States States;

    public int Stage;

    public int Gold;
}
