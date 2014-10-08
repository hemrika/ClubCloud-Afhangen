using System.Collections.Generic;
namespace ClubCloud.Afhangen.UILogic.Models
{
	public class Observation
	{
		public StormBase Storm
		{
			get;
			set;
		}
		public Advisory Advisory
		{
			get;
			set;
		}
		public Position Position
		{
			get;
			set;
		}
		public Measurements MaxWindGust
		{
			get;
			set;
		}
		public Measurements SustainedWind
		{
			get;
			set;
		}
		public Measurements MinimumPressure
		{
			get;
			set;
		}
		public Wind Movement
		{
			get;
			set;
		}
		public string Status
		{
			get;
			set;
		}
		public List<Reference> LandmarkReferences
		{
			get;
			set;
		}
	}
}
