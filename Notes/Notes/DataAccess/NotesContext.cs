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
            optionsBuilder.UseSqlServer(@"DESKTOP-TALUG4B\SQLEXPRESS;Database=GameAd;Trusted_Connection=True;");
        }
    }
}
