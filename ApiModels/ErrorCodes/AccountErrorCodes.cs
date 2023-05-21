namespace SpaceTradersWPF.ApiModels.ErrorCodes;

internal static class AccountErrorCodes
{
    public const int tokenEmptyError = 4100;
    public const int tokenMissingSubjectError = 4101;
    public const int tokenInvalidSubjectError = 4102;
    public const int missingTokenRequestError = 4103;
    public const int invalidTokenRequestError = 4104;
    public const int invalidTokenSubjectError = 4105;
    public const int accountNotExistsError = 4106;
    public const int agentNotExistsError = 4107;
    public const int accountHasNoAgentError = 4108;
    public const int registerAgentExistsError = 4109;
}
