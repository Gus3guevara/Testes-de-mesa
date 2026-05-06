using System;

namespace Teste_Mesa
{


    public class Teste_Mesa_1
    {
        public int Calculo_C(int A, int B)
        {
            int C = (A + B) / 2;
            C = C - 40;
            return C;
        }

        public int Teste()
        {
            const int A = 10, B = 20;
            int C = Calculo_C(A, B);
            int D = (A + B + C);
            return D;
        }
        public void Functionality()
        {
            Teste_Mesa_1 teste = new Teste_Mesa_1();
            int[] V = { 0, 0, 0, teste.Teste() };
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(V[i]);
            }
        }

    }
}
