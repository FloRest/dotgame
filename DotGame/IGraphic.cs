using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotGame
{
    delegate void DotEventHandler(object source, DotEvent e);

    interface IGraphic
    {
        event DotEventHandler TapDot;

        void DrawDot(Dot d);
        void UndrawDot(Dot dot);
        void DrawHitBox(Rectangle r);
        void UndrawHitBox(Rectangle r);
        void ClearScreen();
        void DrawTimer(Timer timer);
    }
}
