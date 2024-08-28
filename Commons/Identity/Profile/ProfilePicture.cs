using System;

namespace Commons.Identity.Profile
{
    public class ProfilePicture
    {
        public string Id { get; set; }
        public string LowQualityPath { get; set; }
        public string MidQualityPath { get; set; }
        public string HightQualityPath { get; set; }
        public DateTime Date { get; set; }
    }
}
