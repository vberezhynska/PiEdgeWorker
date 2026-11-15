namespace PiEdgeWorker
{
    public delegate int Comparator<in T>(T right, T left);

    internal static class Sorting<T>
    {
        public static T[] Perform(ref T[] array, Comparator<T> compare)
        {
            for (int i = 0; i < array.Length - 1; i++) //iteration
            {
                bool wasSwapped = false;
                for (int j = i + 1; j < array.Length - i - 1; j++) //loop by elements
                {
                    if (compare(array[j], array[j + 1]) > 0) //works when return 1
                    {
                        var temp = array[j]; 
                        array[j] = array[j + 1];
                        array[j + 1] = temp;

                        wasSwapped = true;
                    }
                }

                if (!wasSwapped)
                {
                    Console.WriteLine("Sorgin is completed.");
                    break;
                }
            }

            return array;
        }
    }
}
