using KennelPro.Models.Authentication;
using KennelPro.Models.Documents;
using KennelPro.Models.Dogs;
using KennelPro.Models.Kennels;
using KennelPro.Models.Litters;
using KennelPro.Models.Medical;
using KennelPro.Models.Notifications;
using KennelPro.Models.Reproduction;

using Microsoft.EntityFrameworkCore;

namespace KennelPro.Data.Database;


public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    // Authentication
    public DbSet<User> Users => Set<User>();

    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();


    // Kennel
    public DbSet<Kennel> Kennels => Set<Kennel>();


    // Dogs
    public DbSet<Dog> Dogs => Set<Dog>();

    public DbSet<Breed> Breeds => Set<Breed>();


    // Medical
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();

    public DbSet<Vaccination> Vaccinations => Set<Vaccination>();

    public DbSet<ParasiteTreatment> ParasiteTreatments => Set<ParasiteTreatment>();

    public DbSet<Medication> Medications => Set<Medication>();

    public DbSet<Disease> Diseases => Set<Disease>();


    // Reproduction
    public DbSet<HeatCycle> HeatCycles => Set<HeatCycle>();

    public DbSet<Mating> Matings => Set<Mating>();


    // Litters
    public DbSet<Litter> Litters => Set<Litter>();

    public DbSet<Puppy> Puppies => Set<Puppy>();


    // Documents
    public DbSet<Document> Documents => Set<Document>();

    public DbSet<Title> Titles => Set<Title>();


    // Notifications
    public DbSet<Notification> Notifications => Set<Notification>();



    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Dog -> Kennel
        modelBuilder.Entity<Dog>()
            .HasOne(d => d.Kennel)
            .WithMany(k => k.Dogs)
            .HasForeignKey(d => d.KennelId)
            .OnDelete(DeleteBehavior.Cascade);



        // Dog -> Breed
        modelBuilder.Entity<Dog>()
            .HasOne(d => d.Breed)
            .WithMany(b => b.Dogs)
            .HasForeignKey(d => d.BreedId)
            .OnDelete(DeleteBehavior.Restrict);



        // User -> Kennel
        modelBuilder.Entity<User>()
            .HasOne(u => u.Kennel)
            .WithMany(k => k.Users)
            .HasForeignKey(u => u.KennelId)
            .OnDelete(DeleteBehavior.Cascade);



        // Litter Mother
        modelBuilder.Entity<Litter>()
            .HasOne(l => l.MotherDog)
            .WithMany(d => d.LittersAsMother)
            .HasForeignKey(l => l.MotherDogId)
            .OnDelete(DeleteBehavior.Restrict);



        // Litter Father
        modelBuilder.Entity<Litter>()
            .HasOne(l => l.FatherDog)
            .WithMany(d => d.LittersAsFather)
            .HasForeignKey(l => l.FatherDogId)
            .OnDelete(DeleteBehavior.Restrict);



        // Puppy -> Litter
        modelBuilder.Entity<Puppy>()
            .HasOne(p => p.Litter)
            .WithMany(l => l.Puppies)
            .HasForeignKey(p => p.LitterId)
            .OnDelete(DeleteBehavior.Cascade);



        // MedicalRecord -> Dog
        modelBuilder.Entity<MedicalRecord>()
            .HasOne(m => m.Dog)
            .WithMany(d => d.MedicalRecords)
            .HasForeignKey(m => m.DogId)
            .OnDelete(DeleteBehavior.Cascade);



        // Vaccination -> Dog
        modelBuilder.Entity<Vaccination>()
            .HasOne(v => v.Dog)
            .WithMany()
            .HasForeignKey(v => v.DogId)
            .OnDelete(DeleteBehavior.Cascade);



        // Documents -> Dog
        modelBuilder.Entity<Document>()
            .HasOne(d => d.Dog)
            .WithMany(d => d.Documents)
            .HasForeignKey(d => d.DogId)
            .OnDelete(DeleteBehavior.Cascade);



        // Notification -> User
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);



        // Mating Female
        modelBuilder.Entity<Mating>()
            .HasOne(m => m.FemaleDog)
            .WithMany()
            .HasForeignKey(m => m.FemaleDogId)
            .OnDelete(DeleteBehavior.Restrict);



        // Mating Male
        modelBuilder.Entity<Mating>()
            .HasOne(m => m.MaleDog)
            .WithMany()
            .HasForeignKey(m => m.MaleDogId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}