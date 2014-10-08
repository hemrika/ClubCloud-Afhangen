using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
namespace ClubCloud.Afhangen.UILogic.Services
{
	public class ApiServiceProvider
	{
		internal string ApiKey;
		internal Uri ApiUriRoot;
		internal string ApiVersion;
		internal string LanguageCode;
		protected bool? Metric;

        private string apiKey;
        private string apiUriRoot;

		public bool IsMetric
		{
			get
			{
				return this.Metric.HasValue && this.Metric != false;
			}
			set
			{
				this.Metric = new bool?(value);
			}
		}
		internal ApiServiceProvider(string apiKey, string apiUriRoot, string apiVersion = "v1", string languageCode = "nl-nl", bool? metric = false)
		{
			this.ApiUriRoot = new Uri(apiUriRoot, UriKind.Absolute);
			this.ApiVersion = apiVersion;
			this.ApiKey = apiKey;
			this.LanguageCode = languageCode;
			this.Metric = metric;
		}

		public void SetLanguageCode(string languageCode)
		{
			this.LanguageCode = languageCode;
		}
		public void SetMetricFlag(bool? metric)
		{
			this.Metric = metric;
		}

		public async Task<T> MakeWebRequest<T>(Uri requestUri)
		{
			string value;
			try
			{
				HttpClientHandler httpClientHandler = new HttpClientHandler();
				httpClientHandler.AutomaticDecompression = (DecompressionMethods.GZip | DecompressionMethods.Deflate);
				HttpClient httpClient = new HttpClient(httpClientHandler);
				value = await httpClient.GetStringAsync(requestUri);
			}
			catch (WebException ex)
			{
				string.Format("WebException in MakeWebRequest for {0} [{1}] ", new object[]
				{
					requestUri,
					ex.Message
				});
				throw ex;
			}
			catch (Exception ex2)
			{
				string.Format("Error in MakeWebRequest for {0} [{1}] ", new object[]
				{
					requestUri,
					ex2.Message
				});
				throw ex2;
			}
			T result;
			try
			{
				T t = JsonConvert.DeserializeObject<T>(value);
				result = t;
			}
			catch (Exception innerException)
			{
				string message = string.Format("Error in Deserialization of {0} for {1} [{1}] ", new object[]
				{
					typeof(T).Name,
					requestUri
				});
				throw new InvalidOperationException(message, innerException);
			}
			return result;
		}
		public async Task<T> MakeWebRequest<T>(string requestUri)
		{
			return await this.MakeWebRequest<T>(new Uri(requestUri));
		}
		internal string ValidateMetric(bool? metric)
		{
			if (!metric.HasValue)
			{
				metric = new bool?(this.IsMetric);
			}
			if (!(metric == false))
			{
				return "true";
			}
			return "false";
		}
		internal string ValidateLanguageCode(string languageCode)
		{
			if (string.IsNullOrWhiteSpace(languageCode))
			{
				return CultureInfo.CurrentUICulture.Name;
			}
			return languageCode;
		}
	}
}
