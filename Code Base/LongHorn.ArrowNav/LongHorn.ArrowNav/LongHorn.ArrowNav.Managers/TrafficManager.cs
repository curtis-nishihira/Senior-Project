using LongHorn.ArrowNav.Models;
using LongHorn.ArrowNav.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Managers
{
    public class TrafficManager
    {
        public string UpdateZoneValues(TrafficModel model)
        {
            TrafficService trafficService = new TrafficService();
            var response = trafficService.UpdateTraffic(model);
            return response;
        }

        public Dictionary<String,Tuple<int,int>> GetZonevalues(TrafficModel model)
        {
            TrafficService trafficService = new TrafficService();
            var response = trafficService.GetAllTraffic(model);
            return response;
        }

        public List<string> getBuildings()
        {
            TrafficService trafficService = new TrafficService();
            var result = trafficService.GetBuildings();
            return result;
        }

    }
}
