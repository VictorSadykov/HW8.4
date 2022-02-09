using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputPath = @"C:\test\Students.dat";
            string outputPath = @"C:\test\Students";
            BinaryFormatter formatter = new BinaryFormatter();
            DirectoryInfo di = new DirectoryInfo(outputPath);

            di.Create();
            Student[] students;

            if (File.Exists(inputPath))
            {
                using (var fs = new FileStream(inputPath, FileMode.OpenOrCreate))
                {
                    students = (Student[])formatter.Deserialize(fs);

                    List<string> groups = new List<string>(); // Создаем список групп
                    foreach (var student in students)
                    {
                        if (!groups.Contains(student.Group))
                        {
                            groups.Add(student.Group);
                        }
                    }

                    foreach (var group in groups)
                    {
                        using (StreamWriter sw = new StreamWriter(@$"{outputPath}\{group}.txt"))
                        {
                            foreach (var student in students)
                            {
                                if (group == student.Group)
                                {
                                    sw.WriteLine($"Имя: {student.Name}; Дата рождения: {student.DateOfBirth}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
