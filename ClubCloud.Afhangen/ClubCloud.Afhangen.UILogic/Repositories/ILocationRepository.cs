using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
	public interface ILocationRepository
	{
        Task<ObservableCollection<LocationModel>> GetLocationsByIpAddress(string query);
		Task<ObservableCollection<LocationModel>> GetLocationsByAutoCompleteText(string query);
		Task<ObservableCollection<LocationModel>> GetLocationsByText(string query);
		Task<ObservableCollection<LocationModel>> GetLocationsByLatLon(double latitude, double longitude);
		Task<ObservableCollection<LocationModel>> GetLocationsByLocationKey(string key);
        Task<Geoposition> GetLocationAsync();
	}
}
