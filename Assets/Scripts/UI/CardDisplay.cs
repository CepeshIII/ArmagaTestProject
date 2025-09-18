using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;


public class CardDragEventArgs: EventArgs
{
    public CardData cardData;
    public Vector3 worldPosition;

    public CardDragEventArgs(CardData cardData, Vector3 worldPosition)
    {
        this.cardData = cardData;
        this.worldPosition = worldPosition;
    }
}


public class CardDisplay: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("Card UI Elements")]
    [SerializeField] private TextMeshProUGUI cardNameTextField;
    [SerializeField] private TextMeshProUGUI cardDescriptionTextField;
    [SerializeField] private Image cardImage;

    [Header("Other Settings")]
    [SerializeField] private Vector3 dragScale = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);

    private RectTransform rectTransform;
    private Canvas canvas;

    private CardData cardData;

    public event EventHandler<CardDragEventArgs> CardDragStarted;
    public event EventHandler<CardDragEventArgs> CardDragEnded;

    public CardData CardData { get => cardData; }


    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
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
    

    public void Activate()
    {
        gameObject.SetActive(true);
    }


    public void Deactivate()
    {
        gameObject.SetActive(false);

    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        CardDragStarted?.Invoke(this, new CardDragEventArgs(cardData, rectTransform.position));
        rectTransform.localScale = dragScale;
    }


    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        CardDragEnded?.Invoke(this, new CardDragEventArgs(cardData, rectTransform.position));
        rectTransform.localScale = normalScale;
    }
}
