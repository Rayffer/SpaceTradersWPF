﻿using System.Collections.Generic;

namespace SpaceTradersWPF.Repositories;

internal interface IInformationRepository<TypeToStore> where TypeToStore : class
{
    IList<TypeToStore> Store { get; set; }

    void SaveInformation(params TypeToStore[] elements);
}
