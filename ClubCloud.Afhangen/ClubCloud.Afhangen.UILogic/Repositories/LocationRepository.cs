using ClubCloud.Afhangen.UILogic.Models;
using ClubCloud.Afhangen.UILogic.Models.Entities;
using ClubCloud.Afhangen.UILogic.Services;
using Microsoft.Practices.Prism.StoreApps.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private const String ApiVersion = "v1";

        private const String ApiUrl = "http://api.accuweather.com/";

        private const String ApiKey = "236c108414d44b3fa119057d0e81a5e1";

        public ILocationService _locationService;

        public LocationRepository(ILocationService locationService, IResourceLoader resourceLoader)
        {
            _locationService = locationService;// new LocationServiceProvider("236c108414d44b3fa119057d0e81a5e1", "http://api.accuweather.com/", "v1", "en-us");
            //this._locationServiceProvider.SetLanguageCode("nl-nl");
        }

        public async Task<Geoposition> GetLocationAsync()
        {
            return await _locationService.GetLocationAsync();
        }

        public async Task<ObservableCollection<LocationModel>> GetLocationsByAutoCompleteText(String query)
        {
            
            List<LocationBase> locationBases = await _locationService.FindLocationsAutoCompleteText(query);
            return this.MapCityData(locationBases);
        }

        public async Task<ObservableCollection<LocationModel>> GetLocationsByIpAddress(String query)
        {
            return this.MapCityDataDetailed(await _locationService.FindLocationsByIpAddress());
        }

        public async Task<ObservableCollection<LocationModel>> GetLocationsByLatLon(Double latitude, Double longitude)
        {
            List<LocationDetail> locationDetails = await _locationService.FindLocationsByLatLon(latitude, longitude);
            return this.MapCityDataDetailed(locationDetails);
        }

        public async Task<ObservableCollection<LocationModel>> GetLocationsByLocationKey(String key)
        {
            ObservableCollection<LocationModel> observableCollection;
            String str;
            String str1;
            String localizedName;
            String str2;
            ObservableCollection<LocationModel> observableCollection1 = new ObservableCollection<LocationModel>();
            LocationDetail locationDetail = await _locationService.FindLocationDetailByKey(key);
            if (locationDetail != null)
            {
                String d = "";
                if (locationDetail.Details.DMA != null)
                {
                    d = locationDetail.Details.DMA.ID;
                }
                LocationModel locationModel = new LocationModel();
                LocationModel locationModel1 = locationModel;
                str = (!String.IsNullOrEmpty(locationDetail.LocalizedName) ? locationDetail.LocalizedName : locationDetail.EnglishName);
                locationModel1.DisplayName = str;
                locationModel.LocationId = locationDetail.Key;
                locationModel.Latitude = locationDetail.GeoPosition.Latitude.ToString();
                locationModel.Longitude = locationDetail.GeoPosition.Longitude.ToString();
                locationModel.GmtOffSet = (Double)locationDetail.TimeZone.GmtOffset;
                locationModel.DmaId = d;
                locationModel.VideoCode = locationDetail.Details.VideoCode;
                LocationModel locationModel2 = locationModel;
                str1 = (locationDetail.Country.ID.ToLower().Equals("us") ? locationDetail.Details.NXState : locationDetail.Details.Satellite);
                locationModel2.MapCode = str1;
                locationModel.StationCode = locationDetail.Details.StationCode;
                locationModel.CountryCode = locationDetail.Country.ID;
                LocationModel locationModel3 = locationModel;
                if (locationDetail.Country.ID != "US")
                {
                    LocationModel locationModel4 = locationModel3;
                    if (!String.IsNullOrEmpty(locationDetail.LocalizedName))
                    {
                        localizedName = locationDetail.LocalizedName;
                    }
                    else
                    {
                        String[] englishName = new String[] { locationDetail.EnglishName, ", ", locationDetail.Country.LocalizedName, " (", locationDetail.AdministrativeArea.LocalizedName, ")" };
                        localizedName = String.Concat(englishName);
                    }
                    locationModel4.FullName = localizedName;
                }
                else
                {
                    LocationModel locationModel5 = locationModel3;
                    str2 = (!String.IsNullOrEmpty(locationDetail.LocalizedName) ? locationDetail.LocalizedName : String.Concat(locationDetail.EnglishName, ", ", locationDetail.AdministrativeArea.ID));
                    locationModel5.FullName = str2;
                }
                observableCollection1.Add(locationModel3);
                observableCollection = observableCollection1;
            }
            else
            {
                observableCollection = observableCollection1;
            }
            return observableCollection;
        }

        public async Task<ObservableCollection<LocationModel>> GetLocationsByText(String query)
        {
            List<LocationDetail> locationDetails = await _locationService.FindLocationsDetailedByText(query);
            return this.MapCityDataDetailed(locationDetails);
        }

        private ObservableCollection<LocationModel> MapCityData(IEnumerable<LocationBase> cities)
        {
            ObservableCollection<LocationModel> observableCollection = new ObservableCollection<LocationModel>();
            if (cities != null)
            {
                foreach (LocationBase city in cities)
                {
                    LocationModel locationModel = new LocationModel()
                    {
                        DisplayName = city.LocalizedName,
                        LocationId = city.Key,
                        Latitude = "",
                        Longitude = "",
                        GmtOffSet = 0,
                        DmaId = "",
                        VideoCode = ""
                    };
                    LocationModel locationModel1 = locationModel;
                    if (city.Country.ID != "US")
                    {
                        String[] localizedName = new String[] { city.LocalizedName, ", ", city.Country.LocalizedName, " (", city.AdministrativeArea.LocalizedName, ")" };
                        locationModel1.FullName = String.Concat(localizedName);
                    }
                    else
                    {
                        locationModel1.FullName = String.Concat(city.LocalizedName, ", ", city.AdministrativeArea.ID);
                    }
                    observableCollection.Add(locationModel1);
                }
            }
            return observableCollection;
        }

        private ObservableCollection<LocationModel> MapCityDataDetailed(IEnumerable<LocationDetail> cities)
        {
            ObservableCollection<LocationModel> observableCollection = new ObservableCollection<LocationModel>();
            if (cities != null)
            {
                foreach (LocationDetail city in cities)
                {
                    String d = "";
                    if (city.Details.DMA != null)
                    {
                        d = city.Details.DMA.ID;
                    }
                    LocationModel locationModel = new LocationModel()
                    {
                        DisplayName = city.LocalizedName,
                        LocationId = city.Key,
                        Latitude = city.GeoPosition.Latitude.ToString(),
                        Longitude = city.GeoPosition.Longitude.ToString(),
                        GmtOffSet = (Double)city.TimeZone.GmtOffset,
                        DmaId = d,
                        VideoCode = city.Details.VideoCode,
                        MapCode = (city.Country.ID.ToLower().Equals("us") ? city.Details.NXState : city.Details.Satellite),
                        StationCode = city.Details.StationCode,
                        CountryCode = city.Country.ID
                    };
                    LocationModel locationModel1 = locationModel;
                    if (city.Country.ID != "US")
                    {
                        String[] localizedName = new String[] { city.LocalizedName, ", ", city.Country.LocalizedName, " (", city.AdministrativeArea.LocalizedName, ")" };
                        locationModel1.FullName = String.Concat(localizedName);
                    }
                    else
                    {
                        locationModel1.FullName = String.Concat(city.LocalizedName, ", ", city.AdministrativeArea.ID);
                    }
                    observableCollection.Add(locationModel1);
                }
            }
            return observableCollection;
        }
    }
}