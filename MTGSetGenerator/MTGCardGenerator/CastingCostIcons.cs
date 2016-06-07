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
    static class CastingCostIcons
    {
        static CastingCostIcon X = new CastingCostIcon(CastingCostLabel.X, null, "{X}", 0);
        static CastingCostIcon Zero = new CastingCostIcon(CastingCostLabel.ZERO, null, "{0}", 0);
        static CastingCostIcon One = new CastingCostIcon(CastingCostLabel.ONE, null, "{1}", 1);
        static CastingCostIcon Two = new CastingCostIcon(CastingCostLabel.TWO, null, "{2}", 2);
        static CastingCostIcon Three = new CastingCostIcon(CastingCostLabel.THREE, null, "{3}", 3);
        static CastingCostIcon Four = new CastingCostIcon(CastingCostLabel.FOUR, null, "{4}", 4);
        static CastingCostIcon Five = new CastingCostIcon(CastingCostLabel.FIVE, null, "{5}", 5);
        static CastingCostIcon Six = new CastingCostIcon(CastingCostLabel.SIX, null, "{6}", 6);
        static CastingCostIcon Seven = new CastingCostIcon(CastingCostLabel.SEVEN, null, "{7}", 7);
        static CastingCostIcon Eight = new CastingCostIcon(CastingCostLabel.EIGHT, null, "{8}", 8);
        static CastingCostIcon Nine = new CastingCostIcon(CastingCostLabel.NINE, null, "{9}", 9);
        static CastingCostIcon Ten = new CastingCostIcon(CastingCostLabel.TEN, null, "{10}", 10);
        static CastingCostIcon Eleven = new CastingCostIcon(CastingCostLabel.ELEVEN, null, "{11}", 11);
        static CastingCostIcon Twelve = new CastingCostIcon(CastingCostLabel.TWELVE, null, "{12}", 12);
        static CastingCostIcon White = new CastingCostIcon(CastingCostLabel.WHITE, null, "{W}", 1);
        static CastingCostIcon Blue = new CastingCostIcon(CastingCostLabel.BLUE, null, "{U}", 1);
        static CastingCostIcon Black = new CastingCostIcon(CastingCostLabel.BLACK, null, "{B}", 1);
        static CastingCostIcon Red = new CastingCostIcon(CastingCostLabel.RED, null, "{R}", 1);
        static CastingCostIcon Green = new CastingCostIcon(CastingCostLabel.GREEN, null, "{G}", 1);
        static CastingCostIcon WhiteBlue = new CastingCostIcon(CastingCostLabel.WHITE_BLUE, null, "{WB}", 1)
    }

    class CastingCostIcon
    {
        public CastingCostIcon(CastingCostLabel label, BitmapImage icon, string textRepresentation, int cmcContribution)
        {
            Label = label;
            Icon = icon;
            TextRepresentation = textRepresentation;
            CmcContribution = cmcContribution;
        }

        public CastingCostLabel Label { get; private set; }
        public BitmapImage Icon { get; private set; }
        public string TextRepresentation { get; private set; }
        public int CmcContribution { get; private set; }
    }    

    enum CastingCostLabel
    {
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
        TAP
    }
}
