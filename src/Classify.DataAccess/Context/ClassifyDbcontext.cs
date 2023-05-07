using Classify.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Classify.DataAccess.Context;

public class ClassifyDbcontext : DbContext
{
    public ClassifyDbcontext(DbContextOptions<ClassifyDbcontext> options) 
        : base(options){ }

    public DbSet<Student> Students { get; set; } 
}
