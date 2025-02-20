using Cysharp.Threading.Tasks;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class TurnManager : MonoBehaviour
{
    public void Start()
    {
        Local.EventHandler.Register<Unit>(EnumType.TurnRmove, (unit) => { Local.TurnSystem.UnRegister(); });
        Local.TurnSystem.Invoke().Forget();
    }

    public void Update()
    {

    }
}