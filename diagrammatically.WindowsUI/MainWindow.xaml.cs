using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using diagrammatically.Domein;

namespace diagrammatically.WindowsUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly InputProseser _inputProseser;


        private readonly ObservableCollection<Option> _options = new ObservableCollection<Option>();

        private void SetOptions(IEnumerable<Option> options)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() =>
                {
                    _options.Clear();
                    options.ForEach(_options.Add);
                }));
        }

        public MainWindow()
        {
            InitializeComponent();

            var optionConsumer = new OptionsConsumer(SetOptions);

            ListOptions.ItemsSource = _options;

            var inputConsumer = new InputConsumer(2);

            _inputProseser = new InputProseser(new[] { inputConsumer }, new[] { optionConsumer });

        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox box)
                _inputProseser.Loockup(box.Text);
        }
    }


    public class InputConsumer : IInputConsumer
    {
        private readonly int _delaiy;

        public InputConsumer(int delaiy)
        {
            _delaiy = delaiy;
        }
        public async Task<IEnumerable<WordMatch>> Consume(string input)
        {
            await Task.Delay(_delaiy);
            return new[]
            {
                new WordMatch(input,input + " een" , 0 , 0, ""),
                new WordMatch(input,input + " twee" , 0 , 0, "")
            };
        }
    }
}
