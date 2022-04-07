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

        public Dictionary<String, Tuple<int, int>> GetAllTraffic(TrafficModel model)
        {
            TrafficRepository trafficRepo = new TrafficRepository();
            List<String> value = trafficRepo.GetAllValues(model);
            Dictionary<String, Tuple<int, int>> keyValuePairs = new Dictionary<String,Tuple<int,int>>();
            for (int i = 0; i < value.Count; i++)
            {
                var tuple = Tuple.Create(Int32.Parse(value[i].Split(" ")[1]), Int32.Parse(value[i].Split(" ")[2]));

                keyValuePairs.Add(value[i].Split(" ")[0], tuple );
            }
            return keyValuePairs;
        }

        public List<string> GetBuildings()
        {
            TrafficRepository trafficRepo = new TrafficRepository();
            List<String> value = trafficRepo.GetAllBuildings();
            return value;
        }
    }
}
