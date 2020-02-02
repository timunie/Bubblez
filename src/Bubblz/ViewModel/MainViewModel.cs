using Bubblz.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Bubblz.ViewModel
{
    public class MainViewModel : BaseClass
    {
        public DispatcherTimer GrowBallTimer { get; private set; } = new DispatcherTimer();

        public MainViewModel()
        {
            //NewBall.SetRandomColor();
            //GrowBallTimer.Tick += GrowBallTimer_Tick; ;
            //GrowBallTimer.Interval = TimeSpan.FromMilliseconds(100);
            //GrowBallTimer.Start();

            Levels.CollectionChanged += Levels_CollectionChanged;

            Levels.Add(new Level_01 { IsAvailable = true });
        }

        private void Levels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ILevel item in e.NewItems)
                {
                    item.Parent = this;
                }
            }
        }

        public Ball NewBall { get; } = new Ball() { Diameter = 10 };

        public ObservableCollection<ILevel> Levels { get; } = new ObservableCollection<ILevel>();
        public ILevel CurrentLevel { get; set; } 

        public void Reset()
        {
            NewBall.Diameter = 10;
            CurrentLevel.Balls.Clear();

            GrowBallTimer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            GrowBallTimer.Tick += CurrentLevel.GrowBallTimer_Tick;
        }

    }
}
