using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DotGame
{
    class BackgroundTimer
    {
        private double _screenWidth;
        private double _screenHeight;

        private Canvas _cible;
        private Timer _timer;

        private Rectangle _rectangle;
        private SolidColorBrush _color;

        public BackgroundTimer(Canvas cible, Timer timer)
        {
            this._screenWidth = cible.ActualWidth;
            this._screenHeight = cible.ActualHeight;
            this._cible = cible;
            this._timer = timer;
            this._timer.PropertyChanged += _timer_PropertyChanged;
            this.initRectangle();
            this.Draw();
        }

        private void Draw()
        {
            this._cible.Children.Add(this._rectangle);
        }

        private void initRectangle()
        {
            this._rectangle = new Rectangle();
            this._color = new SolidColorBrush(Colors.Blue);
            this._rectangle.MaxHeight = this._screenHeight;
            this._rectangle.MaxWidth = this._screenWidth;
            this._rectangle.Width = this._screenWidth;
            this._rectangle.Height = this._screenHeight / 2;
            this._rectangle.Fill = this._color;
            this._rectangle.HorizontalAlignment = HorizontalAlignment.Left;
            this._rectangle.VerticalAlignment = VerticalAlignment.Top;
        }

        void _timer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var size = System.Math.Abs(this._timer.Time.TotalSeconds * (this._screenHeight / 20));
            this._rectangle.Height = (size);
            this._rectangle.RenderTransform = new TranslateTransform() { Y = this._screenHeight - size};
        }
    }
}
