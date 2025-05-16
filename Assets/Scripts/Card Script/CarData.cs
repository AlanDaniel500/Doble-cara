using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

namespace CardSystem
{

    public class CardData : ScriptableObject
    {
        public string cardName;
        public Sprite cardImage;

        public CardType cardType;
        public int cardNumber;
        public int damage;

        public enum CardType
        {
            Luz,
            Oscuridad,
            Muerte,
            Sangre,
            Comodin
        }

    }
}