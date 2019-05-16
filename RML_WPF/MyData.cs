using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace RML_WPF
{
    class MyData : DbContext
    {
        // VS-ben, a projekt tulajdonságlapjának Settings fülén felvettem a mydata sort.
        // Ebből a VS generálta a Properties\Settings.Designer.cs forrást és az App.config xml fájlt
        // A Settings osztály felolvassa a configot, már csak használni kell
        public MyData() : base(Properties.Settings.Default.mydata) { }
        // Vedd észre, hogy az EF6-ban nincs OnConfiguring! Itt a konstruktorban kell megoldani a
        // connectionstring átadását.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Hiányzik egy-két dolog a modelBuilder-ben is

            #region Person            
            //modelBuilder.Entity<Person>().Property(p => p.Id)
            //    .ValueGeneratedOnAdd();
            modelBuilder.Entity<Person>().Property(p => p.Titles)
                .HasMaxLength(10)
                .IsUnicode();
            modelBuilder.Entity<Person>().Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();
            modelBuilder.Entity<Person>().Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(30)
                .IsUnicode();
            modelBuilder.Entity<Person>().Property(p => p.Phone)
                .HasMaxLength(30);
            modelBuilder.Entity<Person>().Property(p => p.Mobile)
                .HasMaxLength(30);
            modelBuilder.Entity<Person>().Property(p => p.Email)
                .HasMaxLength(80);
            // EF6-ban az egy-sok kapcsolatot a principal irányából konfigurálom
            modelBuilder.Entity<Person>()
                .HasMany<Address>(p => p.Addresses)
                .WithRequired(a => a.Person)
                .HasForeignKey(a => a.FI_Person);
            modelBuilder.Entity<Person>().ToTable("Person");
            #endregion

            #region Address
            modelBuilder.Entity<Address>().Property(b => b.Kind)
                .IsRequired();
                //.HasColumnType("char(10)");  Ez nem kell, mert nincs enum...
            //.HasDefaultValue()
            //.HasConversion(new EnumToStringConverter<AddressKind>());  Nincs enum támogatás, a konverziót a megjelenítési rétegben kell megoldani
            modelBuilder.Entity<Address>().Property(a => a.Country3166)
                .IsRequired()
                .HasMaxLength(2);
            modelBuilder.Entity<Address>().Property(a => a.Zip)
                .IsRequired()
                .HasMaxLength(10);
            modelBuilder.Entity<Address>().Property(a => a.City)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Address>().Property(a => a.Line1)
                .IsRequired()
                .HasMaxLength(30);
            modelBuilder.Entity<Address>().Property(a => a.Line2)
                .HasMaxLength(30);
            //modelBuilder.Entity<Address>()   //Ezt a kapcsolatot a másik irányból konfiguráltuk
            //    .HasOne(a => a.Person)
            //    .WithMany(p => p.Addresses)
            //    .HasForeignKey(a => a.FI_Person);

            modelBuilder.Entity<Address>().ToTable("Address");
            #endregion

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
