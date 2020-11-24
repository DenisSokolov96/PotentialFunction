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

namespace PotentialFunction
{
    public partial class MainForm : Form
    {
        //список входных данных
        private List<int[]> listVectors = new List<int[]>();
        //список тестовых данных
        private List<int[]> listTestVectos = new List<int[]>();
        //список куммулятивных потенциалов
        private List<double> listK = new List<double>();
        //список знаков
        private List<int> listZn = new List<int>();
        //Список индексов элементов для расчета экспонент
        private List<int> listExp = new List<int>();

        /*##############################################################################*/

        public MainForm()
        {
            InitializeComponent();
        }

        private void обучитьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listVectors = redFile();
            learning();
            richTextBox1.Text += "Обучение завершено.\n";
        }
                
        private void распознатьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listTestVectos = redFile();
            recognition();
            richTextBox1.Text += "Распазнавание завершено.\n";
        }

        /*##############################################################################*/

        private void learning()
        {           
            int check = 0;
            int indnClass = 0;            

            //Первый шаг
            if (listVectors[indnClass][0] == 1)
            {
                listZn.Add(1);
                listExp.Add(0);
            }
            else { listZn.Add(-1); }

            //Следующие шаги
            while (check < listVectors.Count()-1)
            {
                double K = 0;
                for (int i = 0; i < listZn.Count; i++)
                {
                    for (int j = 0; j < listVectors[0].Length - 1; j++)
                    {
                        K += Math.Exp(-Math.Pow(listVectors[indnClass][j + 1] - listVectors[Convert.ToInt32(listExp[i])][j + 1], 2)
                                      ) * Convert.ToDouble(listZn[i]);
                    }

                }

                if (((K > 0) && (listVectors[indnClass][0] == 1)) || ((K < 0) && (listVectors[indnClass][0] == 2)))
                { check++; }
                else if ((K <= 0) && (listVectors[indnClass][0] == 1))
                {
                    check = 0;
                    listZn.Add(1);
                    listExp.Add(indnClass);
                }
                else if ((K >= 0) && (listVectors[indnClass][0] == 2))
                {
                    check = 0;
                    listZn.Add(-1);
                    listExp.Add(indnClass);
                }
                if (indnClass == listVectors.Count - 1)
                {
                    indnClass = 0;
                }
                else indnClass++;
                
                listK.Add(K);               
            }
            richTextBox1.Text += "Кол-во итераций обучения: " + listK.Count.ToString() + "\n";
            for (int i = 0; i < listExp.Count; i++)
            {
                richTextBox1.Text += "**************************************\n";
                richTextBox1.Text += listExp[i].ToString() + "\n";
                richTextBox1.Text += "**************************************\n";
            }

        }

        private void recognition()
        {
            double k = 0.0;
            double ksum = 0.0;
            for (int j = 0; j < listTestVectos.Count; j++)
            {
                k = 0.0;
                ksum = 0.0;
                for (int i = 0; i < listZn.Count; i++)
                {
                    k = 0.0;
                    for (int g = 0; g < listTestVectos[0].Length-1; g++)
                    {
                        k += Math.Exp(-Math.Pow(listTestVectos[j][g] - listVectors[Convert.ToInt32(listExp[i])][g + 1], 2));
                    }
                    ksum += k * Convert.ToDouble(listZn[i]);
                }
                if (ksum < 0) richTextBox1.Text += "2\n";
                else richTextBox1.Text += "1\n";
                
            }
        }

        /*##############################################################################*/

        private List<int[]> redFile()
        {
            List<int[]> list = new List<int[]>();
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

        private int[] parsToInt(string line)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] != '\t')
                    list.Add(Convert.ToInt32(line[i].ToString()));
            }

            return list.ToArray();
        }

    }
}
