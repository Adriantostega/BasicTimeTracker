using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BasicTimeTracker.Command;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Model;
using BasicTimeTracker.Properties;

namespace BasicTimeTracker.ViewModel
{
    public class TrackingTimeViewModel : INotifyPropertyChanged
    {
        public const string DateTimeToDisplayFormat = "MM/dd/yyyy HH:mm:ss";
        private string _currentActivityWorking;
        private string _startDate;
        private bool _isCurrentlyTracking;
        private ITrackingRecordDto _currenTrackingRecordDto;
        public ICommand StartTrackingCommand { get; private set; }
        public ICommand StopTrackingCommand { get; private set; }

        public ITrackingRecordDto CurrenTrackingRecordDto
        {
            get { return _currenTrackingRecordDto; }
            private set
            {
                _currenTrackingRecordDto = value; 
                OnPropertyChanged();
            }
        }

        public string CurrentActivityWorking
        {
            get { return _currentActivityWorking; }
            set
            {
                _currentActivityWorking = value;
                OnPropertyChanged();
            }
        }

        public string StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public bool IsCurrentlyTracking
        {
            get { return _isCurrentlyTracking; }
            set
            {
                _isCurrentlyTracking = value;
                OnPropertyChanged();
            }
        }

        public TrackingTimeViewModel(ICommandNotifer startTrackingCommand, ICommandNotifer stoptrackingCommand, ITrackingRecordDto currenTrackingRecordDto)
        {
            startTrackingCommand.NotifyExecuteWasRaised += NotifyExecuteWasRaisedOnStart;
            stoptrackingCommand.NotifyExecuteWasRaised += NotifyExecuteWasRaisedOnStop;

            StartTrackingCommand = startTrackingCommand;
            StopTrackingCommand = stoptrackingCommand;

            CurrenTrackingRecordDto = currenTrackingRecordDto;
            if (currenTrackingRecordDto != null)
            {
                CurrentActivityWorking = currenTrackingRecordDto.ActivityName;
                StartDate = currenTrackingRecordDto.StartDateTime.ToString(DateTimeToDisplayFormat);
                IsCurrentlyTracking = true;
            }
        }

        private void NotifyExecuteWasRaisedOnStop(object sender, EventArgs eventArgs)
        {
            var executeArgumentCommand = eventArgs as ExecuteArgumentCommand;

            if (executeArgumentCommand?.TrackingRecordDto != null)
            {
                CurrentActivityWorking = "";
                StartDate = "";
                IsCurrentlyTracking = false;
            }
        }

        private void NotifyExecuteWasRaisedOnStart(object sender, EventArgs eventArgs)
        {
            var executeArgumentCommand = eventArgs as ExecuteArgumentCommand;

            if (executeArgumentCommand?.TrackingRecordDto != null)
            {
                var trackingRecord = executeArgumentCommand.TrackingRecordDto;
                CurrenTrackingRecordDto = trackingRecord;
                CurrentActivityWorking = trackingRecord.ActivityName;                
                StartDate = trackingRecord.StartDateTime.ToString(DateTimeToDisplayFormat);
                IsCurrentlyTracking = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}