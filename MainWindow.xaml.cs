using System.Windows;

namespace ExampleClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Clock.SmoothSeconds = SmoothSecondHandCheckbox.IsChecked.GetValueOrDefault(false);
        }

        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(SmoothSecondHandCheckbox))
            {
                Clock.SmoothSeconds = SmoothSecondHandCheckbox.IsChecked.GetValueOrDefault(false);
            }
        }
    }
}