using UnityEngine;


public interface ICardViewHandler
{
    void CreateView(CardInstance instance, Transform parent);
    void UpdateView();
    void RemoveView();
}
