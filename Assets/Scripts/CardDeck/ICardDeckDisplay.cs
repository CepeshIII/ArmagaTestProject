using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface ICardDeckDisplay
{
    public event Action<CardData, Vector3> CardDropped;

    public void Hide();
    public void ShowCardDisplayers();
    public void UpdateDisplay(List<CardData> cards);
    public void CleanupDeckDisplay();
}