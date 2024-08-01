namespace VethubLanding.Models
{
    public class SubscriptionResponse
    {
        public string Name { get; set; }
        public int TotalMonth { get; set; }
        public int PatientQuota { get; set; }
        public double Price { get; set; }
        public double InitialDiscount { get; set; }
        public double DiscountedPrice { get; set; }
        public bool IsClinic { get; set; }
    }
}
