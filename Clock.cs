// Copyright (c) 2025 Lukáš Horáček
// SPDX-License-Identifier: MIT

using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ExampleClock
{
    class Clock : StackPanel
    {
        public bool SmoothSeconds = false;

        private Canvas canvas;
        private Ellipse clockOutline;
        private Ellipse clockCenter;
        private Line lineHours;
        private Line lineMinutes;
        private Line lineSeconds;
        private DispatcherTimer tickTimer;

        public Clock()
        {
            canvas = new Canvas();
            canvas.Width = 400;
            canvas.Height = 400;

            clockOutline = new Ellipse();
            clockOutline.Width = 400;
            clockOutline.Height = 400;
            clockOutline.Stroke = Brushes.Black;
            clockOutline.StrokeThickness = 3;
            canvas.Children.Add(clockOutline);

            const double clockCenterDotSize = 7.5D;
            clockCenter = new Ellipse();
            clockCenter.Width = clockCenterDotSize;
            clockCenter.Height = clockCenterDotSize;
            clockCenter.Fill = Brushes.Black;
            Canvas.SetLeft(clockCenter, 200 - clockCenterDotSize / 2.0D);
            Canvas.SetTop(clockCenter, 200 - clockCenterDotSize / 2.0D);
            canvas.Children.Add(clockCenter);

            lineHours = new Line();
            lineHours.X1 = 200;
            lineHours.Y1 = 200;
            lineHours.X2 = 200;
            lineHours.Y2 = 0;
            lineHours.Stroke = Brushes.Black;
            lineHours.StrokeThickness = 5;
            canvas.Children.Add(lineHours);

            lineMinutes = new Line();
            lineMinutes.X1 = 200;
            lineMinutes.Y1 = 200;
            lineMinutes.X2 = 200;
            lineMinutes.Y2 = 0;
            lineMinutes.Stroke = Brushes.Black;
            lineMinutes.StrokeThickness = 3;
            canvas.Children.Add(lineMinutes);

            lineSeconds = new Line();
            lineSeconds.X1 = 200;
            lineSeconds.Y1 = 200;
            lineSeconds.X2 = 200;
            lineSeconds.Y2 = 0;
            lineSeconds.Stroke = Brushes.Black;
            lineSeconds.StrokeThickness = 1;
            canvas.Children.Add(lineSeconds);

            CompositionTarget.Rendering += TickCallback;

            RecalculateLines();
            this.Children.Add(canvas);
        }

        private void RecalculateLines()
        {
            DateTime dt = DateTime.Now;

            double hours12 = dt.Hour % 12 + dt.Minute / 60.0D + dt.Second / 60.0D / 60.0D; // Current hours in 12-hour format
            double minutes = dt.Minute + dt.Second / 60.0D;
            double seconds = dt.Second;
            seconds += dt.Millisecond / 1000.0D + dt.Microsecond / 1000000.0D + dt.Nanosecond / 1000000000.0D;
            if (!SmoothSeconds) seconds = Math.Floor(seconds);

            double hoursAngleDegrees = hours12 / 12.0D * 360.0D - 90.0D;
            double hoursAngleRadians = ToRadians(hoursAngleDegrees);
            Debug.Print($"Angle H: {hoursAngleDegrees} °");
            lineHours.X2 = Math.Cos(hoursAngleRadians) * 200 + 200;
            lineHours.Y2 = Math.Sin(hoursAngleRadians) * 200 + 200;

            double minutesAngleDegrees = minutes / 60.0D * 360.0D - 90.0D;
            double minutesAngleRadians = ToRadians(minutesAngleDegrees);
            Debug.Print($"Angle M: {minutesAngleDegrees} °");
            lineMinutes.X2 = Math.Cos(minutesAngleRadians) * 200 + 200;
            lineMinutes.Y2 = Math.Sin(minutesAngleRadians) * 200 + 200;

            double secondsAngleDegrees = seconds / 60.0D * 360.0D - 90.0D;
            double secondsAngleRadians = ToRadians(secondsAngleDegrees);
            Debug.Print($"Angle S: {secondsAngleDegrees} °");
            lineSeconds.X2 = Math.Cos(secondsAngleRadians) * 200 + 200;
            lineSeconds.Y2 = Math.Sin(secondsAngleRadians) * 200 + 200;
        }

        private double ToRadians(double angle)
        {
            return angle * (Math.PI / 180.0D);
        }

        private void TickCallback(object sender, EventArgs e)
        {
            Debug.WriteLine("Tick");
            RecalculateLines();
        }
    }
}
