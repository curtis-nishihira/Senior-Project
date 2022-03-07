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

        public int GetTraffic(TrafficModel model)
        {
            TrafficRepository trafficRepo = new TrafficRepository();
            List<String> value= trafficRepo.Read(model);
            return Int32.Parse(value[0]);
        }

        public Dictionary<String, int> GetAllTraffic(TrafficModel model)
        {
            TrafficRepository trafficRepo = new TrafficRepository();
            List<String> value = trafficRepo.GetAllValues(model);
            Dictionary<String,int> keyValuePairs = new Dictionary<String,int>();
            for (int i = 0; i < value.Count; i++)
            {
                keyValuePairs.Add(value[i].Split(" ")[0], Int32.Parse(value[i].Split(" ")[1]));
            }
            return keyValuePairs;
        }
    }
}
