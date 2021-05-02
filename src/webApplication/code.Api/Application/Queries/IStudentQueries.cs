using System.Collections.Generic;
using System.Threading.Tasks;
using code.Api.Application.Queries;

namespace code.Api.Application.Queries
{
    public class SortingParams
    {
        public string SortBy { get; set; } = string.Empty;
        public bool IsAscending { get; set; }

        public SortingParams()
        {
            
        }

        public SortingParams(string sorting,bool isAscending)
        {
            SortBy = sorting;
            IsAscending = isAscending;
        }
    }
    public interface IStudentQueries
    {
        Task<Student> FindAsync(int id);

        Task<IEnumerable<Student>> GetAllAsync(SortingParams param=null);

        Task<IEnumerable<Student>> Sort(SortingParams param);
    }
}