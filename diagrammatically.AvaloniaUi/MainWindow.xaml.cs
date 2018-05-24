using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using diagrammatically.Domein;
using diagrammatically.oxforddictionaries;

namespace diagrammatically.AvaloniaUi
{
    public class MainWindow : Window
    {
        private ViewModel _vm;

        public MainWindow()
        {
            InitializeComponent();

            var optionConsumers = new[] {
                new OptionsConsumer(SetWords)
            };

            var inputConsumers = new[] {
                new SearchConsumer()
            };


            var inputProseser = new InputProseser(inputConsumers, optionConsumers);

            _vm = new ViewModel(inputProseser);

            DataContext = _vm;
        }

        private void SetWords(IEnumerable<Option> options)
        {
            Dispatcher.UIThread.InvokeAsync(() => _vm.SetWords(options));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string _input;
        private readonly InputProseser _inputProseser;

        public ViewModel(InputProseser inputProseser)
        {
            this._inputProseser = inputProseser;
        }

        public event PropertyChangedEventHandler PropertyChanged;



        public string Input
        {
            get => _input;
            set
            {
                if (_input == value)
                    return;
                _input = value;
                _inputProseser.Loockup(_input);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input"));
            }
        }

        private readonly ObservableCollection<Option> words = new ObservableCollection<Option>();

        public ObservableCollection<Option> Words
            => words;

        public void SetWords(IEnumerable<Option> newWords)
        {
            words.Clear();
            newWords.ForEach(words.Add);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Words"));
        }

    }
}