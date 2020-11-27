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
        //список куммулятивных потенциалов для 3 и больше
        public static List<double> listK = new List<double>();
        //список знаков
        public static List<int> listZn = new List<int>();
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
            listK.Clear();
            //детерминированный
            if (trackBar1.Value == 0)
            {                
                richTextBox1.Text +=deterministic.learning();                
            }
            //стохастический
            else
            {

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
                richTextBox1.Text += deterministic.recognitionFile();                
            }
            //стохастический
            else
            {

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
                if (listTestVectors[0].Length - 1 < 3)
                    richTextBox1.Text += deterministic.recognitionFile();
                else richTextBox1.Text += deterministic.recognitionFileV3();
            }
            //стохастический
            else
            {

            }
            richTextBox1.Text += "Распознавание завершено.\n";
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
