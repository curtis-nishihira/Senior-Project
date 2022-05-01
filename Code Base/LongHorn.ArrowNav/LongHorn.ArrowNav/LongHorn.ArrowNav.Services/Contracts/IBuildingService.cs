using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongHorn.ArrowNav.Services
{ 
    public interface IBuildingService
    {
        // Method to retrieve the building's name from the acronym
        Task<string> RetrieveBuildingNameAsync(string acronym);

        // Method to retrieve a building coordinates(Long and Lat) from the building name
        Task<object> GetCoordsAsync(string buildingName);

        // Method to retrieve the building's acronym from the building name
        Task<string> RetrieveAcronymAsync(string buildingName);

        //Method to retrieve all the buildings in our database
        Task<object> RetrieveAllBuildingsAsync();

    }

}
