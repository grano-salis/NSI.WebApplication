using System;
namespace NSI.BLL.Interfaces
{
    public interface IScheduledJobService
    {
        void DailyJob();
        void MonthlyJob();
    }
}
