using System.Collections.ObjectModel;
using System.Formats.Tar;

namespace ListaZadan1
{
	public partial class MainPage : ContentPage
	{
		// ObservableCollection automatycznie aktualizuje UI
		public ObservableCollection<string> Tasks { get; set; } = new ObservableCollection<string>();

		// Ścieżka do pliku w bezpiecznym folderze aplikacji
		string filePath = Path.Combine(FileSystem.AppDataDirectory, "tasks.txt");

		public MainPage()
		{
			InitializeComponent();
			LoadTasks();
			TasksView.ItemsSource = Tasks;
		}

		private void OnAddClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(TaskEntry.Text))
			{
				Tasks.Add(TaskEntry.Text);
				TaskEntry.Text = string.Empty;
				SaveTasks();
			}
		}

		private void OnDeleteClicked(object sender, EventArgs e)
		{
			var button = (Button)sender;
			var taskName = (string)button.CommandParameter;

			if (Tasks.Contains(taskName))
			{
				Tasks.Remove(taskName);
				SaveTasks();
			}
		}

		private void SaveTasks()
		{
			// Zapisujemy listę do pliku, każde zadanie w nowej linii
			File.WriteAllLines(filePath, Tasks);
		}

		private void LoadTasks()
		{
			if (File.Exists(filePath))
			{
				var lines = File.ReadAllLines(filePath);
				Tasks.Clear();
				foreach (var line in lines)
				{
					Tasks.Add(line);
				}
			}

		}

	}
}
