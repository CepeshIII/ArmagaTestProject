using System;
using UnityEngine;
using Zenject;


public class RoundScope
{
    public readonly DiContainer Container;

    public RoundScope(DiContainer container)
    {
        Container = container;
    }
}


public class RoundFactory
{
    private readonly DiContainer diContainer;



    [Inject]
    public RoundFactory(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }


   //public void CreateRound()
   //{
   //     var roundContainer = diContainer.CreateSubContainer();
   //     roundContainer.Install<SceneInstaller>();
   //}
}
