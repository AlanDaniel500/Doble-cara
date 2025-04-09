using System.Collections.Generic;
using UnityEngine;

namespace CardData
{
    //Darl click derecho en Unity y sale una al final para crear cartas
    [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
    public class  Card : ScriptableObject
    {
        public string cardName;
        public List<CardType> cardType;
        public int healt;
        public int damageMax;
        public int damageMin;
        public Sprite cardSprite;
        public List<DamageType> damageType;
    }

    //Crear el tipo de carta
    public enum CardType
    {
        Attack,
        Defense,
        Heal,
        Buff,
        Debuff
    }


    public enum DamageType
    {
        Black,
        Red
    }


}
