using System;
using UnityEngine;
using Zenject;


public class CardPrefabFactory: IInitializable
{
    private DiContainer container;



    public void Initialize()
    {
        container = new DiContainer();
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
