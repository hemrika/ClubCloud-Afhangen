using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Connectivity;

namespace ClubCloud.Afhangen.UILogic.Services
{
    public class LocationServiceProxy : ILocationService
    {
        private Geolocator _geolocator = null;
        private Geoposition _currentPosition = null;
        private CancellationTokenSource _cts = null;
        private readonly string apiKey = "236c108414d44b3fa119057d0e81a5e1";
        private readonly string apiUriRoot = "http://api.accuweather.com/";
        //private readonly string apiVersion = "v1";
        //private readonly string languageCode = "nl-nl";
        private const string LocationServiceEndpoint = "locations";

        public bool GetIsInternetConnected()
        {
            ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
            if (internetConnectionProfile == null)
            {
                return false;
            }

            NetworkConnectivityLevel networkConnectivityLevel = internetConnectionProfile.GetNetworkConnectivityLevel();
            if (networkConnectivityLevel == NetworkConnectivityLevel.InternetAccess)
            {
                return true;
            }

            return networkConnectivityLevel == NetworkConnectivityLevel.ConstrainedInternetAccess;
        }

        public async Task<Geoposition> GetLocationAsync()
        {
            _geolocator = new Geolocator();

            _cts = new CancellationTokenSource();
            CancellationToken token = _cts.Token;

            _currentPosition = await _geolocator.GetGeopositionAsync().AsTask(token);

            //TODO setup Fencing

            return _currentPosition;
        }

        public async Task<List<LocationDetail>> FindLocationsByLatLon(double latitude, double longitude)
        {
            string text = string.Format("{0},{1}", new object[]
			{
                latitude.ToString("0.0000", CultureInfo.InvariantCulture),
				longitude.ToString("0.0000", CultureInfo.InvariantCulture)

				//latitude.ToString("F"),
				//longitude.ToString("F")
			});
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"locations/",
				asp.ApiVersion,
				"/cities/search.json?q=",
				text,
				"&apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
            return await asp.MakeWebRequest<List<LocationDetail>>(requestUri);
        }
        public async Task<List<LocationDetail>> FindLocationsByIpAddress()
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"locations/",
				asp.ApiVersion,
				"/cities/ipaddress.json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
            return await asp.MakeWebRequest<List<LocationDetail>>(requestUri);
        }
        public async Task<List<LocationDetail>> FindLocationsDetailedByText(string query)
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"locations/",
				asp.ApiVersion,
				"/cities/search.json?q=",
				query,
				"&apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
            return await asp.MakeWebRequest<List<LocationDetail>>(requestUri);
        }
        public async Task<List<Location>> FindLocationsByText(string query)
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new InvalidOperationException("The search query cannot be empty");
            }
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"locations/",
				asp.ApiVersion,
				"/cities/search.json?q=",
				query,
				"&apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode)
			}));
            return await asp.MakeWebRequest<List<Location>>(requestUri);
        }
        public async Task<List<LocationBase>> FindLocationsAutoCompleteText(string query)
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            if (string.IsNullOrWhiteSpace(query))
            {
                throw new InvalidOperationException("The search query cannot be empty");
            }
            Uri requestUri = new Uri(asp.ApiUriRoot, string.Concat(new string[]
			{
				"locations/",
				asp.ApiVersion,
				"/cities/autocomplete.json?q=",
				query,
				"&apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode)
			}));
            return await asp.MakeWebRequest<List<LocationBase>>(requestUri);
        }
        public async Task<LocationDetail> FindLocationDetailByKey(string key)
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException("The search key cannot be empty");
            }
            Uri requestUri = new Uri(string.Concat(new object[]
			{
				asp.ApiUriRoot,
				"locations/",
				asp.ApiVersion,
				"/",
				key,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
            return await asp.MakeWebRequest<LocationDetail>(requestUri);

        }
        public async Task<ClubCloud.Afhangen.UILogic.Models.Location> FindLocationByKey(string key)
        {
            ApiServiceProvider asp = new ApiServiceProvider(apiKey, apiUriRoot);
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException("The search key cannot be empty");
            }
            Uri requestUri = new Uri(string.Concat(new object[]
			{
				asp.ApiUriRoot,
				"locations/",
				asp.ApiVersion,
				"/",
				key,
				".json?apikey=",
				asp.ApiKey,
				"&language=",
				asp.ValidateLanguageCode(asp.LanguageCode),
				"&details=true"
			}));
            return await asp.MakeWebRequest<Location>(requestUri);
        }
    }
}
