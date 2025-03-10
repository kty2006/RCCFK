using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnumType
{
    PlayerTurnAdd,
    PlayerTurnRemove,
    PlayerTurnSystem,
    PlayerAttack,
    PlayerDefense,
    PlayerRecovery,
    PlayerBuff,
    PlayerSpecial,
    PlayerStatesUi,
    PlayerDie,
    PlayerAllStateSum,
    PlayerAllStateMinus,
    GetExp,

    EnemyTurnAdd,
    EnemyTurnRemove,
    EnemyTurnSystem,
    EnemyAttack,
    EnemyDefense,
    EnemyRecovery,
    EnemyBuff,
    EnemySpecial,
    EnemyStatesUi,
    EnemyDie,
    EnemyTurnSelect,

    BattleUI,
    TurnAdd,
    TurnRmove,
    ContentsMove,
    InformationUi,
    ResetCost,
    CardDrowUp,
    SaveData,
    LoadData,
    ReStart

}
public enum Turn { TurnSystem }
public enum TurnAdd { TurnSystem }
public enum UnitDead { UnitDead }
public enum EnemyTurnSelect { EnemyTurnSelect }
public enum ResetCost { ResetCost }
public enum ReStart { ReStart }

public class EventHandler
{
    private Dictionary<Enum, EventContainer> EventDic = new Dictionary<Enum, EventContainer>();//이벤트를 저장할 딕셔너리

    public void Register<TEvent>(Enum enumType, Action<TEvent> action)//이벤트 저장
    {
        if (!EventDic.ContainsKey(enumType))//enum확인
        {
            EventDic.Add(enumType, new EventContainer());//딕셔너리 추가
        }
        EventDic[enumType].Register<TEvent>(new EventWrapper<TEvent>(action));
    }

    public void UnRegister(Enum enumType)//딕셔너리 제거
    {
        if (EventDic.ContainsKey(enumType))
        {
            EventDic.Remove(enumType);
        }
    }

    public void Invoke<TEvent>(Enum enumType, TEvent ev)//이벤트 실행
    {
        if (!EventDic.ContainsKey(enumType))
        {
            Debug.LogError($"[EventHandler] NotRegisterEvent {nameof(enumType)}");
            return;
        }
        EventDic[enumType].Invoke<TEvent>(ev);
    }
}

public class EventContainer
{
    public HashSet<EventWrapper> EventWrapperSet = new();

    public void Register<TEvent>(EventWrapper<TEvent> eventWrapper)
    {
        EventWrapperSet.Add(eventWrapper);
    }

    public void UnRegister<TEvent>(Action<TEvent> registerEventr)
    {
        foreach (var ev in EventWrapperSet)
        {
            if (ev.EqualEvent(registerEventr))
            {
                EventWrapperSet.Remove(ev);
                return;
            }
        }
    }

    public void Invoke<TEvent>(TEvent ev)
    {
        foreach (var post in EventWrapperSet)
        {
            post.Invoke(ev);
        }
    }
}

public abstract class EventWrapper
{
    public abstract void Invoke(object ev);
    public abstract bool EqualEvent(object ev);
}

public class EventWrapper<TEvent> : EventWrapper//  원하는 매개변수로 받을수 있게 Action을 제네릭 클래스로 덮음
{

    public Action<TEvent> GameEvent;
    public EventWrapper(Action<TEvent> ev)
    {
        GameEvent = ev;
    }

    public override bool EqualEvent(object ev)
    {
        if (this == ev)
        {
            return true;
        }
        return false;
    }

    public override void Invoke(object ev)
    {
        if (ev is TEvent eventValue)
        {
            GameEvent?.Invoke(eventValue);
        }
        else
        {
            Debug.LogError($"Invalid event type: {ev.GetType().Name} is not {typeof(TEvent).Name}");
        }
    }
}
