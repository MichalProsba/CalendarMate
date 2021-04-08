using System;
using Syncfusion.UI.Xaml.Charts;

namespace WeatherChartStyle
{
    public static class SetStyle
    {
        public static ChartAdornmentInfo getChartAdornmentInfo()
        {
            ChartAdornmentInfo adornmentInfo = new ChartAdornmentInfo()
            {
                ShowMarker = true,
                Symbol = ChartSymbol.Diamond,
                SymbolHeight = 5,
                SymbolWidth = 5,
                SymbolInterior = new SolidColorBrush(Colors.Black),
                ShowLabel = true,
                LabelPosition = AdornmentsLabelPosition.Outer,
                Foreground = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Colors.Black),
                Background = new SolidColorBrush(Colors.DarkGray),
                BorderThickness = new Thickness(1),
                Margin = new Thickness(1),
                FontStyle = FontStyles.Italic,
                FontFamily = new FontFamily("Calibri"),
                FontSize = 11
            };
        }
    }
}
