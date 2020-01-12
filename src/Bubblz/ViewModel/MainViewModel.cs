using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Bubblz.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public DispatcherTimer GrowBallTimer { get; } = new DispatcherTimer();

        public MainViewModel()
        {
            NewBall.SetRandomColor();
            GrowBallTimer.Tick += GrowBallTimer_Tick; ;
            GrowBallTimer.Interval = TimeSpan.FromMilliseconds(100);
            GrowBallTimer.Start();
        }


        public Ball NewBall { get; } = new Ball() { Diameter = 10 };

        public ObservableCollection<Ball> Balls { get; } = new ObservableCollection<Ball>();

        private void GrowBallTimer_Tick(object sender, object e)
        {
            NewBall.Diameter += 1;
        }

        public void Reset()
        {
            NewBall.Diameter = 10;
            Balls.Clear();
        }

    }
}
