using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainUIGraphicRaycaster : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster graphicRaycaster;



    public void Awake()
    {
        graphicRaycaster = GetComponent<GraphicRaycaster>();
    }


    /// <summary>
    /// Checks if the pointer is currently over any UI elements.
    /// </summary>
    public bool IsPointerOverUI(Vector2 mousePosition)
    {
        var resultList = new List<RaycastResult>();
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        graphicRaycaster.Raycast(eventData, resultList);

        return resultList.Count > 0;
    }
}
