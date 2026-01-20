using UnityEditor;
using Zenject;

public interface IUIManager
{
    public void ShowDeck();
    public void HideDeck();
    public void ShowCardInfo();
    public void HideCardInfo();
    public void ShowToBoardUI();
    public void HideToBoardUI();
    public void ShowToAttackUI();
    public void HideToAttackUI();
    public void OnToBoardButtonPressed();
    public void OnToAttackButtonPressed();
    public void ShowMenu();
    public void HideMenu();
    public void HideAll();
}