using Microsoft.EntityFrameworkCore;
using BankingWebAPI.Repository.Entities;

public class BankDbContext : DbContext
{
    public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

    // DbSets
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Disposition> Dispositions { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<AccountType> AccountTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ---- Accounts
        modelBuilder.Entity<Account>(e =>
        {
            e.ToTable("Accounts", "dbo");

            // Map FK property name to actual DB column name
            e.Property(a => a.AccountTypeId).HasColumnName("AccountTypesId");

            // FK: Accounts.AccountTypesId -> AccountTypes.AccountTypeId
            e.HasOne(a => a.AccountType)
             .WithMany(t => t.Accounts)
             .HasForeignKey(a => a.AccountTypeId)
             .HasConstraintName("FK_Accounts_AccountTypes");

            e.Property(a => a.Frequency).IsRequired();
            e.Property(a => a.Created).IsRequired();
            e.Property(a => a.Balance).IsRequired();
        });

        // ---- AccountTypes
        modelBuilder.Entity<AccountType>(e =>
        {
            e.ToTable("AccountTypes", "dbo");
            e.HasKey(x => x.AccountTypeId);
        });

        // ---- Customers
        modelBuilder.Entity<Customer>(e =>
        {
            e.ToTable("Customers", "dbo");
            e.HasKey(x => x.CustomerId);

            e.Property(x => x.Gender).IsRequired();
            e.Property(x => x.Givenname).IsRequired();
            e.Property(x => x.Surname).IsRequired();
            e.Property(x => x.Streetaddress).IsRequired();
            e.Property(x => x.City).IsRequired();
            e.Property(x => x.Zipcode).IsRequired();
            e.Property(x => x.Country).IsRequired();
            e.Property(x => x.CountryCode).IsRequired();

            // DB allows NULL → keep optional in entity or assign defaults in controller
            // e.Property(x => x.Birthday).IsRequired(false);
            // e.Property(x => x.Telephonecountrycode).IsRequired(false);
            // e.Property(x => x.Telephonenumber).IsRequired(false);
            // e.Property(x => x.Emailaddress).IsRequired(false);
        });

        // ---- Dispositions (link Customer <-> Account)
        modelBuilder.Entity<Disposition>(e =>
        {
            e.ToTable("Dispositions", "dbo");
            e.HasKey(x => x.DispositionId);

            e.HasOne(d => d.Customer)
             .WithMany()
             .HasForeignKey(d => d.CustomerId)
             .HasConstraintName("FK_Dispositions_Customers");

            e.HasOne(d => d.Account)
             .WithMany()
             .HasForeignKey(d => d.AccountId)
             .HasConstraintName("FK_Dispositions_Accounts");

            e.Property(d => d.Type).IsRequired(); // OWNER / DISPONENT (DB check constraint)
        });

        // ---- Transactions
        modelBuilder.Entity<Transaction>(e =>
        {
            e.ToTable("Transactions", "dbo");
            e.HasKey(x => x.TransactionId);

            e.HasOne(t => t.Account)
             .WithMany(a => a.Transactions)
             .HasForeignKey(t => t.AccountId)
             .HasConstraintName("FK_Transactions_Accounts");

            e.Property(t => t.Date).IsRequired();
            e.Property(t => t.Type).IsRequired();      // "Credit" | "Debit" (DB check constraint)
            e.Property(t => t.Operation).IsRequired(); // e.g., "Transfer", "Deposit", "Loan"
            e.Property(t => t.Amount).IsRequired();
            e.Property(t => t.Balance).IsRequired();
        });

        // ---- Loans
        modelBuilder.Entity<Loan>(e =>
        {
            e.ToTable("Loans", "dbo");
            e.HasKey(x => x.LoanId);

            e.HasOne<Account>()
             .WithMany()
             .HasForeignKey(l => l.AccountId)
             .HasConstraintName("FK_Loans_Accounts");

            e.Property(l => l.Date).IsRequired();
            e.Property(l => l.Amount).IsRequired();
            e.Property(l => l.Duration).IsRequired();
            e.Property(l => l.Payments).IsRequired();
            e.Property(l => l.Status).IsRequired();
        });

        // ---- Cards
        modelBuilder.Entity<Card>(e =>
        {
            e.ToTable("Cards", "dbo");
            e.HasKey(x => x.CardId);

            e.HasOne(c => c.Disposition)
             .WithMany(d => d.Cards)
             .HasForeignKey(c => c.DispositionId)
             .HasConstraintName("FK_Cards_Dispositions");

            e.Property(c => c.Type).IsRequired();
            e.Property(c => c.Issued).IsRequired();
            e.Property(c => c.CCType).IsRequired();
            e.Property(c => c.CCNumber).IsRequired();
            e.Property(c => c.CVV2).IsRequired();   // present in DB schema
            e.Property(c => c.ExpM).IsRequired();
            e.Property(c => c.ExpY).IsRequired();
        });

        // ---- Users (simple auth table you created)
        modelBuilder.Entity<User>(e =>
        {
            e.ToTable("Users", "dbo");
            e.HasKey(x => x.UserId);

            e.Property(x => x.Username).IsRequired();
            e.Property(x => x.Password).IsRequired();
            e.Property(x => x.Role).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
