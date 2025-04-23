using UnityEngine;

public interface ICardDropArea
{
    /// <summary>
    /// Called when a card is dropped on the drop area.
    /// </summary>
    /// <param name="card">The card that was dropped.</param>
    void OnCardDrop(Card card);

}