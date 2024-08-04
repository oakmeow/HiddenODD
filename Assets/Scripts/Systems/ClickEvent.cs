using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickEvent : MonoBehaviour
{
    public bool IsOnArea(GameObject gameObj) { return IsOnArea(gameObj.GetComponent<RectTransform>()); }
    public bool IsOnArea(RectTransform rect)
    {
        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;

        if (!rect.gameObject.activeInHierarchy)
            return false;

        float x1 = rect.transform.position.x - (rect.rect.width / 2);
        float y1 = rect.transform.position.y - (rect.rect.height / 2);
        float x2 = x1 + rect.rect.width;
        float y2 = y1 + rect.rect.height;

        if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
            return true;
        else
            return false;
    }
}
