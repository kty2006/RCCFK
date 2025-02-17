using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> turns = new(); //턴을 가지고 있는 리스트
    public bool turnproress { get; set; } = true;

    public void Register(ITurnObj turn) //턴 할당
    {
        turns.Add(turn);
    }

    public void UnRegister() //턴 해제
    {
        turns.Remove(turns.LastOrDefault<ITurnObj>());
    }

    public bool TurnStart(bool turnstart)//
    {
        return turnproress = turnstart;
    }

    public async UniTask Invoke() //턴 실행
    {
        while (true)
        {
            for (int i = 0; i < turns.Count; i++)
            {
                await UniTask.WaitUntil(() => turnproress);
                TurnStart(false);
                turns[i].Invoke();
            }
            //turns.Clear();
            //TurnStart(false);
        }
    }

}
