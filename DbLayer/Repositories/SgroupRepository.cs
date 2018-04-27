using DbLayer;
using EFCoreGenericRepository.Common;
using Microsoft.EntityFrameworkCore;

namespace EFCoreGenericRepository.Typed
{
	public class SgroupRepository : GenericRepository<Sgroup>
	{
		public SgroupRepository(DbContext context) : base(context) { }
	}
}

