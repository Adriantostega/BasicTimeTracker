using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using BasicTimeTracker.Command;
using BasicTimeTracker.DataAccess;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Util;
using BasicTimeTracker.ViewModel;

namespace BasicTimeTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string ActivityNameToTrack = "Activity Name To Track...";
        private readonly TrackingTimeViewModel _trackingTimeViewModel;
        public MainWindow()
        {
            InitializeComponent();
            Topmost = true;
            var buildTrackingRecordRepository = BuildTrackingRecordRepository();
            ITrackingRecordDto currentTrackingRecordDto = null;
            var readLastRecord = buildTrackingRecordRepository.ReadAll().LastOrDefault();
            if (readLastRecord != null && !readLastRecord.EndDateTime.HasValue)
            {
                currentTrackingRecordDto = readLastRecord;
            }
            _trackingTimeViewModel = new TrackingTimeViewModel(new StartTrackingCommand(buildTrackingRecordRepository), new StopTrackingCommand(buildTrackingRecordRepository), currentTrackingRecordDto);
            DataContext = _trackingTimeViewModel;
            _trackingTimeViewModel.PropertyChanged += TrackingTimeViewModelOnPropertyChanged;
            ActivityTrackingTextBox.Text = ActivityNameToTrack;
            Closing += Window_Closing;
        }

        private ITrackingRecordRepository BuildTrackingRecordRepository()
        {
            var textFileManager = new TextFileManager();
            var textFileTrackingRecordFormatManager = new TextFileTrackingRecordFormatManager(null);
            var textLineReader = new TextLineReader();
            var textFileTrackingRecordCreator = new TextFileTrackingRecordCreator(textFileManager, textLineReader, textFileTrackingRecordFormatManager);
            var textFileTrackingRecordFileManager = new TextFileTrackingRecordFileManager(textFileManager, textFileTrackingRecordFormatManager);
            var textFileTrackingRecordDataSource = new TextFileTrackingRecordDataSource(textFileManager,new DirectoryManager(),textLineReader,textFileTrackingRecordFormatManager,textFileTrackingRecordCreator, textFileTrackingRecordFileManager);
            return new TrackingRecordRepository(textFileTrackingRecordDataSource);
        }

        private void TrackingTimeViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(TrackingTimeViewModel.CurrentActivityWorking))
            {
                if (string.IsNullOrEmpty(_trackingTimeViewModel.CurrentActivityWorking))
                {
                    ActivityTrackingTextBox.Text = ActivityNameToTrack;
                }                
            }
        }
        private void Window_Closing(object sender, CancelEventArgs cancelEventArgs)
        {
            cancelEventArgs.Cancel = true;
        }
        private void ActivityTrackingTextBox_OnGotFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            ActivityTrackingTextBox.Text = "";
        }
        private void ActivityTrackingTextBox_OnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            if (String.IsNullOrWhiteSpace(ActivityTrackingTextBox.Text))
                ActivityTrackingTextBox.Text = ActivityNameToTrack;
        }
    }
}
