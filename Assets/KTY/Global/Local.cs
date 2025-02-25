using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Local
{
    public static EventHandler EventHandler = new();
    public static TurnSystem TurnSystem = new();
    public static DataSave DataSave = new();
    public static Json Json = new();
    private static int stage = 0;
    public static int Gold=10000;
    public static int Stage { get { return stage; } set { stage += value; DataSave.Stage = Stage; } }

    public static void StageReSet()
    {
        stage = 0;
    }
}
