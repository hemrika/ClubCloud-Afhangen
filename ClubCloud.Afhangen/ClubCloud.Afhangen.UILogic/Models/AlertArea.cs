using Newtonsoft.Json;
using System;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class AlertArea
	{
		public string Name
		{
			get;
			set;
		}
		public DateTime StartTime
		{
			get;
			set;
		}
		public DateTime EndTime
		{
			get;
			set;
		}
		public AlertLastAction LastAction
		{
			get;
			set;
		}
		[JsonProperty("Text", NullValueHandling = NullValueHandling.Ignore)]
		public string Text
		{
			get;
			set;
		}
	}
}
