using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.DataAccess
{
    public class NotesContext : DbContext
    {
        public NotesContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=A-104-15;Database=NotesDb;Trusted_Connection=True;");
        }
    }
}
