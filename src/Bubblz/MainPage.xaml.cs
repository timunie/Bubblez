using Bubblz.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Bubblz
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        MainViewModel ViewModel => DataContext as MainViewModel;

        public MainPage()
        { 
            this.InitializeComponent();
        }

        private void Page_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            var pos = e.GetCurrentPoint(this).Position;
            ViewModel.NewBall.X = pos.X;
            ViewModel.NewBall.Y = pos.Y;
        }

        private async void Page_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            var ballToAdd = new Ball 
            { 
                X = ViewModel.NewBall.X, 
                Y = ViewModel.NewBall.Y, 
                Diameter = ViewModel.NewBall.Diameter,
                Color = ViewModel.NewBall.Color
            };

            if (ViewModel.Balls.Any(x => x.IntersectsOtherBall(ballToAdd)))
            {
                var dlg = new MessageDialog("Das war's :-(", "Ende");
                
                ViewModel.NewBall.Diameter = 0;
                ViewModel.GrowBallTimer.Stop();

                dlg.Commands.Add(new UICommand { Label = "Noch mal", Id = 0 });
                dlg.Commands.Add(new UICommand { Label = "Ende", Id = 1 });

                this.PointerMoved -= Page_PointerMoved;
                this.PointerReleased -= Page_PointerReleased;

                var result = await dlg.ShowAsync();

                switch (result.Id)
                {
                    case 0:
                        ViewModel.Reset();
                        ViewModel.GrowBallTimer.Start();
                        this.PointerMoved += Page_PointerMoved;
                        this.PointerReleased += Page_PointerReleased;
                        break;
                    default:
                        App.Current.Exit();
                        break;
                }
            }
            else
            {
ViewModel.Balls.Add(ballToAdd);
            ViewModel.NewBall.SetRandomColor();
            }

            
        }
    }
}
