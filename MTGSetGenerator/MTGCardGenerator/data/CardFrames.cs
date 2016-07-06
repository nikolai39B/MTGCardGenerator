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
    public static class CardFrames
    {
        public static void InitCardFrames()
        {
            Application app = Application.Current;

            Frames = new Dictionary<Label, CardFrame>()
            {
                { Label.NONE, new CardFrame(null, null, null) },
                { Label.SILVER, new CardFrame(app.FindResource("img_FrameSilver") as BitmapImage, null, null) },
                { Label.WHITE, new CardFrame(app.FindResource("img_FrameWhite") as BitmapImage, null, null) },
                { Label.BLUE, new CardFrame(app.FindResource("img_FrameBlue") as BitmapImage, null, null) },
                { Label.BLACK, new CardFrame(app.FindResource("img_FrameBlack") as BitmapImage, null, null) },
                { Label.RED, new CardFrame(app.FindResource("img_FrameRed") as BitmapImage, null, null) },
                { Label.GREEN, new CardFrame(app.FindResource("img_FrameGreen") as BitmapImage, null, null) },
                { Label.GOLD, new CardFrame(app.FindResource("img_FrameGold") as BitmapImage, null, null) },
            };
        }

        public static Dictionary<Label, CardFrame> Frames;

        public enum Label
        {
            NONE,

            SILVER,
            WHITE,
            BLUE,
            BLACK,
            RED,
            GREEN,
            GOLD
        }

        public class CardFrame
        {
            public CardFrame(BitmapImage defaultFrame, BitmapImage creatureFrame, BitmapImage planeswalkerFrame)
            {
                DefaultFrame = defaultFrame;
                CreatureFrame = creatureFrame;
                PlaneswalkerFrame = planeswalkerFrame;
            }

            public BitmapImage DefaultFrame { get; private set; }
            public BitmapImage CreatureFrame { get; private set; }
            public BitmapImage PlaneswalkerFrame { get; private set; }
        }
    }
}
