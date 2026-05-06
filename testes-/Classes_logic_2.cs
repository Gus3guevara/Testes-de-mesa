using System;
namespace Teste_Mesa
{
    public class Teste_Mesa_2
    {
        public void Functionality()
        {
            int a = 2;
            int[] V = new int[10];
            while (a < 6)
            {
                V[a] = a * 10;
                Console.WriteLine(V[a]);
                a++;
            }
        }
    }
}