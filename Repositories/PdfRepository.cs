using Microsoft.EntityFrameworkCore;
using UserHandling.Data;
using UserHandling.Models;

namespace UserHandling.Repositories
{
    public class PdfRepository : IPdfRepository
    {
        private readonly AppDbContext _context;

        public PdfRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pdf>> GetAllPdfsAsync()
        {
            return await _context.Pdfs
                .Include(p => p.UserPdfs)
                    .ThenInclude(up => up.User)
                .ToListAsync();
        }
    }
}
