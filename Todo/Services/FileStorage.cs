using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Todo.Models;

namespace Todo.Services
{
    public class FileStorage
    {
        public IList<TodoItem> Load(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            return csv.GetRecords<TodoItem>().ToList();
        }

        public void Save(string filePath, IEnumerable<TodoItem> content)
        {
            using var writer = new StreamWriter(filePath);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(content);
        }
    }
}
