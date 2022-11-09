using System;
using System.Collections;
using System.Collections.Generic;

namespace Extenstions
{
    public class Array2D<T> : IClonable<Array2D<T>>
    {
        private T[,] _array;

        public int Widht => _array.GetLength(ArrayExtenstions.WidhtIndex);
        public int Height => _array.GetLength(ArrayExtenstions.HeightIndex);

        public Array2D(int widht, int height)
        {
            if (widht < ArrayExtenstions.ArrayIndexMinValue || height < ArrayExtenstions.ArrayIndexMinValue)
                throw new ArgumentException();

            _array = new T[widht, height];
        }

        public Array2D(T[,] array)
        {
            _array = new ArrayExtenstions().Clone2DArray(array);
        }

        public Array2D(Array2D<T> array)
        {
            _array = new ArrayExtenstions().Clone2DArray(array.GetArray());
        }

        public Array2D<T> Clone()
        {
            return new Array2D<T>(GetArray());
        }

        public T[,] GetArray()
        {
            return new ArrayExtenstions().Clone2DArray(_array);
        }

        public T this[int x, int y]
        {
            get
            {
                if (OutOfBounds(x, y))
                    throw new ArgumentException("Index out of range!");

                return _array[x, y];
            }

            set
            {
                if (OutOfBounds(x, y))
                    throw new ArgumentException("Index out of range!");

                _array[x, y] = value;
            }
        }

        public bool OutOfBounds(int x, int y)
        {
            return x < ArrayExtenstions.ArrayIndexMinValue || x >= _array.GetLength(ArrayExtenstions.WidhtIndex) || y < ArrayExtenstions.ArrayIndexMinValue || y >= _array.GetLength(ArrayExtenstions.HeightIndex);
        }
    }
}