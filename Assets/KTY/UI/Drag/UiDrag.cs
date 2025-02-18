using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiDrag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public RectTransform GridPanel;
    private float posY;
    private float posY2;
    List<float> Drag = new List<float>();
    public void OnDrag(PointerEventData eventData)
    {
        Drag.Add(eventData.position.normalized.y);
        if (Drag.Count > 2)
        {
            if ((Drag[Drag.Count - 2] > Drag[Drag.Count - 1]))
            {
                GridPanel.position -= new Vector3(0, 10, 0);
            }
            else
                GridPanel.position += new Vector3(0, 10, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Drag.Clear();
    }

}
