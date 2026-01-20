using UnityEngine;


public class MenuUI : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
