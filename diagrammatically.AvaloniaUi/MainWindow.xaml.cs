using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using diagrammatically.Domein;
using diagrammatically.Domein.Interfaces;
using diagrammatically.localDictionary;

namespace diagrammatically.AvaloniaUi
{
    public class MainWindow : Window
    {
        public static MainWindow Singel { get; private set; }

        public ViewModel ViewModel
        {
            private get => _viewModel;
            set
            {
                _viewModel = value;

                Dispatcher.UIThread.InvokeAsync(() => DataContext = _viewModel);
            }
        }
        public Reposetry Reposetry { set; private get; }
        public IMatchCalculator MatchCalculator { set; get; }


        private Button _bttLoadNl;
        private ViewModel _viewModel;
        private Button _bttLoadEn;

        public MainWindow()
        {
            InitializeComponent();
            Singel = this;
        }

        

        public void SetWords(IEnumerable<Option> options)
        {
            Dispatcher.UIThread.InvokeAsync(() => ViewModel.SetWords(options));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);

            _bttLoadNl = this.Find<Button>("BttLoadNl");
            _bttLoadNl.Click += BttLoadNlClick;

            _bttLoadEn = this.Find<Button>("BttLoadEn");
            _bttLoadEn.Click += BttLoadEnClick;
        }

        private void BttLoadNlClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                new DixionaryLoader(Reposetry, MatchCalculator).Load(@"C:\git\Dictionaries\Dutch.dic", "nl");
            });
        }

        private void BttLoadEnClick(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                new DixionaryLoader(Reposetry, MatchCalculator).Load(@"C:\git\Dictionaries\English (British).dic", "en");
            });
        }
    }



    public class ViewModel : INotifyPropertyChanged
    {
        private string _input = null;

        public ViewModel()
        {
            Words = new ObservableCollection<Option>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Option> Words { get; }

        public void SetWords(IEnumerable<Option> newWords)
        {
            Words.Clear();
            newWords.ForEach(Words.Add);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Words"));
        }

    }
}