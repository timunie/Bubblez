using Bubblz.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Bubblz.Model
{
    public interface ILevel
    { 
        MainViewModel Parent { get; set; }
        string Name { get; }
        string Description { get; }
        bool IsCompleted { get; set; }
        bool IsAvailable { get; set; }

        ObservableCollection<Ball> Balls { get; set; }

        Page CreateView();

        void GrowBallTimer_Tick(object sender, object e);

    }
}
