using Bubblz.Strings;
using Bubblz.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Bubblz.Model
{
    public class Level_01 : BaseClass, ILevel
    {
		public MainViewModel Parent { get; set; }
		public string Name => Strings.Strings.Name_Level01;

		public string Description => Strings.Strings.Description_Level01;

		private bool _IsCompleted;
		public bool IsCompleted
		{
			get { return _IsCompleted; }
			set { _IsCompleted = value; RaisePropertyChanged(nameof(IsCompleted)); }
		}

		private bool _IsAvailable;
		public bool IsAvailable
		{
			get { return _IsAvailable; }
			set { _IsAvailable = value; RaisePropertyChanged(nameof(IsAvailable)); }
		}

		public ObservableCollection<Ball> Balls { get; set; } = new ObservableCollection<Ball>();

		public RelayCommand StartLevelCommand { get; private set; } = new RelayCommand((param) => StartLevelCommand_Execute(param), (param) => StartLevelCommand_CanExecute(param));

		static void StartLevelCommand_Execute (object parameter)
		{
			if (parameter is ILevel lvl)
			{
				var view = lvl.CreateView();
				Window.Current.Content = view;
				lvl.Parent.Reset();
				lvl.Parent.GrowBallTimer.Start();
			}
		}

		public void GrowBallTimer_Tick(object sender, object e)
		{
			Parent.NewBall.Diameter += 0.1;
		}

		static bool StartLevelCommand_CanExecute (object parameter)
		{
			if (parameter is ILevel level)
			{
				return level.IsAvailable;
			}
			return false;
		}

		public Page CreateView()
		{
			var view = new Level.Level_01() { ViewModel = Parent };
			Parent.CurrentLevel = this;
			Parent.Reset();
			return view;
		}



	}
}
