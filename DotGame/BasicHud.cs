using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DotGame
{
    class BasicHud : IHud
    {
        private TextBlock textScore;
        private Score score;

        public BasicHud(TextBlock text)
        {
            this.textScore = text;
        }

        public void DrawScore(Score score)
        {
            this.score = score;
            this.score.PropertyChanged += score_PropertyChanged;
        }

        void score_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            textScore.Text = score.Points.ToString();
        }
    }
}
