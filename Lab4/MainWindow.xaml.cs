using Lab4.Render;
using Lab4.Services;
using Lab4.Utilities;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lab4
{
    public partial class MainWindow : Window
    {
        private const int _traceLength = 100000;
        private bool _isRaceStarted;
        private HorsesService _horsesService;
        private RenderService _renderService;
        private MainRender Render;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _horsesService = new HorsesService();
            _renderService = new RenderService(_horsesService, new BackgroundGenerator().Generate(_traceLength), () => ((int)FieldGrid.ActualWidth, (int)FieldGrid.ActualHeight));
            GenerateHorsesList(PlayersCounter);
            CurrentHorse(0);
            Render = new MainRender(RenderField, _renderService);
            Render.RenderEvent += () =>
            {
                var horse = _renderService.CurrentFocusHorse;
                var percent = horse.Position * 100.0 / _traceLength;
                SelectedHorse_ProgressBar.Value = percent;
                SelectedHorse_Percent_TextBlock.Text = string.Format("{0:0.##}%", percent);                
            };
            Render.Start();
            PlayersList.ItemsSource = _horsesService.Horses;
        }

        private async void TurnOnAutoSorting()
        {
            while (_isRaceStarted)
            {
                PlayersList.Items.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Descending));
                await Task.Delay(100);
            }
        }

        private void GenerateHorsesList(ComboBox comboBox)
        {
            if (_horsesService is null || _renderService is null) return;
            int count = int.Parse((((ComboBoxItem)comboBox.SelectedItem).Content as TextBlock).Text);
            _horsesService?.GenerateList(count);
            if (!_horsesService.Horses.Contains(_renderService.CurrentFocusHorse)) CurrentHorse(0);
        }

        private async void RaceStarter(object sender, RoutedEventArgs e)
        {
            if (_isRaceStarted)
            {
                _horsesService.StopRace();
            }
            else
            {
                PlayersCounter.IsEnabled = false;
                _isRaceStarted = true;
                StartButton_TextBlock.Text = "Stop";
                TurnOnAutoSorting();

                await _horsesService.StartRaceAsync(_traceLength);

                PlayersCounter.IsEnabled = true;
                _isRaceStarted = false;
                StartButton_TextBlock.Text = "Start";
                PlayersList.Items.SortDescriptions.Add(new SortDescription("Time", ListSortDirection.Ascending));
            }
        }
        private void HorsesCountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => GenerateHorsesList(sender as ComboBox);

        private void ChangeFocus_Click(object sender, RoutedEventArgs e)
        {
            var index = _horsesService.Horses.IndexOf(_renderService.CurrentFocusHorse);
            index = (index + 1) % _horsesService.Horses.Count;
            CurrentHorse(index);
        }

        private void CurrentHorse(int index)
        {
            _renderService.FocusChange(_horsesService.Horses[index]);
            ChangeFocusHorse.Background = new SolidColorBrush(_renderService.CurrentFocusHorse.Color);
        }
    }
}