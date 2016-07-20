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
            Application app = Application.Current;
            
            Icons = new Dictionary<Label, CostIcon>()
            {
                { Label.NONE, new CostIcon(null, "", 0) },
                { Label.X, new CostIcon(app.FindResource("img_CostIconX") as BitmapImage, "X", 0) },
                { Label.ZERO, new CostIcon(app.FindResource("img_CostIcon00") as BitmapImage, "0", 0) },
                { Label.ONE, new CostIcon(app.FindResource("img_CostIcon01") as BitmapImage, "1", 1) },
                { Label.TWO, new CostIcon(app.FindResource("img_CostIcon02") as BitmapImage, "2", 2) },
                { Label.THREE, new CostIcon(app.FindResource("img_CostIcon03") as BitmapImage, "3", 3) },
                { Label.FOUR, new CostIcon(app.FindResource("img_CostIcon04") as BitmapImage, "4", 4) },
                { Label.FIVE, new CostIcon(app.FindResource("img_CostIcon05") as BitmapImage, "5", 5) },
                { Label.SIX, new CostIcon(app.FindResource("img_CostIcon06") as BitmapImage, "6", 6) },
                { Label.SEVEN, new CostIcon(app.FindResource("img_CostIcon07") as BitmapImage, "7", 7) },
                { Label.EIGHT, new CostIcon(app.FindResource("img_CostIcon08") as BitmapImage, "8", 8) },
                { Label.NINE, new CostIcon(app.FindResource("img_CostIcon09") as BitmapImage, "9", 9) },
                { Label.TEN, new CostIcon(app.FindResource("img_CostIcon10") as BitmapImage, "10", 10) },
                { Label.ELEVEN, new CostIcon(app.FindResource("img_CostIcon11") as BitmapImage, "11", 11) },
                { Label.TWELVE, new CostIcon(app.FindResource("img_CostIcon12") as BitmapImage, "12", 12) },
                { Label.COLORLESS, new CostIcon(app.FindResource("img_CostIconColorless") as BitmapImage, "C", 1, CardColors.Label.COLORLESS) },
                { Label.WHITE, new CostIcon(app.FindResource("img_CostIconWhite") as BitmapImage, "W", 1, CardColors.Label.WHITE) },
                { Label.BLUE, new CostIcon(app.FindResource("img_CostIconBlue") as BitmapImage, "U", 1, CardColors.Label.BLUE) },
                { Label.BLACK, new CostIcon(app.FindResource("img_CostIconBlack") as BitmapImage, "B", 1, CardColors.Label.BLACK) },
                { Label.RED, new CostIcon(app.FindResource("img_CostIconRed") as BitmapImage, "R", 1, CardColors.Label.RED) },
                { Label.GREEN, new CostIcon(app.FindResource("img_CostIconGreen") as BitmapImage, "G", 1, CardColors.Label.GREEN) },
                { Label.WHITE_BLUE, new CostIcon(app.FindResource("img_CostIconWhiteBlue") as BitmapImage, "WU", 1, CardColors.Label.WHITE, CardColors.Label.BLUE) },
                { Label.WHITE_BLACK, new CostIcon(app.FindResource("img_CostIconWhiteBlack") as BitmapImage, "WB", 1, CardColors.Label.WHITE, CardColors.Label.BLACK) },
                { Label.WHITE_RED, new CostIcon(app.FindResource("img_CostIconWhiteRed") as BitmapImage, "WR", 1, CardColors.Label.WHITE, CardColors.Label.RED) },
                { Label.WHITE_GREEN, new CostIcon(app.FindResource("img_CostIconWhiteGreen") as BitmapImage, "WG", 1, CardColors.Label.WHITE, CardColors.Label.GREEN) },
                { Label.BLUE_BLACK, new CostIcon(app.FindResource("img_CostIconBlueBlack") as BitmapImage, "UB", 1, CardColors.Label.BLUE, CardColors.Label.BLACK) },
                { Label.BLUE_RED, new CostIcon(app.FindResource("img_CostIconBlueRed") as BitmapImage, "UR", 1, CardColors.Label.BLUE, CardColors.Label.RED) },
                { Label.BLUE_GREEN, new CostIcon(app.FindResource("img_CostIconBlueGreen") as BitmapImage, "UG", 1, CardColors.Label.BLUE, CardColors.Label.GREEN) },
                { Label.BLACK_RED, new CostIcon(app.FindResource("img_CostIconBlackRed") as BitmapImage, "BR", 1, CardColors.Label.BLACK, CardColors.Label.RED) },
                { Label.BLACK_GREEN, new CostIcon(app.FindResource("img_CostIconBlackGreen") as BitmapImage, "BG", 1, CardColors.Label.BLACK, CardColors.Label.GREEN) },
                { Label.RED_GREEN, new CostIcon(app.FindResource("img_CostIconRedGreen") as BitmapImage, "RG", 1, CardColors.Label.RED, CardColors.Label.GREEN) },
                { Label.PHYREXIAN_COLORLESS, new CostIcon(app.FindResource("img_CostIconPhyrexianColorless") as BitmapImage, "PC", 1, CardColors.Label.COLORLESS) },
                { Label.PHYREXIAN_WHITE, new CostIcon(app.FindResource("img_CostIconPhyrexianWhite") as BitmapImage, "PW", 1, CardColors.Label.WHITE) },
                { Label.PHYREXIAN_BLUE, new CostIcon(app.FindResource("img_CostIconPhyrexianBlue") as BitmapImage, "PU", 1, CardColors.Label.BLUE) },
                { Label.PHYREXIAN_BLACK, new CostIcon(app.FindResource("img_CostIconPhyrexianBlack") as BitmapImage, "PB", 1, CardColors.Label.BLACK) },
                { Label.PHYREXIAN_RED, new CostIcon(app.FindResource("img_CostIconPhyrexianRed") as BitmapImage, "PR", 1, CardColors.Label.RED) },
                { Label.PHYREXIAN_GREEN, new CostIcon(app.FindResource("img_CostIconPhyrexianGreen") as BitmapImage, "PG", 1, CardColors.Label.GREEN) },
                { Label.TAP, new CostIcon(app.FindResource("img_CostIconTap") as BitmapImage, "T", 0) },
                { Label.UNTAP, new CostIcon(app.FindResource("img_CostIconUntap") as BitmapImage, "UT", 0) }
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
            COLORLESS,
            WHITE,
            BLUE,
            BLACK,
            RED,
            GREEN,
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
            PHYREXIAN_COLORLESS,
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
