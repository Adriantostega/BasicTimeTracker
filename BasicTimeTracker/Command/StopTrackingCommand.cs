using System;
using System.Linq;
using BasicTimeTracker.DataAccess;
using BasicTimeTracker.Dto;

namespace BasicTimeTracker.Command
{
    public class StopTrackingCommand : ICommandNotifer
    {
        public event EventHandler CanExecuteChanged;
        public EventHandler NotifyExecuteWasRaised { get; set; }
        public ITrackingRecordRepository TrackingRecordRepository { get; private set; }
        public StopTrackingCommand(ITrackingRecordRepository trackingRecordRepository)
        {
            TrackingRecordRepository = trackingRecordRepository;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var trackingRecorDto = parameter as ITrackingRecordDto;            
            if (trackingRecorDto != null)
            {
                trackingRecorDto.EndDateTime = DateTime.Now;
                TrackingRecordRepository.Update(trackingRecorDto);
                var readLastRecord = TrackingRecordRepository.ReadAll().LastOrDefault();
                NotifyExecuteWasRaised.Invoke(null, new ExecuteArgumentCommand(readLastRecord));
            }
        }        
    }
}