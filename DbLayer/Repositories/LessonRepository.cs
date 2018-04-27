using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class LessonRepository : GenericRepository<Lesson>
	{
		public LessonRepository(DbContext context) : base(context) { }
	}
}
