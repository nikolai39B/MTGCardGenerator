using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGSetGenerator
{
    public static class CardColors
    {
        public static void InitCardColors()
        {
            Colors = new Dictionary<Label, CardColor>()
            {
                { Label.NONE, new CardColor("", CardFrames.Label.NONE) },
                { Label.WHITE, new CardColor("White", CardFrames.Label.WHITE) },
                { Label.BLUE, new CardColor("Blue", CardFrames.Label.BLUE) },
                { Label.BLACK, new CardColor("Black", CardFrames.Label.BLACK) },
                { Label.RED, new CardColor("Red", CardFrames.Label.RED) },
                { Label.GREEN, new CardColor("Green", CardFrames.Label.GREEN) },
                { Label.COLORLESS, new CardColor("Colorless", CardFrames.Label.SILVER) }
            };
        }

        public static Dictionary<Label, CardColor> Colors;

        public enum Label
        {
            NONE,

            WHITE,
            BLUE,
            BLACK,
            RED,
            GREEN,
            COLORLESS
        }

        public class CardColor
        {
            public CardColor(string textRepresentation, CardFrames.Label cardFrame)
            {
                TextRepresentation = textRepresentation;
                CardFrame = cardFrame;
            }

            public string TextRepresentation { get; private set; }
            public CardFrames.Label CardFrame { get; private set; }
        }
    }
}
