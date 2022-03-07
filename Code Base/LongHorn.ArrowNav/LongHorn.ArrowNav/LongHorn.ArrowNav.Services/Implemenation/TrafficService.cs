using LongHorn.ArrowNav.DAL;
using LongHorn.ArrowNav.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Services
{
    public class TrafficService
    {
        public string UpdateTraffic(TrafficModel model)
        {
            TrafficRepository trafficRepo = new TrafficRepository();
            var response = trafficRepo.Update(model);
            return response;

        }
    }
}
