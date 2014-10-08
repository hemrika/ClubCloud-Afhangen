using Newtonsoft.Json;
using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class AlertLastAction
	{
		[JsonProperty("Localized", NullValueHandling = NullValueHandling.Ignore)]
		public string Localized
		{
			get;
			set;
		}
		[JsonProperty("English", NullValueHandling = NullValueHandling.Ignore)]
		public string English
		{
			get;
			set;
		}
	}
}
