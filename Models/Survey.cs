using System;

namespace SpaceTradersWPF.Models;

public class Survey
{
    public string signature { get; set; }
    public string symbol { get; set; }
    public SurveyDeposit[] deposits { get; set; }
    public DateTime expiration { get; set; }
    public string size { get; set; }
}