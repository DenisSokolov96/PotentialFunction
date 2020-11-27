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
        public List<double[]> listVectors = new List<double[]>();
        //список тестовых данных
        public List<double[]> listTestVectors = new List<double[]>();
        //список куммулятивных потенциалов
        public List<double> listK = new List<double>();
        //список знаков
        public List<int> listZn = new List<int>();
        //список формул
        public List<double> listFormuls = new List<double>();

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
            listFormuls.Clear();
            //детерминированный
            if (trackBar1.Value == 0)
            {
                learning();
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
                recognitionFile();                
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
                    recognitionFile();
                else recognitionFileV3();
            }
            //стохастический
            else
            {

            }
            richTextBox1.Text += "Распознавание завершено.\n";
        }

        /*                                      2                                       */
        /*##############################################################################*/

        private void learning()
        {
            int check = 0;
            int epoch = 1;

            //Первый шаг
            if (listVectors[0][0] == 1)
                listZn.Add(1);
            else listZn.Add(-1);

            for (int i = 1; i < listVectors[0].Length; i++)
                listFormuls.Add(listVectors[0][i]);

            int iVector = 1;
            //Следующие шаги
            while ((check < listVectors.Count()) && (listVectors.Count() > 1))
            {
                double K = funcK(iVector, listVectors);

                if ((K > 0) && (listVectors[iVector][0] == 1.0))
                {
                    check++;
                }
                if ((K < 0) && (listVectors[iVector][0] == 2.0))
                {
                    check++;
                }
                if ((K <= 0) && (listVectors[iVector][0] == 1.0))
                {
                    check = 0;
                    listZn.Add(1);
                    for (int i = 1; i < listVectors[iVector].Length; i++)
                        listFormuls.Add(listVectors[iVector][i]);
                }
                if ((K >= 0) && (listVectors[iVector][0] == 2.0))
                {
                    check = 0;
                    listZn.Add(-1);
                    for (int i = 1; i < listVectors[iVector].Length; i++)
                        listFormuls.Add(listVectors[iVector][i]);
                }

                //проверка, дошли ли мы до конца
                if (iVector == listVectors.Count - 1)
                {
                    iVector = 0;
                }
                else iVector++;

                listK.Add(K);// можно убрать                
                epoch++;
            }
            richTextBox1.Text += "Кол-во итераций обучения: " + epoch.ToString() + "\n";
        }

        private double funcK(int iVector, List<double[]> list)
        {
            double k = 0;
            for (int iExp = 0; iExp < listZn.Count(); iExp++)
            {
                double exp = 0;
                for (int j = 1; j < list[iVector].Length; j++)
                {
                    int a = iExp * (list[iVector].Length - 1);
                    exp += Math.Pow(list[iVector][j] - listFormuls[a + j - 1], 2);
                }
                k += Math.Exp(-exp) * listZn[iExp];
            }
            return k;
        }

        private void recognitionFile()
        {
            for (int iVector = 0; iVector < listTestVectors.Count; iVector++)
            {
                double k = funcK(iVector, listTestVectors);

                if (k < 0) richTextBox1.Text += "2:   ";
                else richTextBox1.Text += "1:  ";

                for (int i = 1; i < listTestVectors[iVector].Length; i++)
                    richTextBox1.Text += listTestVectors[iVector][i].ToString() + "   ";
                richTextBox1.Text += "\n";
            }
        }

        /*                        3 и больше                                            */
        /*##############################################################################*/

        private void recognitionFileV3()
        {
            List<double> listK = new List<double>();
            for (int iVector = 0; iVector < listTestVectors.Count; iVector++)
            {
                double k = funcK(iVector, listTestVectors);

                if (k < 0) richTextBox1.Text += "2:   ";
                else richTextBox1.Text += "1:  ";

                for (int i = 1; i < listTestVectors[iVector].Length; i++)
                    richTextBox1.Text += listTestVectors[iVector][i].ToString() + "   ";
                richTextBox1.Text += "\n";
            }
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
