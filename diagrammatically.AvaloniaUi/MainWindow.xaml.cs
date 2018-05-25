using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
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

        public MainWindow()
        {
            InitializeComponent();

            var optionConsumers = new[] {
                new OptionsConsumer(SetWords)
            };

            var calculator = new MatchCalculator();

            var repo = new Reposetry(calculator);

            var localFinder = new LocalFinder(calculator, repo);

            Task.Run(() =>
            {
                new DixionaryLoader(repo).Load(@"C:\git\Dictionaries\Dutch.dic", "nl");
            });


            var inputConsumers = new IInputConsumer[] {
                new SearchConsumer(),
                localFinder
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
            AvaloniaXamlLoaderPortableXaml.Load(this);
        }

    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string _input;
        private readonly InputProseser _inputProseser;

        public ViewModel(InputProseser inputProseser)
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