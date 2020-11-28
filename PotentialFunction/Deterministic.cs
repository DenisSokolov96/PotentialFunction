using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialFunction
{    
    class Deterministic
    {

        /*                                      2                                       */
        /*##############################################################################*/

        public string learning()
        {            
            int check = 0;
            int epoch = 1;
            int corFormul = 0;

            //Первый шаг
            if (MainForm.listVectors[0][0] == 1)
                MainForm.listZn.Add(1);
            else MainForm.listZn.Add(-1);

            for (int i = 1; i < MainForm.listVectors[0].Length; i++)
                MainForm.listFormul.Add(MainForm.listVectors[0][i]);

            int iVector = 1;
            //Следующие шаги
            while ((check < MainForm.listVectors.Count()) && (MainForm.listVectors.Count() > 1))
            {
                double K = funcK(iVector, MainForm.listVectors);

                if ((K > 0) && (MainForm.listVectors[iVector][0] == 1.0))
                {
                    check++;
                }
                if ((K < 0) && (MainForm.listVectors[iVector][0] == 2.0))
                {
                    check++;
                }
                if ((K <= 0) && (MainForm.listVectors[iVector][0] == 1.0))
                {
                    check = 0;
                    MainForm.listZn.Add(1);
                    for (int i = 1; i < MainForm.listVectors[iVector].Length; i++)
                        MainForm.listFormul.Add(MainForm.listVectors[iVector][i]);
                    corFormul++;
                    MainForm.listKorrect[0]++;
                }
                if ((K >= 0) && (MainForm.listVectors[iVector][0] == 2.0))
                {
                    check = 0;
                    MainForm.listZn.Add(-1);
                    for (int i = 1; i < MainForm.listVectors[iVector].Length; i++)
                        MainForm.listFormul.Add(MainForm.listVectors[iVector][i]);
                    corFormul++;
                    MainForm.listKorrect[1]++;
                }

                //проверка, дошли ли мы до конца
                if (iVector == MainForm.listVectors.Count - 1)
                {
                    iVector = 0;
                }
                else iVector++;

                epoch++;
            }            
            
            string str = "";
            str += "Корректировка для 1: " + MainForm.listKorrect[0].ToString() + "\n";
            str += "Корректировка для 2: " + MainForm.listKorrect[1].ToString() + "\n";
            str += "Кол-во корректировок формулы: " + corFormul.ToString() + "\n" + "Кол-во итераций обучения: " + epoch + "\n";
            return str;
        }

        private double funcK(int iVector, List<double[]> list)
        {
            double k = 0;
            for (int iExp = 0; iExp < MainForm.listZn.Count(); iExp++)
            {
                double exp = 0;
                for (int j = 1; j < list[iVector].Length; j++)
                {
                    int a = iExp * (list[iVector].Length - 1);
                    exp += Math.Pow(list[iVector][j] - MainForm.listFormul[a + j - 1], 2);
                }
                k += Math.Exp(-exp) * MainForm.listZn[iExp];
            }
            return k;
        }

        public List<string> recognitionFile()
        {
            List<string> text = new List<string>();            
            for (int iVector = 0; iVector < MainForm.listTestVectors.Count; iVector++)
            {
                string str = "";
                double k = funcK(iVector, MainForm.listTestVectors);

                if (k < 0) str += "2:   ";
                else str += "1:  ";

                for (int i = 1; i < MainForm.listTestVectors[iVector].Length; i++)
                    str += MainForm.listTestVectors[iVector][i].ToString() + "   ";
                str += "\n";
                text.Add(str);
            }
            return text;
        }

        /*                        3 и более                                            */
        /*##############################################################################*/

        public string learningCl3()
        {
            string str = "";
            for (int i = 0; i < MainForm.listFormuls.Count(); i++)
                str += learningClass(i+1);
                       
            return str;
        }

        private string learningClass(int iClass)
        {
            int check = 0;
            int epoch = 1;
            if (MainForm.listFormuls[iClass - 1].Count == 0)
            {
                //поиск вектора для iClas класса
                int iVectorCl = 0;
                while ((iVectorCl < MainForm.listVectors.Count()) && (MainForm.listVectors[iVectorCl][0] != iClass))
                    iVectorCl++;
                //Первый шаг
                for (int i = 1; i < MainForm.listVectors[0].Length; i++)
                    MainForm.listFormuls[iClass - 1].Add(MainForm.listVectors[iVectorCl][i]);
            }
            int iVector = 0;
            //Следующие шаги
            while ((check < MainForm.listVectors.Count()) && (MainForm.listVectors.Count() > 1))
            {                
                //поиск вектора для iClas класса
                while ((iVector < MainForm.listVectors.Count()) && (MainForm.listVectors[iVector][0] != iClass))
                    iVector++;
                if (iVector >= MainForm.listVectors.Count()) iVector = 0;
             
                double K = funcKV3(iVector, iClass, MainForm.listVectors);

                if (K > 0) check++;
                if (K <= 0)
                {
                    check = 0;
                    for (int i = 1; i < MainForm.listVectors[iVector].Length; i++)
                        MainForm.listFormuls[iClass - 1].Add(MainForm.listVectors[iVector][i]);
                    MainForm.listKorrect[iClass--]++;
                }

                //проверка, дошли ли мы до конца
                if (iVector == MainForm.listVectors.Count - 1)
                {
                    iVector = 0;
                }
                else iVector++;

                epoch++;
            }

            string str = "";
            str += "Корректировка для " + iClass.ToString() + ": " + MainForm.listKorrect[iClass-1].ToString() + "\n";            
            str += "Кол-во итераций обучения для " + iClass.ToString() + ": " + epoch + "\n";
            return str;
        }
        
        private double funcKV3(int iVector, int iClass, List<double[]> list)
        {
            double k = 0;
            for (int iExp = 0; iExp < MainForm.listFormuls[iClass-1].Count()/2; iExp++)
            {
                double exp = 0;
                for (int j = 1; j < list[iVector].Length; j++)
                {
                    int a = iExp * (list[iVector].Length - 1);
                    exp += Math.Pow(list[iVector][j] - MainForm.listFormuls[iClass-1][a + j - 1], 2);
                }
                k += Math.Exp(-exp);
            }
            return k;
        }

        public List<string> recognitionFileV3()
        {
            List<string> text = new List<string>();            
            List<double> listK = new List<double>();
            for (int iVector = 0; iVector < MainForm.listTestVectors.Count; iVector++)
            {
                string str = "";
                //надо взять вектор, подставить во все формулы, получить на выходе K, и выбрать наибольшее(значит принадлежит этому классу)
                for (int i = 0; i < MainForm.listFormuls.Count(); i++)
                {
                    listK.Add(funcKV3(iVector, i+1, MainForm.listTestVectors));
                }                
                str += (listK.IndexOf(listK.Max()) + 1).ToString() + ": ";
                listK.Clear();
                for (int i = 1; i < MainForm.listTestVectors[iVector].Length; i++)
                    str += MainForm.listTestVectors[iVector][i].ToString() + "   ";
                str += "\n";                
                text.Add(str);
            }
            return text;
        }

    }
}
