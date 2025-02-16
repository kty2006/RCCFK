using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    // 타겟 찾기//완료
    public Unit Player;
    public Unit Enemy;
    public Unit NextUnit;

    public void Start()
    {
        StartBattle();
        Local.EventHandler.Register<TurnAdd>(EnumType.TurnAdd, (turnAdd) => { GetTurn(); });
    }
    public void StartBattle()
    {
        if (Player.UnitStates.speed > Enemy.UnitStates.speed)
        {
            NextUnit = Player;
            GetTurn();
        }
        else
        {
            NextUnit = Enemy;
            GetTurn();
        }

    }

    public void GetTurn()
    {
        Player.TargetStates = Enemy;
        Enemy.TargetStates = Player;
        if (NextUnit == Player)
        {
            Local.EventHandler.Invoke<Turn>(EnumType.PlayerTurnSystem, Turn.TurnSystem);
            Local.EventHandler.Invoke<Turn>(EnumType.EnemyTurnSystem, Turn.TurnSystem);
            NextUnit = Enemy;
        }
        else
        {
            Local.EventHandler.Invoke<Turn>(EnumType.EnemyTurnSystem, Turn.TurnSystem);
            Local.EventHandler.Invoke<Turn>(EnumType.PlayerTurnSystem, Turn.TurnSystem);
            NextUnit = Player;
        }
    }
    //배틀 시작시 속도 체크
    //턴 지급
}
