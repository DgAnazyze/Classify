using Microsoft.EntityFrameworkCore;

namespace Classify.DataAccess.Context;

public class ClassifyDbcontext : DbContext
{
    public ClassifyDbcontext(DbContextOptions<ClassifyDbcontext> options) : base(options)
    {
        
    }
}
