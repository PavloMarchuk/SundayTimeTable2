using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class TeacherInSubjectRepository : GenericRepository<TeacherInSubject>
	{
		public TeacherInSubjectRepository(DbContext context) : base(context) { }
	}
}