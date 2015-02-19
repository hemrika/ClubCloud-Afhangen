namespace ClubCloud.Afhangen.UILogic.Services
{
    using System;

    public class DialogCommand
    {
        public object Id { get; set; }
        public string Label { get; set; }
        public Action Invoked { get; set; }
    }
}
