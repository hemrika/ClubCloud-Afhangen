namespace ClubCloud.Afhangen.UILogic.Models
{
	public class StormPosition
	{
		public StormBase Storm
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
		public string Status
		{
			get;
			set;
		}
	}
}
