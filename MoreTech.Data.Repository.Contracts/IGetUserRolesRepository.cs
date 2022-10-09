namespace MoreTech.Data.Repository.Contracts;

/// <summary>
/// Получить список ролей и тем
/// </summary>
public interface IGetUserRolesRepository
{
    /// <summary>
    /// Получить список ролей и соответсвующих роле тем
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<string, List<string>>> GetRolesAndThemes(CancellationToken token);
}