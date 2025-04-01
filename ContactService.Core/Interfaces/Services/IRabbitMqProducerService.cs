using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Core.Interfaces.Services
{
    public interface IRabbitMqProducerService
    {
        void PublishMessage(string message);
    }
}
