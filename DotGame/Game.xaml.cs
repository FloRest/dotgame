using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Media;
using System.Windows.Threading;

namespace DotGame
{
    public partial class Game : PhoneApplicationPage
    {
        private Core core;
        private bool sound;
        private ISound soundInstance;

        public Game() 
        {
            InitializeComponent();
            Loaded += Game_Loaded;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            sound = (bool)(PhoneApplicationService.Current.State["sound"]);
            soundInstance = new NoSound();
            if (sound == true)
                soundInstance = new _8bitSound();
            core = new Core(new PixelArtGraphic(gameContent), soundInstance, new BasicHud(scoreDisplay));
            core.Stop();
            base.OnNavigatedTo(e);
        }

        void Game_Loaded(object sender, RoutedEventArgs e)
        {
            core.Play();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void gameContent_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {

        }
    }
}