using Microsoft.EntityFrameworkCore;
using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public class GetUserRolesRepository : IGetUserRolesRepository
{
    private readonly DataContext context;

    public GetUserRolesRepository(DataContext context)
    {
        this.context = context;
    }
    public async Task<Dictionary<string, List<string>>> GetRolesAndThemes(CancellationToken token)
    {
        var rolesAndThemes = await context.NewsFromSource.AsNoTracking()
            .Select(i => new Tuple<string, string>(i.Role, i.KeyWord).ToValueTuple()).ToListAsync(token);
        var distinctRoles = rolesAndThemes.ToLookup(i => i.Item1, i => i.Item2);
        var groupedList = distinctRoles.SelectMany(group =>
                group.Distinct()
                    .Select(item => new
                    {
                        key = group.Key,
                        element = item,
                    }))
            .GroupBy(pair => pair.key, pair => pair.element)
            .ToDictionary(group => group.Key, group => group.ToList());
        return groupedList;
    }
}