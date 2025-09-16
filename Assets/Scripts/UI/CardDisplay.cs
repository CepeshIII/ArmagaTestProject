using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class CardDisplay: MonoBehaviour, IPointerDownHandler
{
    [Header("Card UI Elements")]
    [SerializeField] private TextMeshProUGUI cardNameTextField;
    [SerializeField] private TextMeshProUGUI cardDescriptionTextField;
    [SerializeField] private Image cardImage;
    [SerializeField] private Image selectedImage;

    private CardData cardData;

    public event EventHandler OnCardClicked;

    public CardData CardData { get => cardData; }
    

    public void Start()
    {
        Deselected();
    }

    public void SetCardData(CardData newCardData)
    {
        cardData = newCardData;

        if (cardNameTextField != null)
        {
            cardNameTextField.SetText(newCardData.cardName);
        }

        if (cardDescriptionTextField != null)
        {
            cardDescriptionTextField.SetText(newCardData.description);
        }

        if (cardImage != null)
        {
            cardImage.sprite = newCardData.sprite;
        }
    }
    

    public void Selected()
    {
        if(selectedImage != null)
            selectedImage.gameObject.SetActive(true);
    }


    public void Deselected()
    {
        if (selectedImage != null)
            selectedImage.gameObject.SetActive(false);
    }


    public void Activate()
    {
        gameObject.SetActive(true);
    }


    public void Deactivate()
    {
        gameObject.SetActive(false);

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnCardClicked?.Invoke(this, new EventArgs());
    }
}
