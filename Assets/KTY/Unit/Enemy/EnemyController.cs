using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private AbillityWrapper unitBehaviour = new();

    public void Awake()
    {
        Local.EventHandler.Register<EnemyTurnSelect>(EnumType.EnemyTurnSelect, (turnSelect) => { SelectAction(); });
    }

    public void SelectAction()
    {
        //int num = Random.Range(0, 5);
        int num = 0;
        switch (num)
        {
            case 0:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyAttack, unitBehaviour);
                unitBehaviour.AbillityFunc += () => { Local.TurnSystem.TurnStart(true); };
                Local.EventHandler.Invoke<Action>(EnumType.EnemyTurnAdd, unitBehaviour.AbillityFunc);
                break;
            case 1:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyDefense, unitBehaviour);
                unitBehaviour.AbillityFunc += () => { Local.TurnSystem.TurnStart(true); };
                Local.EventHandler.Invoke<Action>(EnumType.EnemyTurnAdd, unitBehaviour.AbillityFunc);
                break;
            case 2:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyRecovery, unitBehaviour);
                unitBehaviour.AbillityFunc += () => { Local.TurnSystem.TurnStart(true); };
                Local.EventHandler.Invoke<Action>(EnumType.EnemyTurnAdd, unitBehaviour.AbillityFunc);
                break;
            case 3:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemyBuff, unitBehaviour);
                unitBehaviour.AbillityFunc += () => { Local.TurnSystem.TurnStart(true); };
                Local.EventHandler.Invoke<Action>(EnumType.EnemyTurnAdd, unitBehaviour.AbillityFunc);
                break;
            case 4:
                Local.EventHandler.Invoke<AbillityWrapper>(EnumType.EnemySpecial, unitBehaviour);
                unitBehaviour.AbillityFunc += () => { Local.TurnSystem.TurnStart(true); };
                Local.EventHandler.Invoke<Action>(EnumType.EnemyTurnAdd, unitBehaviour.AbillityFunc);
                break;
        }

    }
}
