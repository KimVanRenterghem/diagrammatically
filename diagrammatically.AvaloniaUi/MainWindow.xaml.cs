using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Avalonia.Controls;
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
            
            this.Activated += (sender, args) =>
            {
                Hide();
            };
        }


        public void SetWords(IEnumerable<Option> options)
        {
            Dispatcher.UIThread.InvokeAsync(() => ViewModel.SetWords(options));
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoaderPortableXaml.Load(this);
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