using DataAccessGeneral.Interfaces;

namespace DataAccessNoSql
{

	public class NoSqlReposytory<T> : INoSqlReposytory<T> where T : IId
    {

    }

	public interface INoSqlReposytory<T> where T : IId
	{
	}
}