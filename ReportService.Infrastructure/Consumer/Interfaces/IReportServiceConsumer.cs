using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Consumer.Interfaces
{
    public interface IReportServiceConsumer
    {
        void Consume(CancellationToken stoppingToken);
    }
}
