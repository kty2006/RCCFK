using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class ContentsMove : MonoBehaviour
{
    public RectTransform MainUi;
    public Action<Vector2> ChangeScreenFunc;
    public void Start()
    {
        ChangeScreenFunc = (pivot) => { ChangeScreen(pivot).Forget(); };
        Local.EventHandler.Register<Vector2>(EnumType.ContentsMove, ChangeScreenFunc);
    }
    public async UniTask ChangeScreen(Vector2 pivot)
    {
        float currtPivot = MainUi.pivot.x;
        while (MainUi.pivot.x != pivot.x)
        {
            if (MainUi.pivot.x > pivot.x)
            {
                MainUi.pivot = new Vector2(Mathf.Clamp(currtPivot -= 0.1f, pivot.x, 2.5f), 0.5f);
            }
            else if (MainUi.pivot.x < pivot.x)
                MainUi.pivot = new Vector2(Mathf.Clamp(currtPivot += 0.1f, -1.5f, pivot.x), 0.5f);
            await UniTask.Yield();
        }
    }
}
