using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class TeacherRepository : GenericRepository<Teacher>
	{
		public TeacherRepository(DbContext context) : base(context) { }
	}
}