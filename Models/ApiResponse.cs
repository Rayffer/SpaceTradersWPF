namespace SpaceTradersWPF.Models;

internal class ApiResponse<ResponseType>
{
    public ResponseType data { get; set; }
}