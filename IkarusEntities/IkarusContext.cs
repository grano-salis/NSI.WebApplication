using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IkarusEntities
{
    public partial class IkarusContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<AddressType> AddressType { get; set; }
        public virtual DbSet<CaseCategory> CaseCategory { get; set; }
        public virtual DbSet<CaseContact> CaseContact { get; set; }
        public virtual DbSet<CaseInfo> CaseInfo { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<ClientContact> ClientContact { get; set; }
        public virtual DbSet<ClientType> ClientType { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentCategory> DocumentCategory { get; set; }
        public virtual DbSet<FileType> FileType { get; set; }
        public virtual DbSet<Hearing> Hearing { get; set; }
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<PaymentGateway> PaymentGateway { get; set; }
        public virtual DbSet<PricingPackage> PricingPackage { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<UserCase> UserCase { get; set; }
        public virtual DbSet<UserHearing> UserHearing { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserMeeting> UserMeeting { get; set; }
        public virtual DbSet<Phone> Phone { get; set; }
        public virtual DbSet<Email> Email { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql(@"Host=46.101.178.124;Database=ikarus;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasIndex(e => e.AddressTypeId)
                    .HasName("IX_Relationship19");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship68");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.AddressTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship19");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Address)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship68");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship77");

                entity.Property(e => e.AddressTypeName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.ModifiedDate).HasColumnType("timestamptz");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.AddressType)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Relationship77");
            });

            modelBuilder.Entity<CaseCategory>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship57");

                entity.Property(e => e.CaseCategoryName).IsRequired();

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CaseCategory)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship57");
            });

            modelBuilder.Entity<CaseContact>(entity =>
            {
                entity.HasKey(e => new { e.CaseContactId, e.CaseId, e.Contact });

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.CaseContact)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship30");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.CaseContact)
                    .HasForeignKey(d => d.Contact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship31");
            });

            modelBuilder.Entity<CaseInfo>(entity =>
            {
                entity.HasKey(e => e.CaseId);

                entity.HasIndex(e => e.CaseCategory)
                    .HasName("IX_Relationship4");

                entity.HasIndex(e => e.CaseNumber)
                    .HasName("IX_CaseNumber");

                entity.HasIndex(e => e.ClientId)
                    .HasName("IX_Relationship61");

                entity.HasIndex(e => e.CourtNumber)
                    .HasName("IX_CourtNumber");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship66");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship36");

                entity.Property(e => e.CaseNumber).IsRequired();

                entity.Property(e => e.CourtNumber).IsRequired();

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.Judge).HasColumnType("char(255)");

                entity.HasOne(d => d.CaseCategoryNavigation)
                    .WithMany(p => p.CaseInfo)
                    .HasForeignKey(d => d.CaseCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship4");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.CaseInfo)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship61");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.CaseInfo)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship66");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CaseInfo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship36");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("IX_Relationship73");

                entity.HasIndex(e => e.ClientTypeId)
                    .HasName("IX_Relationship2");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship72");

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship60");

                entity.Property(e => e.ClientName).IsRequired();

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("Relationship73");

                entity.HasOne(d => d.ClientType)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.ClientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship2");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship72");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship60");
            });

            modelBuilder.Entity<ClientContact>(entity =>
            {
                entity.HasIndex(e => e.ClientId)
                    .HasName("IX_Relationship63");

                entity.HasIndex(e => e.Contact)
                    .HasName("IX_Relationship64");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientContact)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship63");

                entity.HasOne(d => d.ContactNavigation)
                    .WithMany(p => p.ClientContact)
                    .HasForeignKey(d => d.Contact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship64");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship59");

                entity.Property(e => e.ClientTypeName).IsRequired();

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.ClientType)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("Relationship59");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(e => e.Contact1);

                entity.HasIndex(e => e.AddressId)
                    .HasName("IX_Relationship53");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship71");

                entity.Property(e => e.Contact1).HasColumnName("Contact");

                entity.Property(e => e.CreatedDate).HasColumnType("timestamptz");

                entity.Property(e => e.FirsttName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.ModifiedDate).HasColumnType("timestamptz");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("Relationship53");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship71");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Relationship75");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Conversation)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Relationship75");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.AddressId)
                    .HasName("IX_Relationship56");

                entity.HasIndex(e => e.PricingPackageId)
                    .HasName("IX_Relationship50");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsActive).HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.AddressId)
                    .HasConstraintName("Relationship56");

                entity.HasOne(d => d.PricingPackage)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.PricingPackageId)
                    .HasConstraintName("Relationship50");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.HasIndex(e => e.CaseId)
                    .HasName("IX_Relationship8");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship69");

                entity.HasIndex(e => e.DocumentCategoryId)
                    .HasName("IX_Relationship37");

                entity.HasIndex(e => e.FileTypeId)
                    .HasName("IX_Relationship38");

                entity.Property(e => e.DocumentId).HasDefaultValueSql("nextval('\"Customer_CustomerId_seq\"'::regclass)");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship8");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("Relationship69");

                entity.HasOne(d => d.DocumentCategory)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.DocumentCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship37");

                entity.HasOne(d => d.FileType)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.FileTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship38");
            });

            modelBuilder.Entity<DocumentCategory>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship78");

                entity.Property(e => e.CategoryTitle).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.DocumentCategory)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship78");
            });

            modelBuilder.Entity<FileType>(entity =>
            {
                entity.Property(e => e.Extension).IsRequired();

                entity.Property(e => e.IconPath).IsRequired();
            });

            modelBuilder.Entity<Hearing>(entity =>
            {
                entity.HasIndex(e => e.CaseId)
                    .HasName("IX_Relationship17");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship67");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.HearingDate).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.Hearing)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship17");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Hearing)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship67");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship70");

                entity.Property(e => e.DateCreated).HasColumnType("timetz");

                entity.Property(e => e.DateModified).HasColumnType("timetz");

                entity.Property(e => e.From).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.To).HasColumnType("timestamptz");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Meeting)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship70");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasIndex(e => e.ConversationId)
                    .HasName("IX_Relationship39");

                entity.HasIndex(e => e.CreatedByUserId)
                    .HasName("IX_Relationship74");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.Message1)
                    .IsRequired()
                    .HasColumnName("Message");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("Relationship39");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Message)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .HasConstraintName("Relationship74");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.NoteId).HasDefaultValueSql("nextval('\"Note_NoteId_seq\"'::regclass)");

                entity.Property(e => e.Text).IsRequired();

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NoteUserFK");

                entity.HasOne(d => d.Hearing)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.HearingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("NoteHearingFK");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasIndex(e => e.ConversationId)
                    .HasName("IX_Relationship76");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Relationship52");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.IsSnoozed).HasDefaultValueSql("false");

                entity.Property(e => e.LastSeenTime).HasColumnType("timestamptz");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("Relationship76");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Participant)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Relationship52");
            });

            modelBuilder.Entity<PaymentGateway>(entity =>
            {
                entity.Property(e => e.GatewayName).IsRequired();

                entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            });

            modelBuilder.Entity<PricingPackage>(entity =>
            {
                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.IsActive).HasDefaultValueSql("true");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.Price).HasColumnType("numeric(10, 2)");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Relationship22");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.DueDate).HasColumnType("timestamptz");

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.Title).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AssignedTo");

                entity.HasQueryFilter(x => EF.Property<bool>(x, "IsDeleted") == false);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship48");

                entity.HasIndex(e => e.PaymentGatewayId)
                    .HasName("IX_Relationship41");

                entity.HasIndex(e => e.PricingPackageId)
                    .HasName("IX_Relationship44");

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship48");

                entity.HasOne(d => d.PaymentGateway)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.PaymentGatewayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship41");

                entity.HasOne(d => d.PricingPackage)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.PricingPackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship44");
            });

            modelBuilder.Entity<UserCase>(entity =>
            {
                entity.HasKey(e => new { e.UserCaseId, e.CaseId, e.UserId });

                entity.Property(e => e.UserCaseId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Case)
                    .WithMany(p => p.UserCase)
                    .HasForeignKey(d => d.CaseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AssignedTo");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserCase)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("HasCases");
            });

            modelBuilder.Entity<UserHearing>(entity =>
            {
                entity.HasKey(e => new { e.UserHearingId, e.HearingId, e.UserId });

                entity.Property(e => e.UserHearingId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Hearing)
                    .WithMany(p => p.UserHearing)
                    .HasForeignKey(d => d.HearingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserHearingHearingFK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserHearing)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserHearingUserFK");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.HasIndex(e => e.CustomerId)
                    .HasName("IX_Relationship3");

                entity.HasIndex(e => e.Username)
                    .HasName("Username")
                    .IsUnique();

                entity.Property(e => e.DateCreated).HasColumnType("timestamptz");

                entity.Property(e => e.DateModified).HasColumnType("timestamptz");

                entity.Property(e => e.Email).IsRequired();

                entity.Property(e => e.FirstName).IsRequired();

                entity.Property(e => e.IsDeleted).HasDefaultValueSql("false");

                entity.Property(e => e.LastName).IsRequired();

                entity.Property(e => e.Username).IsRequired();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.UserInfo)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship3");
            });

            modelBuilder.Entity<UserMeeting>(entity =>
            {
                entity.HasKey(e => new { e.UserMeetingId, e.MeetingId, e.UserId });

                entity.Property(e => e.UserMeetingId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Meeting)
                    .WithMany(p => p.UserMeeting)
                    .HasForeignKey(d => d.MeetingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship28");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserMeeting)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Relationship29");
            });
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries<Task>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        entry.CurrentValues["DateCreated"] = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                    case EntityState.Modified:
                        entry.CurrentValues["DateModified"] = DateTime.Now;
                        break;
                }
            }
        }

    }
}
