using System;
using System.Collections.Generic;
/// <summary>
/// Factory class responsible for creating instances of different card types 
/// (Unit, Building, Spell) based on their <see cref="CardType"/>.
/// </summary>
public class CardInstanceFactory
{
    /// <summary>
    /// Dictionary mapping card types to their corresponding factory methods.
    /// </summary>
    public Dictionary<CardType, Func<CardData, CardInstance>> cardInstances;

    /// <summary>
    /// Initializes a new instance of <see cref="CardInstanceFactory"/> 
    /// and registers all available card types.
    /// </summary>
    public CardInstanceFactory()
    {
        RegisterCardInstances();
    }

    /// <summary>
    /// Creates a card instance for the given <paramref displayName="cardData"/>.
    /// </summary>
    /// <param displayName="cardData">The card data to create an instance from.</param>
    /// <returns>
    /// A new <see cref="CardInstance"/> corresponding to the card type, 
    /// or <c>null</c> if the card type is not registered or data is invalid.
    /// </returns>
    public CardInstance GetInstance(CardData cardData)
    {
        if (cardData == null || cardInstances == null) return null;

        if (cardInstances.TryGetValue(cardData.CardType, out var creator))
        {
            return creator(cardData);
        }

        return null;
    }

    /// <summary>
    /// Attempts to create a card instance for the given <paramref displayName="cardData"/>.
    /// </summary>
    /// <param displayName="cardData">The card data to create an instance from.</param>
    /// <param displayName="cardInstance">When this method returns, contains the created card instance if successful, otherwise <c>null</c>.</param>
    /// <returns><c>true</c> if a card instance was created successfully; otherwise, <c>false</c>.</returns>
    public bool TryGetInstance(CardData cardData, out CardInstance cardInstance)
    {
        cardInstance = null;

        if (cardData == null || cardInstances == null) return false;

        if (cardInstances.TryGetValue(cardData.CardType, out var creator))
        {
            cardInstance = creator(cardData);
            return true;
        }

        return false;
    }

    /// <summary>
    /// Registers all supported card types with their corresponding creation logic.
    /// </summary>
    private void RegisterCardInstances()
    {
        cardInstances = new()
        {
            { CardType.Unit,     (data) => new UnitCardInstance((UnitCard)data) },
            { CardType.Building, (data) => new BuildingCardInstance((BuildingCard)data) },
            { CardType.Spell,    (data) => new SpellCardInstance((SpellCard)data) }
        };
    }
}
