

namespace LongestSecuence
{
    class Program
    {
        static void Main(string[] args)
        {
            String fileName = "matrix.txt";
            MatrixProcessor processor = new MatrixProcessor(fileName);
            String Longest = processor.getLongestSecuence();
            Console.WriteLine(Longest);
        }

        
    }
}