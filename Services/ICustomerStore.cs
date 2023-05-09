using System.Collections.Generic;

namespace SpaceTradersWPF.Services;

public interface ICustomerStore
{
    List<string> GetAll();
}