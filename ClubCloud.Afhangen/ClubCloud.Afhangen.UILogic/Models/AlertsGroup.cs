using System;
using System.Collections.ObjectModel;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class AlertsGroup
	{
		public string Title
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public string ThumbnailUrl
		{
			get;
			set;
		}
		public ObservableCollection<AlertModel> AlertsGroupItems
		{
			get;
			set;
		}
		public AlertsGroup()
		{
			this.AlertsGroupItems = new ObservableCollection<AlertModel>();
		}
	}
}
