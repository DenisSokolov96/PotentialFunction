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
//MK2
//стох
//3 3 - 2
//2 2 - 1
//9 8 -2
//-5 -4 - 2
//детер
//-5 -4 - 1
//1,5 1,5 - 1 
// 9 8 - 2
// 0 0 - 1
// -1 4 - 1
namespace PotentialFunction
{
    public partial class MainForm : Form
    {
        //список входных данных
        public static List<double[]> listVectors = new List<double[]>();
        //список тестовых данных
        public static List<double[]> listTestVectors = new List<double[]>();
        //список формул для 3 и больше
        public static List<List<double>> listFormuls = new List<List<double>>();
        //список корректировки формул
        public static List<int> listKorrect = new List<int>();
        //список знаков
        public static List<double> listZn = new List<double>();
        //список формул
        public static List<double> listFormul = new List<double>();   
        //классы
        Deterministic deterministic = new Deterministic();
        Stochastic stochastic = new Stochastic();

        /*##############################################################################*/

        public MainForm()
        {
            InitializeComponent();
        }

        private void обучитьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listVectors.Clear();
            listVectors = readFile();
            listZn.Clear();
            listFormul.Clear();
            listFormuls.Clear();
            listKorrect.Clear();
            //детерминированный
            if (trackBar1.Value == 0)
            {
                int a = countClass();
                for (int i = 0; i < a; i++)
                    listKorrect.Add(0);
                if (a < 3)
                    richTextBox1.Text += deterministic.learning();
                else
                {
                    for (int i = 0; i < a; i++)
                        listFormuls.Add(new List<double>());

                    richTextBox1.Text += deterministic.learningCl3();
                }
            }
            //стохастический
            else
            {
                int a = countClass();
                for (int i = 0; i < a; i++)
                    listKorrect.Add(0);
                if (a < 3)
                    richTextBox1.Text += stochastic.learning();
                else
                {
                    for (int i = 0; i < a; i++)
                        listFormuls.Add(new List<double>());

                    richTextBox1.Text += stochastic.learningCl3();
                }
            }

            richTextBox1.Text += "Обучение завершено.\n";
        }
                
        private void распознатьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listTestVectors.Clear();
            listTestVectors = readFile();
            //детерминированный
            if (trackBar1.Value == 0)
            {
                int a = countClass();
                if (a < 3)
                {
                    List<string> str = new List<string>();
                    str = deterministic.recognitionFile();
                    //сортировочный вывод
                    for (int iClass = 0; iClass < a; iClass++)
                        for (int i = 0; i < str.Count; i++)
                        {
                            string[] mas = str[i].Split(':');
                            if (Convert.ToInt32(mas[0]) == iClass + 1)
                                richTextBox1.Text += str[i];
                        }
                }
                else
                {
                    List<string> str = new List<string>();
                    str = deterministic.recognitionFileV3();
                    //сортировочный вывод
                    for (int iClass = 0; iClass < a; iClass++)
                        for (int i = 0; i < str.Count; i++)
                        {
                            string[] mas = str[i].Split(':');
                            if (Convert.ToInt32(mas[0]) == iClass + 1)
                                richTextBox1.Text += str[i];
                        }

                }
            }
            //стохастический
            else
            {
                int a = countClass();
                if (a < 3)
                {
                    List<string> str = new List<string>();
                    str = stochastic.recognitionFile();
                    //сортировочный вывод
                    for (int iClass = 0; iClass < a; iClass++)
                        for (int i = 0; i < str.Count; i++)
                        {
                            string[] mas = str[i].Split(':');
                            if (Convert.ToInt32(mas[0]) == iClass + 1)
                                richTextBox1.Text += str[i];
                        }
                }
                else
                {
                    List<string> str = new List<string>();
                    str = stochastic.recognitionFileV3();
                    //сортировочный вывод
                    for (int iClass = 0; iClass < a; iClass++)
                        for (int i = 0; i < str.Count; i++)
                        {
                            string[] mas = str[i].Split(':');
                            if (Convert.ToInt32(mas[0]) == iClass + 1)
                                richTextBox1.Text += str[i];
                        }

                }
            }

            richTextBox1.Text += "Распознавание завершено.\n";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            listTestVectors.Clear();
            //детерминированный
            if (trackBar1.Value == 0)
            {                
                listTestVectors.Add(parsToInt("0 " + textBox1.Text.ToString()));
                if (countClass() < 3)
                    richTextBox1.Text += deterministic.recognitionFile()[0];
                else richTextBox1.Text += deterministic.recognitionFileV3()[0];
            }
            //стохастический
            else
            {
                listTestVectors.Add(parsToInt("0 " + textBox1.Text.ToString()));
                if (countClass() < 3)
                    richTextBox1.Text += stochastic.recognitionFile()[0];
                else richTextBox1.Text += stochastic.recognitionFileV3()[0];
            }
            richTextBox1.Text += "Распознавание завершено.\n";
        }

        /*##############################################################################*/

        private int countClass()
        {
            List<double> list = new List<double>();
            foreach (double[] val in listVectors)
                list.Add(val[0]);

            return list.Distinct().Count();            
        }

        /*##############################################################################*/

        private List<double[]> readFile()
        {
            List<double[]> list = new List<double[]>();
            var filePath = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "D:\\Универ\\2й курс магистратура\\Системы распознавания\\Классификатор по методу потенциальных функций\\PotentialFunction\\PotentialFunction\\datas";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName, Encoding.Default))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            richTextBox1.Text += line.ToString() + "\n";
                            list.Add(parsToInt(line));
                        }
                    }
                }
            }
            richTextBox1.Text += "Файл загружен.\n";
            return list;
        }

        private double[] parsToInt(string line)
        {
            List<double> list = new List<double>();
            string[] mas = line.Split(' ', '\t');
            for (int i = 0; i < mas.Length; i++)            
                list.Add(Convert.ToDouble(mas[i].ToString()));
              
            return list.ToArray();
        }
    }
}
