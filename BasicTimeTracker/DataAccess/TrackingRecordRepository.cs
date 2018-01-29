using System;
using System.Collections.Generic;
using System.Linq;
using BasicTimeTracker.CustomException;
using BasicTimeTracker.Dto;
using BasicTimeTracker.Model;

namespace BasicTimeTracker.DataAccess
{
    public class TrackingRecordRepository : ITrackingRecordRepository
    {
        //Validate if exist on the database before to update or delete
        public ITrackingRecordDataSource TrackingRecordDataSource { get; private set; }

        public TrackingRecordRepository(ITrackingRecordDataSource trackingRecordDataSource)
        {
            TrackingRecordDataSource = trackingRecordDataSource;
        }

        public List<ITrackingRecordDto> ReadAll()
        {
            var trackingRecords = TrackingRecordDataSource.GetAllRecords();
            var trackingRecordsByDatetime = trackingRecords.OrderBy(tr => tr.StartDateTime);

            var trackingRecordDtos = new List<ITrackingRecordDto>();

            foreach (var trackingRecordEntity in trackingRecordsByDatetime)
            {
                trackingRecordDtos.Add(new TrackingRecordWithEntityDto(trackingRecordEntity));
            }
                        
            return trackingRecordDtos;
        }

        public ITrackingRecordDto Create(ITrackingRecordDto trackingRecordDto)
        {           
            if (string.IsNullOrWhiteSpace(trackingRecordDto?.ActivityName))
            {
                throw new InvalidTrackingRecord("The TrackingRecord data is not valid.");
            }

            var recordEntity = new TrackingRecordEntity
            {                
                ActivityName = trackingRecordDto.ActivityName,
                StartDateTime = trackingRecordDto.StartDateTime,
                EndDateTime = trackingRecordDto.EndDateTime
            };

            var trackingRecordEntityCreated = TrackingRecordDataSource.Create(recordEntity);
            return new TrackingRecordWithEntityDto(trackingRecordEntityCreated);
        }

        public void Update(ITrackingRecordDto trackingRecordDto)
        {
            var trackingRecordWithEntityDto = trackingRecordDto as TrackingRecordWithEntityDto;

            if (trackingRecordWithEntityDto == null || trackingRecordWithEntityDto.TrackingRecordEntity == NullableTrackingRecordEntity.Instance())
            {
                throw new InvalidTrackingRecord("The TrackingRecord does not exist on the database.");
            }

            trackingRecordWithEntityDto.UpdateEntity();        

            TrackingRecordDataSource.Update(trackingRecordWithEntityDto.TrackingRecordEntity);
        }

        public void Delete(ITrackingRecordDto trackingRecordDto)
        {
            var trackingRecordWithEntityDto = trackingRecordDto as TrackingRecordWithEntityDto;

            if (trackingRecordWithEntityDto == null || trackingRecordWithEntityDto.TrackingRecordEntity == NullableTrackingRecordEntity.Instance())
            {
                throw new InvalidTrackingRecord("The TrackingRecord does not exist on the database.");
            }

            TrackingRecordDataSource.Delete(trackingRecordWithEntityDto.TrackingRecordEntity.Id);
        }
    }
}