﻿#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;

using IKVM.Compiler.Managed;

namespace IKVM.Compiler.Collections
{

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList1<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                default:
                    return ref self.more![index - 1];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 1 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList1<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList1<T> Empty = new FixedValueList1<T>();

        internal readonly int count;
        internal T item0 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList1(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 1)
            {
                var size = count - 1;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList1(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList1(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                for (int i = 1; i < this.count; i++)
                    more[i - 1] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList1(in FixedValueList1<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList1(int count, in FixedValueList1<T> source) :
            this(count)
        {
            item0 = source.item0;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 1)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList1(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                for (int i = 1; i < this.count; i++)
                    more[i - 1] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList1(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                for (int i = 1; i < this.count; i++)
                    more[i - 1] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList1(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];

            if (this.count > 1)
            {
                var s = this.count - 1;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 1];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                default:
                    return more![index - 1];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                default:
                    more![index - 1] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList2<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                default:
                    return ref self.more![index - 2];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 2 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList2<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList2<T> Empty = new FixedValueList2<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList2(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 2)
            {
                var size = count - 2;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList2(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList2(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                for (int i = 2; i < this.count; i++)
                    more[i - 2] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList2(in FixedValueList2<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList2(int count, in FixedValueList2<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 2)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList2(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                for (int i = 2; i < this.count; i++)
                    more[i - 2] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList2(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                for (int i = 2; i < this.count; i++)
                    more[i - 2] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList2(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];

            if (this.count > 2)
            {
                var s = this.count - 2;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 2];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                default:
                    return more![index - 2];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                default:
                    more![index - 2] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList3<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                default:
                    return ref self.more![index - 3];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 3 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList3<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList3<T> Empty = new FixedValueList3<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList3(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 3)
            {
                var size = count - 3;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList3(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList3(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                for (int i = 3; i < this.count; i++)
                    more[i - 3] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList3(in FixedValueList3<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList3(int count, in FixedValueList3<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 3)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList3(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                for (int i = 3; i < this.count; i++)
                    more[i - 3] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList3(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                for (int i = 3; i < this.count; i++)
                    more[i - 3] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList3(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];

            if (this.count > 3)
            {
                var s = this.count - 3;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 3];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                default:
                    return more![index - 3];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                default:
                    more![index - 3] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList4<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                case 3:
                    return ref self.item3;
                default:
                    return ref self.more![index - 4];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 4 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList4<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList4<T> Empty = new FixedValueList4<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal T item3 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList4(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 4)
            {
                var size = count - 4;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList4(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList4(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                for (int i = 4; i < this.count; i++)
                    more[i - 4] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList4(in FixedValueList4<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList4(int count, in FixedValueList4<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            item3 = source.item3;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 4)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList4(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                for (int i = 4; i < this.count; i++)
                    more[i - 4] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList4(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                for (int i = 4; i < this.count; i++)
                    more[i - 4] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList4(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];

            if (this.count > 4)
            {
                var s = this.count - 4;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 4];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                case 3:
                    return item3;
                default:
                    return more![index - 4];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                case 3:
                    item3 = value;
                    break;
                default:
                    more![index - 4] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList5<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                case 3:
                    return ref self.item3;
                case 4:
                    return ref self.item4;
                default:
                    return ref self.more![index - 5];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 5 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList5<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList5<T> Empty = new FixedValueList5<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal T item3 = default;
        internal T item4 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList5(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 5)
            {
                var size = count - 5;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList5(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList5(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                for (int i = 5; i < this.count; i++)
                    more[i - 5] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList5(in FixedValueList5<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList5(int count, in FixedValueList5<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            item3 = source.item3;
            item4 = source.item4;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 5)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList5(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                for (int i = 5; i < this.count; i++)
                    more[i - 5] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList5(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                for (int i = 5; i < this.count; i++)
                    more[i - 5] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList5(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];

            if (this.count > 5)
            {
                var s = this.count - 5;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 5];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                case 3:
                    return item3;
                case 4:
                    return item4;
                default:
                    return more![index - 5];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                case 3:
                    item3 = value;
                    break;
                case 4:
                    item4 = value;
                    break;
                default:
                    more![index - 5] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList6<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                case 3:
                    return ref self.item3;
                case 4:
                    return ref self.item4;
                case 5:
                    return ref self.item5;
                default:
                    return ref self.more![index - 6];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 6 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList6<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList6<T> Empty = new FixedValueList6<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal T item3 = default;
        internal T item4 = default;
        internal T item5 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList6(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 6)
            {
                var size = count - 6;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList6(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList6(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                for (int i = 6; i < this.count; i++)
                    more[i - 6] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList6(in FixedValueList6<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList6(int count, in FixedValueList6<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            item3 = source.item3;
            item4 = source.item4;
            item5 = source.item5;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 6)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList6(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                for (int i = 6; i < this.count; i++)
                    more[i - 6] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList6(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                for (int i = 6; i < this.count; i++)
                    more[i - 6] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList6(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];

            if (this.count > 6)
            {
                var s = this.count - 6;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 6];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                case 3:
                    return item3;
                case 4:
                    return item4;
                case 5:
                    return item5;
                default:
                    return more![index - 6];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                case 3:
                    item3 = value;
                    break;
                case 4:
                    item4 = value;
                    break;
                case 5:
                    item5 = value;
                    break;
                default:
                    more![index - 6] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList7<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                case 3:
                    return ref self.item3;
                case 4:
                    return ref self.item4;
                case 5:
                    return ref self.item5;
                case 6:
                    return ref self.item6;
                default:
                    return ref self.more![index - 7];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 7 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList7<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList7<T> Empty = new FixedValueList7<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal T item3 = default;
        internal T item4 = default;
        internal T item5 = default;
        internal T item6 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList7(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 7)
            {
                var size = count - 7;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList7(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList7(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                for (int i = 7; i < this.count; i++)
                    more[i - 7] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList7(in FixedValueList7<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList7(int count, in FixedValueList7<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            item3 = source.item3;
            item4 = source.item4;
            item5 = source.item5;
            item6 = source.item6;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 7)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList7(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                for (int i = 7; i < this.count; i++)
                    more[i - 7] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList7(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                for (int i = 7; i < this.count; i++)
                    more[i - 7] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList7(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];

            if (this.count > 7)
            {
                var s = this.count - 7;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 7];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                case 3:
                    return item3;
                case 4:
                    return item4;
                case 5:
                    return item5;
                case 6:
                    return item6;
                default:
                    return more![index - 7];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                case 3:
                    item3 = value;
                    break;
                case 4:
                    item4 = value;
                    break;
                case 5:
                    item5 = value;
                    break;
                case 6:
                    item6 = value;
                    break;
                default:
                    more![index - 7] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

    internal static partial class FixedValueList
    {

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static ref T GetItemRef<T>(this ref FixedValueList8<T> self, int index)
        {
            switch (index)
            {
                case 0:
                    return ref self.item0;
                case 1:
                    return ref self.item1;
                case 2:
                    return ref self.item2;
                case 3:
                    return ref self.item3;
                case 4:
                    return ref self.item4;
                case 5:
                    return ref self.item5;
                case 6:
                    return ref self.item6;
                case 7:
                    return ref self.item7;
                default:
                    return ref self.more![index - 8];
            }
        }

    }

    /// <summary>
    /// A fixed structural list implementation that optimizes for lists up to 8 items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal partial struct FixedValueList8<T>
    {

        /// <summary>
        /// Returns an empty list.
        /// </summary>
        public static readonly FixedValueList8<T> Empty = new FixedValueList8<T>();

        internal readonly int count;
        internal T item0 = default;
        internal T item1 = default;
        internal T item2 = default;
        internal T item3 = default;
        internal T item4 = default;
        internal T item5 = default;
        internal T item6 = default;
        internal T item7 = default;
        internal readonly T[] more;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        public FixedValueList8(int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            this.count = count;

            if (count > 8)
            {
                var size = count - 8;
                more = new T[size];
            }
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList8(ReadOnlySpan<T> source) :
            this(source.Length, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList8(int count, ReadOnlySpan<T> source) :
            this(count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                item7 = source[7];
            if (this.count > 8)
                for (int i = 8; i < this.count; i++)
                    more[i - 8] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList8(in FixedValueList8<T> source) :
            this(source.Count, source)
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="source"></param>
        public FixedValueList8(int count, in FixedValueList8<T> source) :
            this(count)
        {
            item0 = source.item0;
            item1 = source.item1;
            item2 = source.item2;
            item3 = source.item3;
            item4 = source.item4;
            item5 = source.item5;
            item6 = source.item6;
            item7 = source.item7;
            source.more?.AsSpan(0, Math.Min(source.more.Length, count - 8)).CopyTo(more);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList8(IList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                item7 = source[7];
            if (this.count > 8)
                for (int i = 8; i < this.count; i++)
                    more[i - 8] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList8(T[] source) :
            this(source.Length)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                item7 = source[7];
            if (this.count > 8)
                for (int i = 8; i < this.count; i++)
                    more[i - 8] = source[i];
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="source"></param>
        public FixedValueList8(IReadOnlyList<T> source) :
            this(source.Count)
        {
            if (this.count > 0)
                item0 = source[0];
            if (this.count > 1)
                item1 = source[1];
            if (this.count > 2)
                item2 = source[2];
            if (this.count > 3)
                item3 = source[3];
            if (this.count > 4)
                item4 = source[4];
            if (this.count > 5)
                item5 = source[5];
            if (this.count > 6)
                item6 = source[6];
            if (this.count > 7)
                item7 = source[7];

            if (this.count > 8)
            {
                var s = this.count - 8;
                for (int i = 0; i < s; i++)
                    more[i] = source[i + 8];
            }
        }

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            readonly get => GetItem(index);
            set => SetItem(index, value);
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        readonly T GetItem(int index)
        {
            switch (index)
            {
                case 0:
                    return item0;
                case 1:
                    return item1;
                case 2:
                    return item2;
                case 3:
                    return item3;
                case 4:
                    return item4;
                case 5:
                    return item5;
                case 6:
                    return item6;
                case 7:
                    return item7;
                default:
                    return more![index - 8];
            }
        }

        /// <summary>
        /// Sets the item at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        void SetItem(int index, T value)
        {
            switch (index)
            {
                case 0:
                    item0 = value;
                    break;
                case 1:
                    item1 = value;
                    break;
                case 2:
                    item2 = value;
                    break;
                case 3:
                    item3 = value;
                    break;
                case 4:
                    item4 = value;
                    break;
                case 5:
                    item5 = value;
                    break;
                case 6:
                    item6 = value;
                    break;
                case 7:
                    item7 = value;
                    break;
                default:
                    more![index - 8] = value;
                    break;
            }
        }

        /// <summary>
        /// Gets the number of items in the list.
        /// </summary>
        public readonly int Count => count;

    }

}