using UnityEngine;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardImage;
        public CardType cardType;
        public int damage;

        [TextArea]
        public string description;
    }


    public enum CardType
    {
        Luz,
        Oscuridad,
        Muerte,
        Sangre
    }
}

