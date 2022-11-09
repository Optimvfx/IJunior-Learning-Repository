namespace Extenstions
{
    public class ArrayExtenstions
    {
        public const int WidhtIndex = 0;
        public const int HeightIndex = 1;

        public const int ArrayIndexMinValue = 0;

        public T[,] Clone2DArray<T>(T[,] array)
        {
            var newArray = new T[array.GetLength(WidhtIndex), array.GetLength(HeightIndex)];

            for (int x = 0; x < array.GetLength(WidhtIndex); x++)
            {
                for (int y = 0; y < array.GetLength(HeightIndex); y++)
                {
                    newArray[x, y] = array[x, y];
                }
            }

            return newArray;
        }
    }
}
