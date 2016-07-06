using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MTGSetGenerator
{
    public static class CostIcons
    {
        public static void InitCostIcons()
        {
            // TODO set icon image references in InitializeCostIcons()
            Icons = new Dictionary<Label, CostIcon>()
            {
                { Label.NONE, new CostIcon(null, "", 0) },
                { Label.X, new CostIcon(null, "X", 0) },
                { Label.ZERO, new CostIcon(null, "0", 0) },
                { Label.ONE, new CostIcon(null, "1", 1) },
                { Label.TWO, new CostIcon(null, "2", 2) },
                { Label.THREE, new CostIcon(null, "3", 3) },
                { Label.FOUR, new CostIcon(null, "4", 4) },
                { Label.FIVE, new CostIcon(null, "5", 5) },
                { Label.SIX, new CostIcon(null, "6", 6) },
                { Label.SEVEN, new CostIcon(null, "7", 7) },
                { Label.EIGHT, new CostIcon(null, "8", 8) },
                { Label.NINE, new CostIcon(null, "9", 9) },
                { Label.TEN, new CostIcon(null, "10", 10) },
                { Label.ELEVEN, new CostIcon(null, "11", 11) },
                { Label.TWELVE, new CostIcon(null, "12", 12) },
                { Label.WHITE, new CostIcon(null, "W", 1, CardColors.Label.WHITE) },
                { Label.BLUE, new CostIcon(null, "U", 1, CardColors.Label.BLUE) },
                { Label.BLACK, new CostIcon(null, "B", 1, CardColors.Label.BLACK) },
                { Label.RED, new CostIcon(null, "R", 1, CardColors.Label.RED) },
                { Label.GREEN, new CostIcon(null, "G", 1, CardColors.Label.GREEN) },
                { Label.WHITE_BLUE, new CostIcon(null, "WU", 1, CardColors.Label.WHITE, CardColors.Label.BLUE) },
                { Label.WHITE_BLACK, new CostIcon(null, "WB", 1, CardColors.Label.WHITE, CardColors.Label.BLACK) },
                { Label.WHITE_RED, new CostIcon(null, "WR", 1, CardColors.Label.WHITE, CardColors.Label.RED) },
                { Label.WHITE_GREEN, new CostIcon(null, "WG", 1, CardColors.Label.WHITE, CardColors.Label.GREEN) },
                { Label.BLUE_BLACK, new CostIcon(null, "UB", 1, CardColors.Label.BLUE, CardColors.Label.BLACK) },
                { Label.BLUE_RED, new CostIcon(null, "UR", 1, CardColors.Label.BLUE, CardColors.Label.RED) },
                { Label.BLUE_GREEN, new CostIcon(null, "UG", 1, CardColors.Label.BLUE, CardColors.Label.GREEN) },
                { Label.BLACK_RED, new CostIcon(null, "BR", 1, CardColors.Label.BLACK, CardColors.Label.RED) },
                { Label.BLACK_GREEN, new CostIcon(null, "BG", 1, CardColors.Label.BLACK, CardColors.Label.GREEN) },
                { Label.RED_GREEN, new CostIcon(null, "RG", 1, CardColors.Label.RED, CardColors.Label.GREEN) },
                { Label.PHYREXIAN_WHITE, new CostIcon(null, "PW", 1, CardColors.Label.WHITE) },
                { Label.PHYREXIAN_BLUE, new CostIcon(null, "PU", 1, CardColors.Label.BLUE) },
                { Label.PHYREXIAN_BLACK, new CostIcon(null, "PB", 1, CardColors.Label.BLACK) },
                { Label.PHYREXIAN_RED, new CostIcon(null, "PR", 1, CardColors.Label.RED) },
                { Label.PHYREXIAN_GREEN, new CostIcon(null, "PG", 1, CardColors.Label.GREEN) },
                { Label.TAP, new CostIcon(null, "T", 0) },
                { Label.UNTAP, new CostIcon(null, "UT", 0) }
            };
        }

        public static Dictionary<Label, CostIcon> Icons;

        public enum Label
        {
            NONE,

            X,
            ZERO,
            ONE,
            TWO,
            THREE,
            FOUR,
            FIVE,
            SIX,
            SEVEN,
            EIGHT,
            NINE,
            TEN,
            ELEVEN,
            TWELVE,
            WHITE,
            BLUE,
            BLACK,
            RED,
            GREEN,
            COLORLESS,
            WHITE_BLUE,
            WHITE_BLACK,
            WHITE_RED,
            WHITE_GREEN,
            BLUE_BLACK,
            BLUE_RED,
            BLUE_GREEN,
            BLACK_RED,
            BLACK_GREEN,
            RED_GREEN,
            PHYREXIAN_WHITE,
            PHYREXIAN_BLUE,
            PHYREXIAN_BLACK,
            PHYREXIAN_RED,
            PHYREXIAN_GREEN,
            TAP,
            UNTAP
        }

        public class CostIcon
        {
            public CostIcon(BitmapImage icon, string textRepresentation, int cmcContribution, 
                CardColors.Label colorContribution = CardColors.Label.NONE, CardColors.Label secondaryColorContribution = CardColors.Label.NONE)
            {
                Icon = icon;
                TextRepresentation = textRepresentation;
                CmcContribution = cmcContribution;
                ColorContribution = colorContribution;
                SecondaryColorContribution = secondaryColorContribution;
            }

            public BitmapImage Icon { get; private set; }
            public string TextRepresentation { get; private set; }
            public int CmcContribution { get; private set; }
            public CardColors.Label ColorContribution { get; private set; }
            public CardColors.Label SecondaryColorContribution { get; private set; }
        } 
    }  
}
