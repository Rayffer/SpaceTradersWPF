using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.Models;
using SpaceTradersWPF.Repositories;

namespace SpaceTradersWPF.Services;

internal class ShipAutomationService : IShipAutomationService
{
    private readonly IInformationRepository<ShipAutomation> informationRepository;

    public ShipAutomationService(IInformationRepository<ShipAutomation> informationRepository)
    {
        this.informationRepository = informationRepository;
    }

    public Survey GetShipAutomation(string shipSymbol)
    {
        throw new NotImplementedException();
    }

    public void RemoveShipAutomation(ShipAutomation shipAutomation)
    {
        throw new NotImplementedException();
    }

    public void SaveShipAutomation(params ShipAutomation[] shipAutomations)
    {
        throw new NotImplementedException();
    }
}
