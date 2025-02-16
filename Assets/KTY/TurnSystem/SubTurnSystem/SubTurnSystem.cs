using Cysharp.Threading.Tasks;
using System;
using System.ComponentModel;
using UnityEditor.PackageManager;
using UnityEngine;

public class SubTurnSystem : MonoBehaviour, ITurnObj, IEvent
{
    protected Action TurnAction;//턴 마다 실행할 액션
    private bool turnEnd = false;
    public virtual void Start()
    {
    }

    public bool Invoke()//액션 실행
    {
        Inside().Forget();
        return turnEnd;
    }

    public async UniTask Inside()
    {
        while (!Local.TurnSystem.turnproress)
        {
            TurnAction?.Invoke();
            TurnAction = null;

            await UniTask.WaitUntil(() => TurnAction != null);
        }
        Debug.Log("끝");
    }
    public void Register(Action ActionType)//액션 할당
    {
        TurnAction += ActionType.Invoke;
    }

    public void UnRegister(Action ActionType)//액션 해제
    {
        TurnAction -= ActionType.Invoke;
    }

    public void TurnEnd()
    {
        turnEnd = true;
    }
    public void Execute()
    {
        throw new NotImplementedException();
    }
}
