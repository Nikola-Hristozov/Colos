using Color.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Color.Services.CouponAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Coupon>().HasData(new List<Coupon>()
        {
            new()
            {
                CouponCode = "1234",
                CouponId = 1,
                DiscountAmount = 12
            },
            new()
            {
                CouponCode = "12344",
                CouponId = 2,
                DiscountAmount = 50,
                MinAmount = 100
            },
        });
    }
}