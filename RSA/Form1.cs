using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace RSA
{
    public partial class RSA : Form
    {
        BigInteger ee;
        BigInteger d;
        BigInteger n;
        bool bol = false;
        public RSA()
        {
            InitializeComponent();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            int numberOne = 0, numberTwo = 0;
            numberOne = Convert.ToInt32(textBoxOne.Text);
            numberTwo = Convert.ToInt32(textBoxTwo.Text);
            ee = Convert.ToInt32(textBoxE.Text);
            if (IsTheNumberSimple(numberOne) && IsTheNumberSimple(numberTwo))
                RSA_Method_GenerateKeys(numberOne, numberTwo);
        }

        private void RSA_Method_GenerateKeys(int numberOne, int numberTwo)
        {
            n = numberOne * numberTwo; // 1) n = p * q
            BigInteger fi = (numberOne - 1) * (numberTwo - 1); //2) Вычислении функции Эйлера
            d = 1;
            for (; ; d++) //3) вычисления d (закрытого ключа)
            {
                BigInteger k = ee * d;

                k = k % fi;
                
                if (k.IsOne)
                {
                    break;
                }
            }
            publicKey.Text = Convert.ToString(ee) + "," + Convert.ToString(n);
            privateKey.Text = Convert.ToString(d) + "," + Convert.ToString(n);
            bol = true;
        }

        private bool IsTheNumberSimple(BigInteger n)
        {
            if (n < 1)
                return false;

            if (n == 2)
                return true;

            for (BigInteger i = 2; i < n; i++)
                if (n % i == 0)
                    return false;

            return true;
        }

        private void buttonCipher_Click(object sender, EventArgs e)
        {
            if (bol)
            {
                BigInteger s = Convert.ToUInt64(textBoxInputNoRSA.Text);
                s = BigInteger.ModPow(s, ee, n);
                textBoxOutputRSA.Text = Convert.ToString(s);
                s = BigInteger.ModPow(s, d, n);
                textBoxOutputNoRSA.Text = Convert.ToString(s);
            }
        }
    }
}
