using Quartz;
using Quartz.Core;
using Quartz.Impl;
using Quartz.Spi;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Quartz
{
    class SchedulerFactory : StdSchedulerFactory, ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public SchedulerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override IScheduler Instantiate(QuartzSchedulerResources rsrcs, QuartzScheduler qs)
        {
            
            var sch = base.Instantiate(rsrcs, qs);
            sch.JobFactory = _serviceProvider.GetService<IJobFactory>();
            return sch;
        }
    }
}
