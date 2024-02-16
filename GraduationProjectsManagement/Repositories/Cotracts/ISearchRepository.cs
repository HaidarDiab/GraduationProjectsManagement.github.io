

namespace GraduationProjectsManagement.Repositories.Cotracts
{
    public interface ISearchRepository
    {
        Task<List<Result>> SearchAsync(string term);
    }


}
