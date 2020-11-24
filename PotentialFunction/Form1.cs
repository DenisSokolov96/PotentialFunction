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
    public partial class Form1 : Form
    {
        //список входных данных
        private List<int[]> listVectos = new List<int[]>();
        //список тестовых данных
        private List<int[]> listTestVectos = new List<int[]>();

        /*##############################################################################*/

        public Form1()
        {
            InitializeComponent();
        }

        private void обучитьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listVectos = redFile();
        }
                
        private void распознатьНаФайлеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listTestVectos = redFile();
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
