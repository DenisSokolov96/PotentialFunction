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
                }
                if ((K >= 0) && (MainForm.listVectors[iVector][0] == 2.0))
                {
                    check = 0;
                    MainForm.listZn.Add(-1);
                    for (int i = 1; i < MainForm.listVectors[iVector].Length; i++)
                        MainForm.listFormul.Add(MainForm.listVectors[iVector][i]);
                }

                //проверка, дошли ли мы до конца
                if (iVector == MainForm.listVectors.Count - 1)
                {
                    iVector = 0;
                }
                else iVector++;

                epoch++;
            }
            return "Кол-во итераций обучения: " + epoch + "\n";
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

        public string recognitionFile()
        {
            string str = "";
            for (int iVector = 0; iVector < MainForm.listTestVectors.Count; iVector++)
            {
                double k = funcK(iVector, MainForm.listTestVectors);

                if (k < 0) str += "2:   ";
                else str += "1:  ";

                for (int i = 1; i < MainForm.listTestVectors[iVector].Length; i++)
                    str += MainForm.listTestVectors[iVector][i].ToString() + "   ";
                str += "\n";
            }
            return str;
        }

        /*                        3 и больше                                            */
        /*##############################################################################*/

        public string recognitionFileV3()
        {
            string str = "";
            List<double> listK = new List<double>();
            for (int iVector = 0; iVector < MainForm.listTestVectors.Count; iVector++)
            {
                double k = funcK(iVector, MainForm.listTestVectors);

                if (k < 0) str += "2:   ";
                else str += "1:  ";

                for (int i = 1; i < MainForm.listTestVectors[iVector].Length; i++)
                    str += MainForm.listTestVectors[iVector][i].ToString() + "   ";
                str += "\n";
            }
            return str;
        }

    }
}
