using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    public static class CardTypes
    {
        public static void InitCardTypes()
        {
            Types = new Dictionary<Label,CardType>()
            {
                { Label.NONE, new CardType("") },
                { Label.CREATURE, new CardType("Creature") },
                { Label.INSTANT, new CardType("Instant") },
                { Label.SORCERY, new CardType("Sorcery") },
                { Label.ENCHANTMENT, new CardType("Enchantment") },
                { Label.ARTIFACT, new CardType("Artifact") },
                { Label.PLANESWALKER, new CardType("Planeswalker") },
                { Label.LAND, new CardType("Land") },
            };
        }

        public static Dictionary<Label, CardType> Types;

        public enum Label
        {
            NONE,

            CREATURE,
            INSTANT,
            SORCERY,
            ENCHANTMENT,
            ARTIFACT,
            PLANESWALKER,
            LAND
        }

        public class CardType
        {
            public CardType(string textRepresentation)
            {
                TextRepresentation = textRepresentation;
            }

            public string TextRepresentation { get; private set; }
        }
    }
}
