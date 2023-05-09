using System.Collections.Generic;

namespace SpaceTradersWPF.Services;

public class DbCustomerStore : ICustomerStore
{
    public List<string> GetAll()
    {
        return new List<string>()
            {
                "cust 1",
                "cust 2",
                "cust 3",
            };
    }
}