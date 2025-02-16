using UnityEngine;
using Cysharp.Threading.Tasks;

public class TurnUIPresenter : MonoBehaviour
{
    public TurnUIView playUI;

    public void Start()
    {//옵저버 패턴 버튼이 눌릴때마다 이벤트 핸들러 행동이 바뀌게
        //playUI.Attack.onClick.AddListener(() => Local.EventHandler.Invoke<Type>(new PlayerAttack()));
        //playUI.Move.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerMove>(new PlayerMove()));
        playUI.Start.onClick.AddListener(() => Local.TurnSystem.TurnStart(true));
        //playUI.Remove.onClick.AddListener(() => Local.EventHandler.Invoke<PlayerTurnSystem>(new PlayerTurnSystem()));
    }
}
