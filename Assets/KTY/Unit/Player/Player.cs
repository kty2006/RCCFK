using System;
using UnityEditor;
using UnityEngine;

[DefaultExecutionOrder(0)]
[Serializable]
public class Player : Unit
{
    public PlayerStates PlayerStates;
    public void Awake()
    {
        ResetStates();
        //StatesUiSet();
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerAttack, (unit) => { SetUnitAttack(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerDefense, (unit) => { SetUnitDefense(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerRecovery, (unit) => { SetUnitRecovery(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerBuff, (unit) => { SetUnitBuff(unit); });
        Local.EventHandler.Register<AbillityWrapper>(EnumType.PlayerSpecial, (unit) => { SetUnitSpecial(unit); });
        Local.EventHandler.Register<States>(EnumType.EnemyDie, (enemyState) => { UnitStates.Cost = UnitStates.MaxCost; UnitStates.Exp = enemyState.SetExp; StatesUiSet(); });
        Local.EventHandler.Register<ResetCost>(EnumType.ResetCost, (reset) => { UnitStates.Cost = UnitStates.MaxCost; StatesUiSet(); });
        Local.EventHandler.Register<DataSave>(EnumType.SaveData, (datasave) => { datasave.States = UnitStates; });
        Local.EventHandler.Register<int>(EnumType.LoadData, (num) => { UnitStates = Local.DataSave.States; Local.Json.ReadJson(); });
        Local.EventHandler.Register<States>(EnumType.PlayerAllStateSum, (states) => { AllStateSum(states); });
        Local.EventHandler.Register<States>(EnumType.PlayerAllStateMinus, (states) => { AllStateMinus(states); });
    }

    public override void StatesUiSet()
    {
        Local.EventHandler.Invoke<States>(EnumType.PlayerStatesUi, UnitStates);
    }

    protected override void Die()
    {
        //적 재생성
        //카드새로뽑고
        //Local.EventHandler.Invoke<int>(EnumType.ReStart, 1);
        TargetStates.UnitStates.Hp = -TargetStates.UnitStates.MaxHp;
        ResetStates();
        Local.StageReSet();
        StatesUiSet();
        UnitStates.DeadFunc = Die;
        Local.EventHandler.Invoke<DataSave>(EnumType.SaveData, Local.DataSave);
    }

    public override void AllStateSum(States state)
    {
        base.AllStateSum(state);
        PlayerStates.Power += state.Power;
        PlayerStates.Defense += state.Defense;
        PlayerStates.MaxHp += state.Hp;
        PlayerStates.Speed += state.Speed;
    }

    public override void AllStateMinus(States state)
    {
        base.AllStateMinus(state);
        PlayerStates.Power -= state.Power;
        PlayerStates.Defense -= state.Defense;
        PlayerStates.MaxHp -= state.Hp;
        PlayerStates.Speed -= state.Speed;
    }

    public void ResetStates()
    {
        UnitStates = new();
        UnitStates.Power = PlayerStates.Power;
        UnitStates.Defense = PlayerStates.Defense;
        UnitStates.MaxHp = PlayerStates.MaxHp;
        UnitStates.Hp = PlayerStates.Hp;
        UnitStates.Speed = PlayerStates.Speed;
        UnitStates.MaxCost += PlayerStates.Maxcost;
        UnitStates.Cost = PlayerStates.Maxcost;
    }
}

