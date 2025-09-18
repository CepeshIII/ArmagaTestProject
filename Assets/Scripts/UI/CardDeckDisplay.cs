using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardDeckDisplay : MonoBehaviour
{
    [SerializeField] private CardDisplay cardDisplayPrefab;
    [SerializeField] private LayoutGroup layoutGroup;

    private List<CardDisplay> currentDeck = new List<CardDisplay>();

    public event Action<CardData, Vector3> CardDropped;


    private void Start()
    {
        layoutGroup = GetComponent<LayoutGroup>();

        GroupActivate();
    }


    public void UpdateDisplay(List<CardData> cards)
    {
        // Enable the layout group to ensure proper layout
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }


        // If the current deck is null create a new list
        currentDeck ??= new List<CardDisplay>(cards.Count);


        int index = 0;
        foreach (var card in cards)
        {
            CardDisplay cardDisplay;
            
            // if there is already a cardDisplay in the current deck at this index, just get it
            if ( index < currentDeck.Count)
            {
                // If the cardDisplay at this index is null, instantiate a new one
                if (currentDeck[index] == null)
                {
                    cardDisplay = CardInstantiate();
                    currentDeck[index] = cardDisplay;
                }
                else
                {
                    cardDisplay = currentDeck[index];
                }
            }
            else
            {
                //Instantiate a new cardDisplay if there isn't one already by the index
                cardDisplay = CardInstantiate();
                currentDeck.Add(cardDisplay);
            }

            // Set the card data to the cardDisplay
            cardDisplay.SetCardData(card);
            cardDisplay.Activate();
            index++;
        }

        // If there are more cardDisplays in the current deck than there are cards in the deck, hide the extra cardDisplays
        if(index < currentDeck.Count)
        {
            for (int i = index; i < currentDeck.Count; i++)
            {
                // Logic to hide the cardDisplay from display by deactivating its game object
                var cardDisplay = currentDeck[i];
                cardDisplay.Deactivate();
            }
        }
    }


    private CardDisplay CardInstantiate()
    {
        if (cardDisplayPrefab != null)
        {
            var cardDisplay = Instantiate(cardDisplayPrefab, transform);
            cardDisplay.CardDragStarted += HandleCardDragStarted;
            cardDisplay.CardDragEnded += HandleCardDragEnded;
            return cardDisplay;
        }
        Debug.LogError("CardDisplay prefab is not assigned in the inspector.");
        return null;
    }


    private void HandleCardDragStarted(object sender, CardDragEventArgs e)
    {
        // Deactivate the group to prevent layout issues during drag
        GroupDeactivate();
    }


    private void HandleCardDragEnded(object sender, CardDragEventArgs e)
    {
        CardDropped?.Invoke(e.cardData, e.worldPosition);
        // Reactivate the group after drag ends
        GroupActivate();
    }


    private void GroupActivate()
    {
        // Enable the layout group to ensure proper layout
        if (layoutGroup != null)
        {
            layoutGroup.enabled = true;
        }
    }


    private void GroupDeactivate()
    {
        // Disable the layout group to stop layout calculations
        if (layoutGroup != null)
        {
            layoutGroup.enabled = false;
        }
    }
    
}


