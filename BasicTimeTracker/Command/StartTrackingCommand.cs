using System;
using BasicTimeTracker.DataAccess;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.Command
{
    public class StartTrackingCommand : ICommandNotifer
    {
        public event EventHandler CanExecuteChanged;
        public EventHandler NotifyExecuteWasRaised { get; set; }
        public ITrackingRecordRepository TrackingRecordRepository { get; private set; }
        public StartTrackingCommand(ITrackingRecordRepository trackingRecordRepository)
        {
            TrackingRecordRepository = trackingRecordRepository;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var activityName = parameter as string;

            if (!string.IsNullOrWhiteSpace(activityName))
            {
                var startDateTime = DateTime.Now;

                var trackingRecordDto = new TrackingRecordDto();
                trackingRecordDto.ActivityName = activityName;
                trackingRecordDto.StartDateTime = startDateTime;

                var recordDto = TrackingRecordRepository.Create(trackingRecordDto);                

                NotifyExecuteWasRaised.Invoke(null, new ExecuteArgumentCommand(recordDto));
            }            
        }       
    }
}