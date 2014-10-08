using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class Alert
	{
		[JsonProperty("AlertID")]
		public int AlertId
		{
			get;
			set;
		}
		public List<AlertArea> Area
		{
			get;
			set;
		}
		public string CountryCode
		{
			get;
			set;
		}
		public AlertDescription Description
		{
			get;
			set;
		}
		public string Link
		{
			get;
			set;
		}
		public string MobileLink
		{
			get;
			set;
		}
		public int Priority
		{
			get;
			set;
		}
		public string Source
		{
			get;
			set;
		}
		public string Type
		{
			get;
			set;
		}
	}
}
