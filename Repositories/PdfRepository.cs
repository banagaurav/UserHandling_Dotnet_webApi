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

        public async Task SavePdfAsync(Pdf pdf, int userId)
        {
            _context.Pdfs.Add(pdf);
            await _context.SaveChangesAsync();

            var userPdf = new UserPdf
            {
                UserId = userId,
                PdfId = pdf.PdfId
            };

            _context.UserPdfs.Add(userPdf);
            await _context.SaveChangesAsync();
        }
    }
}
