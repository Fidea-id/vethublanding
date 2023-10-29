namespace VethubLanding.Models
{
    public class BookDemoRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public string PhoneNumber { get; set; }
        public string ClinicName { get; set; }
        public string ClinicType { get; set; }
        public string ClinicAddress { get; set; }
        public string ClinicCity { get; set; }
        public bool IsNeedSubscribe { get; set; }
    }
}