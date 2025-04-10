using System.Collections.Generic;
using UnityEngine;

namespace CardData
{
    //Dar click derecho en Unity y sale una al final para crear cartas
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class  Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int pointsGained;  // Puntos que sumás (si la carta es roja)
        public int pointsStolen;  // Puntos que le sacás al rival (si la carta es negra)
        public Sprite cardSprite;
        public List<DamageType> damageType;
    }

    //Crear el tipo de carta
    public enum CardType
    {
        GainPoints,
        StealPoints 
    }

    public enum DamageType
    {
        Red,   // Gana puntos (cartas "buenas")
        Black  // Resta puntos (cartas "malas")
    }


}
