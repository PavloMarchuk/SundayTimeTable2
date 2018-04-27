using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class StudentRepository : GenericRepository<Student>
	{
		public StudentRepository(DbContext context) : base(context) { }
	}
}