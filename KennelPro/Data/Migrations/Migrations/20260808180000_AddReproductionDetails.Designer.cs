using KennelPro.Data.Database; using Microsoft.EntityFrameworkCore; using Microsoft.EntityFrameworkCore.Infrastructure; using Microsoft.EntityFrameworkCore.Migrations;
#nullable disable
namespace KennelPro.Migrations; [DbContext(typeof(AppDbContext))] [Migration("20260808180000_AddReproductionDetails")] partial class AddReproductionDetails { protected override void BuildTargetModel(ModelBuilder modelBuilder) => modelBuilder.HasAnnotation("ProductVersion","10.0.10"); }
