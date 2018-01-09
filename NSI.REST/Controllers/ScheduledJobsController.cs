using System;
using NSI.BLL.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace NSI.REST.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ScheduledJobsController:Controller
    {
        private readonly IScheduledJobService _scheduledJobService;
        public ScheduledJobsController(IScheduledJobService scheduledJobService)
        {
            _scheduledJobService = scheduledJobService;
        }

        [HttpGet("/schedulejobs")]
        public void ScheduleJobs(){
            RecurringJob.AddOrUpdate(()=> _scheduledJobService.DailyJob(),Cron.Daily());
            RecurringJob.AddOrUpdate(()=> _scheduledJobService.MonthlyJob(), Cron.Monthly());
        }
    }
}
