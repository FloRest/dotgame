using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DotGame
{
    class PixelArtGraphic : BasicGraphic
    {
        private List<Tuple<Dot, Image>> ListDot;
        private BitmapImage CoinImage;
        private BitmapImage MalusImage;


        public PixelArtGraphic(Canvas canvas) : base(canvas)
        {
            ListDot = new List<Tuple<Dot, Image>>();
            CoinImage = new BitmapImage();
            CoinImage.UriSource = new Uri("/Assets/Graphics/PixelArt/coin_1.png", UriKind.Relative);
            MalusImage = new BitmapImage();
            MalusImage.UriSource = new Uri("/Assets/Graphics/PixelArt/coin_2.png", UriKind.Relative);
        }

        public override void DrawDot(Dot dot)
        {
            Image test = new Image();
            ListDot.Add(new Tuple<Dot, Image>(dot, test));
            switch (dot.Type)
            {
                case DotType.Coin :
                    test.Source = CoinImage;
                    break;
                case DotType.Malus :
                    test.Source = MalusImage;
                    break;
                default:
                    test.Source = CoinImage;
                    break;
            }
            test.Height = dot.size;
            test.Width = dot.size;
            test.Tap += test_Tap;

            Canvas.Children.Add(test);
            Canvas.SetLeft(test, dot.TopLeft.X);
            Canvas.SetTop(test, dot.TopLeft.Y);
        }


        void test_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var image = (Image)(sender);
            int index = ListDot.FindIndex(t => t.Item2 == image);
            if (index != -1)
            {
                var dot = ListDot[index].Item1;
                OnTapDot(dot, new DotEvent("tap"));
            }
        }

        public override void UndrawDot(Dot dot)
        {
            int index = ListDot.FindIndex(t => t.Item1 == dot);
            if (index != -1)
            {
                var ellipse = ListDot[index].Item2;
                Canvas.Children.Remove(ellipse);
                ListDot.RemoveAt(index);
            }
        }

        public override void ClearScreen()
        {
            ListDot.Clear();
            base.ClearScreen();
        }
    }
}
