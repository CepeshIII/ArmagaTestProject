using System;
using UnityEngine;
using Zenject;


public class CardPrefabFactory: IInitializable
{
    private readonly DiContainer container;


    public CardPrefabFactory(DiContainer container)
    {
        this.container = container;
    }


    public void Initialize()
    {

    }


    public bool TryGetGameObject(CardData cardData, out GameObject gameObject)
    {
        gameObject = null;

        if (cardData is IPrefabSource prefabSource) 
        { 
            gameObject = InstantiatePrefab(prefabSource.Prefab);
            if (gameObject != null)
            {
                return true;
            }
        }

        return false;
    }


    private GameObject InstantiatePrefab(GameObject prefab)
    {
        if (container != null && prefab != null) 
        { 
            return container.InstantiatePrefab(prefab);
        }
        return null;
    }

}
