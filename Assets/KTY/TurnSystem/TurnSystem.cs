using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TurnSystem
{
    private List<ITurnObj> turns = new(); //���� ������ �ִ� ����Ʈ
    public bool Turnproress { get; set; } = true;
    public bool Turnskip { get; set; } = false;
    private ITurnObj currObj;
    public void Register(ITurnObj turn) //�� �Ҵ�
    {
        turns.Add(turn);
    }

    public void UnRegister() //�� ����
    {
        int index = (currObj == turns[0]) ? 1 : 0;
        turns.Remove(turns[index]);
    }

    public void Reset()
    {
        turns.Clear();
    }

    public bool TurnStart(bool turnstart)//
    {
        return Turnproress = turnstart;
    }

    public async UniTask Invoke() //�� ����
    {
        while (true)
        {
            for (int i = 0; i < turns.Count; i++)
            {
                await UniTask.WaitUntil(() => Turnproress);
                TurnStart(false);
                if (!Turnskip)
                {
                    currObj = turns[i];
                    currObj.Invoke();
                }
                else
                {
                    TurnStart(true);
                }
                Turnskip = false;
            }
            Local.EventHandler.Invoke<ResetCost>(EnumType.ResetCost, ResetCost.ResetCost);
        }
    }

}
