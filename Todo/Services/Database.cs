using CsvHelper;
using System.Collections.Generic;
using System.IO;
using Todo.Models;

namespace Todo.Services
{
    public class Database
    {
        public List<TodoItem> GetItems()
        {
            using (var reader = new StreamReader(@"C:\Users\ganna.minakova\Desktop\BachelorArbeit\Avalonia Todo\todolist.csv"))
            {
                //List<TodoItem> listDone = new List<TodoItem>();
                List<TodoItem> toDo = new List<TodoItem>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    TodoItem item = new TodoItem
                    {
                        Date = values[0],
                        Description = values[1],
                        IsChecked = bool.Parse(values[2])
                    };
                    toDo.Add(item);
                }
                return toDo;
            }
        }

        public TodoItem SaveItem(TodoItem item){
            using (var writer = new StreamWriter(@"C:\Users\ganna.minakova\Desktop\BachelorArbeit\todo\todolist.csv", append: true))
            {
                var line = $"{item.Date};{item.Description};{item.IsChecked}";
                writer.WriteLine(line);
                writer.Flush();
                writer.Close();
            }

            return item;
        }
    }
}
