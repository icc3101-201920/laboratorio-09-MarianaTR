using Laboratorio_8_OOP_201920.Cards;
using Laboratorio_8_OOP_201920.Enums;
using System.Collections.Generic;

namespace Laboratorio_8_OOP_201920
{
    public static class Effect
    {
        private static Dictionary<EnumEffect, string> effectDescriptions = new Dictionary<EnumEffect, string>()
        {
            { EnumEffect.bitingFrost, "Sets the strength of all melee cards to 1 for both players" },
            { EnumEffect.impenetrableFog, "Sets the strength of all range cards to 1 for both players" },
            { EnumEffect.torrentialRain, "Sets the strength of all longRange cards to 1 for both players" },
            { EnumEffect.clearWeather, "Removes all Weather Card (Biting Frost, Impenetrable Fog and Torrential Rain) effects" },
            { EnumEffect.moraleBoost, "Adds +1 to all units in the row (excluding itself)" },
            { EnumEffect.spy, "Place on your opponent's battlefield (counts towards opponent's total) and draw 2 cards from your deck" },
            { EnumEffect.tightBond, "Place next to a card with the same name to double the strength of both cards" },
            { EnumEffect.buff, "Doubles the strength of all unit cards in that row. Limited to 1 per row" },
            { EnumEffect.none, "None" },
        };

        public static string GetEffectDescription(EnumEffect e)
        {
            return effectDescriptions[e];
        }

        private static void AttackPonitsOne(EnumType type, Player pj1, Player pj2)
        {
            foreach (Card card in pj1.Deck.Cards)
            {
                if (card.Type == type)
                {
                    CombatCard combatCard= (CombatCard)card;
                    combatCard.VistaAttackPoints = combatCard.AttackPoints;
                    combatCard.AttackPoints = 1;
                }
            }

            foreach (Card card in pj2.Deck.Cards)
            {
                if (card.Type == type)
                {
                    CombatCard combatCard = (CombatCard)card;
                    combatCard.VistaAttackPoints = combatCard.AttackPoints;
                    combatCard.AttackPoints = 1;
                }
            }
        }
        public static void ApplyEffect(Card playedCard, Player activePlayer, Player opponent, Board board)
        {
            //Recomendación: Utilice switch(playedCard.CardEffect) para definir los distintos efectos.
            if (playedCard.Type == EnumType.weather)
            {
                switch (playedCard.CardEffect)
                {
                    case EnumEffect.bitingFrost:
                        AttackPonitsOne(EnumType.melee, activePlayer, opponent);
                        break;

                    case EnumEffect.clearWeather:
                        int idCard = 0;
                        foreach (Card card in activePlayer.Deck.Cards)
                        {

                            if (card.Type == EnumType.weather)
                            {
                                activePlayer.Deck.DestroyCard(idCard);
                            }
                            else if (card.Type == EnumType.melee || card.Type == EnumType.range || card.Type == EnumType.longRange)
                            {
                                CombatCard combatCard = (CombatCard)card;
                                if (combatCard.VistaAttackPoints != -1)
                                {
                                    combatCard.AttackPoints = combatCard.VistaAttackPoints;
                                    combatCard.VistaAttackPoints = -1;
                                }
                            }
                            idCard++;
                        }
                        idCard = 0;
                        foreach(Card card1 in opponent.Deck.Cards)
                        {
                            if (card1.Type == EnumType.weather)
                            {
                                opponent.Deck.DestroyCard(idCard);
                            }
                            else if (card1.Type == EnumType.melee || card1.Type == EnumType.range || card1.Type == EnumType.longRange)
                            {
                                CombatCard combatCard = (CombatCard)card1;
                                if (combatCard.VistaAttackPoints != -1)
                                {
                                    combatCard.AttackPoints = combatCard.VistaAttackPoints;
                                    combatCard.VistaAttackPoints = -1;
                                }
                            }
                            idCard++;
                        }
                        break;

                    case EnumEffect.impenetrableFog:
                        AttackPonitsOne(EnumType.range, activePlayer, opponent);
                        break;

                    case EnumEffect.none:
                        break;
                    case EnumEffect.torrentialRain:
                        AttackPonitsOne(EnumType.longRange, activePlayer, opponent);
                        break;
                    
                }
            }

        }
    }
}
