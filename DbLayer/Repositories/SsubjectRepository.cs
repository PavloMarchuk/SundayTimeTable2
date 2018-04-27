using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class SsubjectRepository : GenericRepository<Ssubject>
	{
		public SsubjectRepository(DbContext context) : base(context) { }
	}
}