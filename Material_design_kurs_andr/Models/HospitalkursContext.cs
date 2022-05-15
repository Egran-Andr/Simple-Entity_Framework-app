using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Material_design_kurs_andr.Models
{
    public partial class HospitalkursContext : DbContext
    {
        public HospitalkursContext()
        {
        }

        public HospitalkursContext(DbContextOptions<HospitalkursContext> options)
            : base(options)
        {
        }
        private static HospitalkursContext _context;
        public static HospitalkursContext GetContext()
        {
            if (_context == null)
                _context = new HospitalkursContext();
            return _context;
            {
            }
        }

        public virtual DbSet<AccountFio> AccountFio { get; set; }
        public virtual DbSet<Drugs> Drugs { get; set; }
        public virtual DbSet<FioId> FioId { get; set; }
        public virtual DbSet<Holidays> Holidays { get; set; }
        public virtual DbSet<HospitalDepatment> HospitalDepatment { get; set; }
        public virtual DbSet<IllProgress> IllProgress { get; set; }
        public virtual DbSet<Ilness> Ilness { get; set; }
        public virtual DbSet<LoginStorage> LoginStorage { get; set; }
        public virtual DbSet<PackageType> PackageType { get; set; }
        public virtual DbSet<PatientInfo> PatientInfo { get; set; }
        public virtual DbSet<PatientProcedure> PatientProcedure { get; set; }
        public virtual DbSet<PatientVisit> PatientVisit { get; set; }
        public virtual DbSet<PatientfioId> PatientfioId { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<ProcedureList> ProcedureList { get; set; }
        public virtual DbSet<Records> Records { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        public virtual DbSet<ServiceSchedule> ServiceSchedule { get; set; }
        public virtual DbSet<VizitIll> VizitIll { get; set; }
        public virtual DbSet<WorkScedule> WorkScedule { get; set; }
        public virtual DbSet<WorkerSpeciality> WorkerSpeciality { get; set; }
        public virtual DbSet<Workers> Workers { get; set; }
        public virtual DbSet<WorkersDepartment> WorkersDepartment { get; set; }
        public virtual DbSet<WorkersScedule> WorkersScedule { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;database=hospital-kurs;user=root;password=NtcnGfhjkm1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountFio>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("account_fio");

                entity.Property(e => e.AccountVerification)
                    .HasColumnName("account_verification")
                    .HasColumnType("enum('+','-')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(137)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Drugs>(entity =>
            {
                entity.HasKey(e => e.IdDrugs)
                    .HasName("PRIMARY");

                entity.ToTable("drugs");

                entity.HasIndex(e => e.DrugPackage)
                    .HasName("Package_idx");

                entity.Property(e => e.IdDrugs).HasColumnName("idDrugs");

                entity.Property(e => e.DrugContradication)
                    .IsRequired()
                    .HasColumnName("drug_contradication")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DrugCost)
                    .HasColumnName("drug_cost")
                    .HasColumnType("decimal(15,2)");

                entity.Property(e => e.DrugIndication)
                    .IsRequired()
                    .HasColumnName("drug_indication")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DrugName)
                    .IsRequired()
                    .HasColumnName("drug_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DrugPackage).HasColumnName("drug_package");

                entity.HasOne(d => d.DrugPackageNavigation)
                    .WithMany(p => p.Drugs)
                    .HasForeignKey(d => d.DrugPackage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Package");
            });

            modelBuilder.Entity<FioId>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("fio_id");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(137)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdWorkers).HasColumnName("idWorkers");
            });

            modelBuilder.Entity<Holidays>(entity =>
            {
                entity.HasKey(e => e.Idholidays)
                    .HasName("PRIMARY");

                entity.ToTable("holidays");

                entity.HasIndex(e => e.Worker)
                    .HasName("worker_holiday_idx");

                entity.Property(e => e.Idholidays).HasColumnName("idholidays");

                entity.Property(e => e.Hday)
                    .HasColumnName("hday")
                    .HasColumnType("date");

                entity.Property(e => e.IsWork).HasColumnName("isWork");

                entity.Property(e => e.Worker).HasColumnName("worker");

                entity.HasOne(d => d.WorkerNavigation)
                    .WithMany(p => p.Holidays)
                    .HasForeignKey(d => d.Worker)
                    .HasConstraintName("worker_holiday");
            });

            modelBuilder.Entity<HospitalDepatment>(entity =>
            {
                entity.HasKey(e => e.Departmentid)
                    .HasName("PRIMARY");

                entity.ToTable("hospital_depatment");

                entity.HasIndex(e => e.DepartmentChief)
                    .HasName("Chief_idx");

                entity.Property(e => e.DepartmentAdress)
                    .HasColumnName("Department_adress")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.DepartmentChief).HasColumnName("Department_chief");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasColumnName("Department_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Description)
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.DepartmentChiefNavigation)
                    .WithMany(p => p.HospitalDepatment)
                    .HasForeignKey(d => d.DepartmentChief)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Chief");
            });

            modelBuilder.Entity<IllProgress>(entity =>
            {
                entity.HasKey(e => new { e.IllId, e.IllDrugId })
                    .HasName("PRIMARY");

                entity.ToTable("ill_progress");

                entity.HasIndex(e => e.IllDrugId)
                    .HasName("Drug_idx");

                entity.HasIndex(e => e.IllId)
                    .HasName("Ilness_idx");

                entity.Property(e => e.IllId).HasColumnName("ill_id");

                entity.Property(e => e.IllDrugId).HasColumnName("ill_drug_id");

                entity.HasOne(d => d.IllDrug)
                    .WithMany(p => p.IllProgress)
                    .HasForeignKey(d => d.IllDrugId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Drug");

                entity.HasOne(d => d.Ill)
                    .WithMany(p => p.IllProgress)
                    .HasForeignKey(d => d.IllId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Ilness_key");
            });

            modelBuilder.Entity<Ilness>(entity =>
            {
                entity.HasKey(e => e.IdIlness)
                    .HasName("PRIMARY");

                entity.ToTable("ilness");

                entity.Property(e => e.IdIlness).HasColumnName("idIlness");

                entity.Property(e => e.IllEffects)
                    .IsRequired()
                    .HasColumnName("Ill_effects")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IllLenght).HasColumnName("Ill_lenght");

                entity.Property(e => e.IllName)
                    .IsRequired()
                    .HasColumnName("Ill_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IllSymptoms)
                    .IsRequired()
                    .HasColumnName("Ill_symptoms")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<LoginStorage>(entity =>
            {
                entity.HasKey(e => e.WorkerId)
                    .HasName("PRIMARY");

                entity.ToTable("login_storage");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.AccountVerification)
                    .HasColumnName("account_verification")
                    .HasColumnType("enum('+','-')")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.Worker)
                    .WithOne(p => p.LoginStorage)
                    .HasForeignKey<LoginStorage>(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Worker_num_login");
            });

            modelBuilder.Entity<PackageType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PRIMARY");

                entity.ToTable("package_type");

                entity.Property(e => e.TypeId).HasColumnName("Type_id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PackageStorage)
                    .IsRequired()
                    .HasColumnName("Package_storage")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PackageType1)
                    .IsRequired()
                    .HasColumnName("package_type")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<PatientInfo>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.OmcNumber })
                    .HasName("PRIMARY");

                entity.ToTable("patient_info");

                entity.Property(e => e.PatientId)
                    .HasColumnName("patient_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.OmcNumber)
                    .HasColumnName("OMC_number")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DmcNumber)
                    .HasColumnName("DMC_number")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DmcOrganisation)
                    .HasColumnName("DMC_organisation")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientAdress)
                    .IsRequired()
                    .HasColumnName("patient_adress")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientAge)
                    .HasColumnName("patient_age")
                    .HasColumnType("date");

                entity.Property(e => e.PatientGender)
                    .IsRequired()
                    .HasColumnName("patient_gender")
                    .HasColumnType("enum('М','Ж','Other')")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientName)
                    .IsRequired()
                    .HasColumnName("patient_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientPhone)
                    .IsRequired()
                    .HasColumnName("patient_phone")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientSecondname)
                    .HasColumnName("patient_secondname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientSurname)
                    .IsRequired()
                    .HasColumnName("patient_surname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.SnilsNumber)
                    .IsRequired()
                    .HasColumnName("SNILS_number")
                    .HasColumnType("varchar(14)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<PatientProcedure>(entity =>
            {
                entity.HasKey(e => new { e.PatientVisit, e.AnalisType, e.AnalisDate })
                    .HasName("PRIMARY");

                entity.ToTable("patient_procedure");

                entity.HasIndex(e => e.AnalisType)
                    .HasName("analis_type_id_idx");

                entity.Property(e => e.PatientVisit).HasColumnName("patient_visit");

                entity.Property(e => e.AnalisType).HasColumnName("analis_type");

                entity.Property(e => e.AnalisDate)
                    .HasColumnName("analis_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.AnakisResult)
                    .HasColumnName("anakis_result")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.AnalisTypeNavigation)
                    .WithMany(p => p.PatientProcedure)
                    .HasForeignKey(d => d.AnalisType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("analis_type_id");

                entity.HasOne(d => d.PatientVisitNavigation)
                    .WithMany(p => p.PatientProcedure)
                    .HasForeignKey(d => d.PatientVisit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Patient_visit_analis");
            });

            modelBuilder.Entity<PatientVisit>(entity =>
            {
                entity.HasKey(e => e.VizitPatId)
                    .HasName("PRIMARY");

                entity.ToTable("patient_visit");

                entity.HasIndex(e => e.DepartmentVisit)
                    .HasName("Department_idx");

                entity.HasIndex(e => e.PatWorkerId)
                    .HasName("Worker_idx");

                entity.HasIndex(e => e.PatientId)
                    .HasName("Patient_idx1");

                entity.HasIndex(e => e.VizitPatId)
                    .HasName("Patient_idx");

                entity.Property(e => e.VizitPatId).HasColumnName("Vizit_pat_id");

                entity.Property(e => e.DepartmentVisit).HasColumnName("Department_visit");

                entity.Property(e => e.PatWorkerId).HasColumnName("pat_worker_id");

                entity.Property(e => e.PatientId).HasColumnName("Patient_id");

                entity.Property(e => e.VizitPatDatebegin)
                    .HasColumnName("Vizit_pat_datebegin")
                    .HasColumnType("date");

                entity.Property(e => e.VizitPatDateend)
                    .HasColumnName("Vizit_pat_dateend")
                    .HasColumnType("date");

                entity.Property(e => e.VizitRezult)
                    .HasColumnName("Vizit_rezult")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.DepartmentVisitNavigation)
                    .WithMany(p => p.PatientVisit)
                    .HasForeignKey(d => d.DepartmentVisit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Department");

                entity.HasOne(d => d.PatWorker)
                    .WithMany(p => p.PatientVisit)
                    .HasForeignKey(d => d.PatWorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Worker");
            });

            modelBuilder.Entity<PatientfioId>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("patientfio_id");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(137)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => e.IdPositions)
                    .HasName("PRIMARY");

                entity.ToTable("positions");

                entity.HasIndex(e => e.PositionName)
                    .HasName("position_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdPositions).HasColumnName("idPositions");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasColumnName("position_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PositionRequirements)
                    .IsRequired()
                    .HasColumnName("position_requirements")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PositionResponsibility)
                    .IsRequired()
                    .HasColumnName("position_responsibility")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PositionSalary)
                    .HasColumnName("position_Salary")
                    .HasColumnType("decimal(15,2)");
            });

            modelBuilder.Entity<ProcedureList>(entity =>
            {
                entity.HasKey(e => e.AnlisId)
                    .HasName("PRIMARY");

                entity.ToTable("procedure_list");

                entity.Property(e => e.AnlisId).HasColumnName("anlis_id");

                entity.Property(e => e.AnalisDiscryption)
                    .IsRequired()
                    .HasColumnName("analis_discryption")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.AnalisName)
                    .IsRequired()
                    .HasColumnName("analis_name")
                    .HasColumnType("varchar(255)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<Records>(entity =>
            {
                entity.HasKey(e => new { e.WorkerRecord, e.RecordDate })
                    .HasName("PRIMARY");

                entity.ToTable("records");

                entity.Property(e => e.WorkerRecord).HasColumnName("worker_record");

                entity.Property(e => e.RecordDate)
                    .HasColumnName("record_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.PatientOmc)
                    .HasColumnName("patient_OMC")
                    .HasColumnType("varchar(16)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.HasOne(d => d.WorkerRecordNavigation)
                    .WithMany(p => p.Records)
                    .HasForeignKey(d => d.WorkerRecord)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("worker_record");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.IdService)
                    .HasName("PRIMARY");

                entity.ToTable("service");

                entity.Property(e => e.IdService).HasColumnName("idService");

                entity.Property(e => e.Descryption)
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.ServiceCost).HasColumnType("decimal(10,2)");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            modelBuilder.Entity<ServiceSchedule>(entity =>
            {
                entity.HasKey(e => new { e.PatientId, e.ServiceDate })
                    .HasName("PRIMARY");

                entity.ToTable("service_schedule");

                entity.HasIndex(e => e.ServiceId)
                    .HasName("Service_key_idx");

                entity.HasIndex(e => e.WorkerId)
                    .HasName("Worker_key_idx");

                entity.Property(e => e.PatientId).HasColumnName("Patient_id");

                entity.Property(e => e.ServiceDate)
                    .HasColumnName("Service_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ServiceId).HasColumnName("Service_id");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceSchedule)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Service_key");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.ServiceSchedule)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Worker_key");
            });

            modelBuilder.Entity<VizitIll>(entity =>
            {
                entity.HasKey(e => new { e.IdVizit, e.IdIll })
                    .HasName("PRIMARY");

                entity.ToTable("vizit_ill");

                entity.HasIndex(e => e.IdIll)
                    .HasName("Illness_key_idx");

                entity.Property(e => e.IdVizit).HasColumnName("idVizit");

                entity.Property(e => e.IdIll).HasColumnName("Id_ill");

                entity.HasOne(d => d.IdIllNavigation)
                    .WithMany(p => p.VizitIll)
                    .HasForeignKey(d => d.IdIll)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Illness_key");

                entity.HasOne(d => d.IdVizitNavigation)
                    .WithMany(p => p.VizitIll)
                    .HasForeignKey(d => d.IdVizit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vizit_key");
            });

            modelBuilder.Entity<WorkScedule>(entity =>
            {
                entity.HasKey(e => new { e.WorkerId, e.Weekday })
                    .HasName("PRIMARY");

                entity.ToTable("work_scedule");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.Weekday)
                    .HasColumnName("weekday")
                    .HasDefaultValueSql("'62'");

                entity.Property(e => e.Endtime)
                    .HasColumnName("endtime")
                    .HasColumnType("time");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("time");

                entity.Property(e => e.WorkFrom)
                    .HasColumnName("workFrom")
                    .HasColumnType("date");

                entity.Property(e => e.WorkTo)
                    .HasColumnName("workTo")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'3000-01-01'");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.WorkScedule)
                    .HasForeignKey(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("worker_scedule");
            });

            modelBuilder.Entity<WorkerSpeciality>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("worker_speciality");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(138)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasColumnName("position_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Workers>(entity =>
            {
                entity.HasKey(e => e.IdWorkers)
                    .HasName("PRIMARY");

                entity.ToTable("workers");

                entity.HasIndex(e => e.WorkerPosition)
                    .HasName("worker_positions_idx");

                entity.Property(e => e.IdWorkers).HasColumnName("idWorkers");

                entity.Property(e => e.WorkExperience).HasColumnName("work_experience");

                entity.Property(e => e.WorkerAdress)
                    .IsRequired()
                    .HasColumnName("worker_adress")
                    .HasColumnType("varchar(100)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerAge)
                    .HasColumnName("worker_age")
                    .HasColumnType("date");

                entity.Property(e => e.WorkerGender)
                    .IsRequired()
                    .HasColumnName("worker_gender")
                    .HasColumnType("enum('М','Ж','Other')")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerName)
                    .IsRequired()
                    .HasColumnName("Worker_name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerPassport)
                    .IsRequired()
                    .HasColumnName("worker_passport")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerPhone)
                    .IsRequired()
                    .HasColumnName("worker_phone")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerPosition).HasColumnName("worker_position");

                entity.Property(e => e.WorkerSecondname)
                    .HasColumnName("Worker_secondname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.WorkerSurname)
                    .IsRequired()
                    .HasColumnName("Worker_surname")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.WorkerPositionNavigation)
                    .WithMany(p => p.Workers)
                    .HasForeignKey(d => d.WorkerPosition)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("worker_positions");
            });

            modelBuilder.Entity<WorkersDepartment>(entity =>
            {
                entity.HasKey(e => e.WorkerId)
                    .HasName("PRIMARY");

                entity.ToTable("workers_department");

                entity.HasIndex(e => e.DepartmentId)
                    .HasName("department_num_idx");

                entity.Property(e => e.WorkerId).HasColumnName("worker_id");

                entity.Property(e => e.DepartmentId).HasColumnName("department_id");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.WorkersDepartment)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("department_num");

                entity.HasOne(d => d.Worker)
                    .WithOne(p => p.WorkersDepartment)
                    .HasForeignKey<WorkersDepartment>(d => d.WorkerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("worker_department");
            });

            modelBuilder.Entity<WorkersScedule>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("workers_scedule");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasColumnType("varchar(57)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.Endtime)
                    .HasColumnName("endtime")
                    .HasColumnType("time");

                entity.Property(e => e.Fio)
                    .HasColumnName("FIO")
                    .HasColumnType("varchar(137)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Starttime)
                    .HasColumnName("starttime")
                    .HasColumnType("time");

                entity.Property(e => e.Weekday)
                    .HasColumnName("weekday")
                    .HasDefaultValueSql("'62'");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
