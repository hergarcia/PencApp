using System.Diagnostics;
using PencApp.Services.ApiClient;

namespace PencApp.Services.Pencas;

public class PencaService(IApiClient apiClient) : IPencaService
{
    public async Task<List<MatchDTO>> GetPencaMatches(long pencaId)
    {
        // var pencas = await apiClient.GetAllPencasAsync();
        var matches = new List<MatchDTO>();
        // var tasks = new List<Task>();
        // try
        // {
        //     var match1 = await apiClient.GetMatchAsync(6051);
        //     var match2 = await apiClient.GetMatchAsync(6052);
        //     var match3 = await apiClient.GetMatchAsync(6053);
        //     var match4 = await apiClient.GetMatchAsync(6054);
        // }
        // catch (Exception e)
        // {
        //     Debug.WriteLine(e);
        //     throw;
        // }
        
        // tasks.Add(Task.Run(async () => matches.Add(await apiClient.GetMatchAsync(6051))));
        // tasks.Add(Task.Run(async () => matches.Add(await apiClient.GetMatchAsync(6052))));
        // tasks.Add(Task.Run(async () => matches.Add(await apiClient.GetMatchAsync(6053))));
        // tasks.Add(Task.Run(async () => matches.Add(await apiClient.GetMatchAsync(6054))));
        
        // await Task.WhenAll(tasks);
        return matches;
    }
}