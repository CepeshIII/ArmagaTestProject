using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<CardDataBase>().AsSingle().NonLazy();
        Container.Bind<GameManager>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.Bind<CardDeckBuilder>().AsSingle();
    }
}
