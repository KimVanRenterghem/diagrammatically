using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using diagrammatically.Domein;
using diagrammatically.localDictionary;
using diagrammatically.oxforddictionaries;

namespace diagrammatically.AvaloniaUi
{
    public class MainWindow : Window
    {
        private ViewModel _vm;
        private Reposetry _reposetry;
        private IMatchCalculator _matchCalculator;
        private Button _bttLoadNl;

        public MainWindow()
        {
            InitializeComponent();

            BuildDependencys();
        }

        private void BuildDependencys()
        {
            var optionConsumers = new[]
            {
                new OptionsConsumer(SetWords)
            };

            _matchCalculator = new MatchCalculator();

            _reposetry = new Reposetry(_matchCalculator);

            var localFinder = new LocalFinder(_matchCalculator, _reposetry);

            var inputConsumers = new IInputConsumer[]
            {
                new SearchConsumer(),
                localFinder
            };

            var inputProseser = new InputProseser(inputConsumers, optionConsumers);
            var splitter = new CreateWordInPut(inputProseser);
            _vm = new ViewModel(splitter);

            DataContext = _vm;
        }

        private void SetWords(IEnumerable<Option> options)
        {
            Dispatcher.UIThread.InvokeAsync(() => _vm.SetWords(options));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);

            _bttLoadNl = this.Find<Button>("BttLoadNl");
            _bttLoadNl.Click += BttLoadNlClick;
        }

        private void BttLoadNlClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                new DixionaryLoader(_reposetry, _matchCalculator, new OptionsConsumer(SetWords)).Load(@"C:\git\Dictionaries\Dutch.dic", "nl");
            });
        }
    }



    public class ViewModel : INotifyPropertyChanged
    {
        private string _input;
        private readonly IInputProseser _inputProseser;

        public ViewModel(IInputProseser inputProseser)
        {
            Words = new ObservableCollection<Option>();
            _inputProseser = inputProseser;
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
                _inputProseser.Loockup(_input, new[] { "nl" });

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Input"));

                if(string.IsNullOrEmpty(_input))
                    SetWords(new Option[0]);
            }
        }

        public ObservableCollection<Option> Words { get; }

        public void SetWords(IEnumerable<Option> newWords)
        {
            Words.Clear();
            newWords.ForEach(Words.Add);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Words"));
        }

    }
}