using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerClickHandler
{
    public Vector2 Pivot;

    public void OnPointerClick(PointerEventData eventData)
    {
        Local.EventHandler.Invoke<Vector2>(EnumType.ContentsMove, Pivot);
    }
}
