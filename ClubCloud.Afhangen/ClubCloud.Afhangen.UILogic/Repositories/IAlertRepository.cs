using ClubCloud.Afhangen.UILogic.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace ClubCloud.Afhangen.UILogic.Repositories
{
	public interface IAlertRepository
	{
		Task<ObservableCollection<AlertModel>> GetLocationAlerts(string locationKey);
	}
}
