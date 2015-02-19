using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public interface ILocationService
    {

        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.Location> FindLocationByKey(string key);
        System.Threading.Tasks.Task<ClubCloud.Afhangen.UILogic.Models.LocationDetail> FindLocationDetailByKey(string key);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.LocationBase>> FindLocationsAutoCompleteText(string query);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.LocationDetail>> FindLocationsByIpAddress();
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.LocationDetail>> FindLocationsByLatLon(double latitude, double longitude);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.Location>> FindLocationsByText(string query);
        System.Threading.Tasks.Task<System.Collections.Generic.List<ClubCloud.Afhangen.UILogic.Models.LocationDetail>> FindLocationsDetailedByText(string query);
        bool GetIsInternetConnected();
        System.Threading.Tasks.Task<global::Windows.Devices.Geolocation.Geoposition> GetLocationAsync();
    }
}
