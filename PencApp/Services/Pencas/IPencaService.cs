using PencApp.Services.ApiClient;

namespace PencApp.Services.Pencas;

public interface IPencaService
{
    Task<List<MatchDTO>> GetPencaMatches(long pencaId);
}