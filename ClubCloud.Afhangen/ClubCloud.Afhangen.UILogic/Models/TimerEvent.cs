using ClubCloud.Core.Prism.PubSubEvents;
using System;

namespace ClubCloud.Afhangen.UILogic.Models
{
    public class ActivityEvent : PubSubEvent<TimeSpan>
    {
    }

    public class KlokEvent : PubSubEvent<TimeSpan>
    {
    }

    public class SponsorEvent : PubSubEvent<object>
    {
    }
}
