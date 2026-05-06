using System;
namespace Teste_Mesa
{
    public class Teste_Mesa_3
    {
        public void Functionality()
        {
            int a = 7, b = a - 6;
            int[] V = new int[10];
            while (b < a)
            {
                V[b] = b + a;
                Console.WriteLine(V[b]);
                b = b + 2;
            }

        }

    }
}