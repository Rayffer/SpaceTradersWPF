﻿using System.Threading.Tasks;

using SpaceTradersWPF.ApiModels;
using SpaceTradersWPF.ApiModels.Requests;
using SpaceTradersWPF.ApiModels.Responses;
using SpaceTradersWPF.Types;

namespace SpaceTradersWPF.Services;

internal partial interface ISpaceTradersApi
{
    void ClearAccessToken();

    void SetAccessTokenHeader(string token);
}
