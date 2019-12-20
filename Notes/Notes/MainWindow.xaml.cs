using Notes.DataAccess;
using Notes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Notes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Note> notes = new List<Note>();
        public MainWindow()
        {
            InitializeComponent();
            using(var context = new NotesContext())
            {
                notes = context.Notes.Where(x=> x.DeletedDate == null).ToList();
            }
            notesDG.ItemsSource = notes;
        }

        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private Task Delete()
        {
            using (var context = new NotesContext())
            {
                var selectedNote = notesDG.SelectedItem as Note;
                var noteDb = context.Notes.FirstOrDefault(x => x.Id == selectedNote.Id && x.DeletedDate == null);
                noteDb.DeletedDate = DateTime.Now;
                context.Update(noteDb);
                notes = context.Notes.Where(x => x.DeletedDate == null).ToList();
                notesDG.ItemsSource = notes;
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        private Task Save()
        {
            using (var context = new NotesContext())
            {
                foreach (var note in notes)
                {
                    var noteDb = context.Notes.FirstOrDefault(x => x.Id == note.Id && x.DeletedDate == null);
                    if (noteDb is null)
                    {
                        context.Add(note);
                    }
                    else
                    {
                        noteDb.Text = note.Text;
                        noteDb.IsCompleted = note.IsCompleted;
                        context.Update(noteDb);
                    }
                }
                context.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
