namespace ONGES.Contracts.DTOs;

public class DonationMessage
{
    public Guid CampaignId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DonatedAt { get; set; }
}
