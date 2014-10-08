using System;
using System.Collections.ObjectModel;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class AlertModel
	{
		public int AlertId
		{
			get;
			set;
		}
		public string CountryCode
		{
			get;
			set;
		}
		public string Type
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
		public string Title
		{
			get;
			set;
		}
		public string TitleLocal
		{
			get;
			set;
		}
		public ObservableCollection<AlertAreaModel> Areas
		{
			get;
			set;
		}
		public string ThumbnailUrl
		{
			get;
			set;
		}
	}
}
