using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Путь к файлу
            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();
            textBox1.Text += Environment.NewLine;
            // Получаем текст из TextBox и преобразуем его в массив байтов
            byte[] dataToWrite = Encoding.UTF8.GetBytes(textBox1.Text);

            // Записываем данные в файл
            using (FileStream fs = new FileStream(filePath, FileMode.Append))
            {
                fs.Write(dataToWrite, 0, dataToWrite.Length);
            }
            textBox1.Text = " ";
        }
        private void ReviewText_Click(object sender, EventArgs e)
        {
            // Путь к файлу

            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();

            // Считываем данные из файла
            byte[] dataFromFile = File.ReadAllBytes(filePath);

            // Преобразуем массив байтов в строку и отображаем ее в TextBox
            textBox1.Text = Encoding.UTF8.GetString(dataFromFile);
            label3.Text = "Полученный текст из файла";
        }
        public string fileNameWrite()
        {
            string fileName = textBox2.Text;
            return fileName;
        }
        private void CreateFile_Click(object sender, EventArgs e)
        {
            if (File.Exists(fileNameWrite()))
            {
                MessageBox.Show($"Файл с именем {fileNameWrite()} уже существует", "Ошибка");
            }
            else {
                using (FileStream fs = new FileStream(fileNameWrite(), FileMode.CreateNew)) ;
            }
        }

        private void CountLinesButton_Click(object sender, EventArgs e)
        {
            // Путь к файлу
            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();

            // Считываем все строки из файла в массив строк
            string[] lines = File.ReadAllLines(filePath);

            // Выводим количество строк в Label
            label1.Text = $"Количество строк: {lines.Length}";
        }

        private void CountConditionButton_Click(object sender, EventArgs e)
        {
            // Путь к файлу
            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();

            // Считываем все строки из файла в массив строк
            string[] lines = File.ReadAllLines(filePath);

            // Считаем количество строк, содержащих заданное условие
            int count = lines.Count(line => line.Contains("a"));

            // Выводим количество строк в Label
            label4.Text = $"Количество строк \nс условием: {count}";
        }

        private void CountWordButton_Click(object sender, EventArgs e)
        {
            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();
            char charToCount = 'a'; // символ, частоту появления которого мы хотим подсчитать
            int charCount = 0;

            // Читаем данные из файла и подсчитываем частоту появления символа
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    foreach (char c in line)
                    {
                        if (c == charToCount)
                        {
                            charCount++;
                        }
                    }
                }
            }
            label5.Text = $"Количество: {charCount}";
        }

        private void ReplaceButton_Click(object sender, EventArgs e)
        {
            // Путь к файлу
            string oldWord = textBox3.Text;
            string newWord = textBox4.Text;
            string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();
            // Считываем текст из файла в строку
            string text = File.ReadAllText(filePath);

            // Заменяем все вхождения заданного слова на новое слово
            string newText = text.Replace(oldWord, newWord);

            // Преобразуем новый текст в массив байтов и записываем его в файл
            byte[] dataToWrite = Encoding.UTF8.GetBytes(newText);
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(dataToWrite, 0, dataToWrite.Length);
            }
        }

        private void ToUpperButton_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();
                string text = File.ReadAllText(filePath);
                if (radioButton6.Checked)
                {
                    // Приводим текст к верхнему регистру
                    string newText = text.ToUpper();
                    byte[] dataToWrite = Encoding.UTF8.GetBytes(newText);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(dataToWrite, 0, dataToWrite.Length);
                    }
                }
                if (radioButton5.Checked)
                {
                    // Приводим текст к нижнему регистру
                    string newText = text.ToLower();
                    byte[] dataToWrite = Encoding.UTF8.GetBytes(newText);
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        fs.Write(dataToWrite, 0, dataToWrite.Length);
                    }
                }
            }
            if (radioButton2.Checked)
            {
                
                string filePath = @"C:\Users\polla\Desktop\WindowsFormsApp1\WindowsFormsApp1\bin\Debug\" + fileNameWrite();
                string text = File.ReadAllText(filePath);
                string word = textBox5.Text;
                if (radioButton6.Checked)
                { 
                    string fileContent;
                    // Читаем содержимое файла в строку
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        fileContent = sr.ReadToEnd();
                    }
                    if (fileContent.Contains(word))
                    {
                        string newWord = text.Replace(word, word.ToUpper());
                        byte[] dataToWrite = Encoding.UTF8.GetBytes(newWord);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            fs.Write(dataToWrite, 0, dataToWrite.Length);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Слово '{word}' не найдено в файле", "Результат");
                    }
                }
                if (radioButton5.Checked)
                {
                    string fileContent;
                    // Читаем содержимое файла в строку
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        fileContent = sr.ReadToEnd();
                    }
                    if (fileContent.Contains(word))
                    {
                        string newWord = text.Replace(word, word.ToLower());
                        byte[] dataToWrite = Encoding.UTF8.GetBytes(newWord);
                        using (FileStream fs = new FileStream(filePath, FileMode.Create))
                        {
                            fs.Write(dataToWrite, 0, dataToWrite.Length);
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Слово '{word}' не найдено в файле", "Результат");
                    }
                }

            }
        }
    }
}
