using UnityEngine;
using UnityEngine.UI;

public class RoundIconContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject fullContainer;
    [SerializeField]
    private GameObject emptyContainer;
    [SerializeField]
    private Image icon;

    public void Start()
    {
        fullContainer.SetActive(false);
        emptyContainer.SetActive(true);
    }


    public void SetState(bool isFull)
    {
        fullContainer.SetActive(isFull);
        emptyContainer.SetActive(!isFull);
    }


    public void SetIcon(Sprite sprite)
    {
        this.icon.sprite = sprite;
    }
}
