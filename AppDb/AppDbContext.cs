using FBR_Invoicing_Integration.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FBR_Invoicing_Integration.AppDb
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SellerEntity> Sellers { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<InvoiceItemEntity> InvoiceItems { get; set; }
        public DbSet<BuyerEntity> Buyers { get; set; }
    }
}
