using UnityEngine;
using UnityEngine.UI;

public class MyBar : MonoBehaviour
{


    [SerializeField]
    private Image barController;


    public void SetValue(float value)
    {
        barController.fillAmount = value;

    }
}
