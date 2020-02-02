
using Microsoft.Toolkit.Uwp.UI.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Bubblz.Model
{
    public class Ball : BaseClass
    {
       
        double _Diameter;
        public double Diameter
        {
            get => _Diameter;
            set { _Diameter = value; RaisePropertyChanged(nameof(Diameter)); RaisePropertyChanged(nameof(Top)); RaisePropertyChanged(nameof(Left)); }
        }


        double _X;
        public double X
        {
            get => _X;
            set { _X = value; RaisePropertyChanged(nameof(X)); RaisePropertyChanged(nameof(Left)); }
        }


        double _Y;
        public double Y
        {
            get => _Y;
            set { _Y = value; RaisePropertyChanged(nameof(Y)); RaisePropertyChanged(nameof(Top)); }
        }

        public double Left => _X - Diameter / 2;
        public double Top => _Y - Diameter / 2;


        Color _Color = default;
        public Color Color
        {
            get => _Color;
            set { _Color = value; RaisePropertyChanged(nameof(Color)); RaisePropertyChanged(nameof(Fill)); RaisePropertyChanged(nameof(Stroke)); }
        }

        public RadialGradientBrush Fill
            => new RadialGradientBrush(Colors.White, Color)
            {
                AlphaMode = AlphaMode.Premultiplied,
                FallbackColor = Colors.White,
                ColorInterpolationMode = ColorInterpolationMode.ScRgbLinearInterpolation,
                GradientOrigin = new Point(0.7, 0.2),
                SpreadMethod = GradientSpreadMethod.Pad
            };


        public SolidColorBrush Stroke => new SolidColorBrush(Color);

        public bool IntersectsOtherBall (Ball BallToCompare)
        {
            var dist = Math.Sqrt(Math.Pow(this.X - BallToCompare.X, 2) + Math.Pow(this.Y - BallToCompare.Y, 2));
            return dist < this._Diameter / 2 + BallToCompare.Diameter / 2;
        }

        readonly Random random = new Random();
        public void SetRandomColor()
        {
            Color = Color.FromArgb(100, (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255));
        }

    }
}
