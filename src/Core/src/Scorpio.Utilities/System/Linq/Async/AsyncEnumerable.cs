using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Scorpio;

namespace System.Linq.Async
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class AsyncEnumerable
    {
        /// <summary>
        /// Converts an enumerable sequence to an async-enumerable sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">Enumerable sequence to convert to an async-enumerable sequence.</param>
        /// <returns>The async-enumerable sequence whose elements are pulled from the given enumerable sequence.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
        public static IAsyncEnumerable<TSource> ToAsyncEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            Check.NotNull(source, nameof(source));

            return source switch
            {
                IList<TSource> list => new AsyncIListEnumerableAdapter<TSource>(list),
                ICollection<TSource> collection => new AsyncICollectionEnumerableAdapter<TSource>(collection),
                _ => new AsyncEnumerableAdapter<TSource>(source),
            };
        }

        private abstract class AsyncEnumerableAdapterBase<T> : AsyncIterator<T>, IAsyncIListProvider<T>
        {
            private IEnumerator<T> _enumerator;

            protected AsyncEnumerableAdapterBase()
            {
            }

            public override async ValueTask DisposeAsync()
            {
                if (_enumerator != null)
                {
                    _enumerator.Dispose();
                    _enumerator = null;
                }
                await base.DisposeAsync().ConfigureAwait(false);
            }

            [Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used", Justification = "<挂起>")]
            protected override async ValueTask<bool> MoveNextCore()
            {
                switch (_state)
                {
                    case AsyncIteratorState.Allocated:
                        _enumerator = GetEnumerator();
                        _state = AsyncIteratorState.Iterating;
                        goto case AsyncIteratorState.Iterating;

                    case AsyncIteratorState.Iterating:
                        if (_enumerator!.MoveNext())
                        {
                            _current = _enumerator.Current;
                            return true;
                        }
                        await DisposeAsync().ConfigureAwait(false);
                        break;
                }
                return false;
            }

            //
            // NB: These optimizations rely on the System.Linq implementation of IEnumerable<T> operators to optimize
            //     and short-circuit as appropriate.
            //

            public ValueTask<T[]> ToArrayAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                return new ValueTask<T[]>(ToArray());
            }


            public ValueTask<List<T>> ToListAsync(CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                return new ValueTask<List<T>>(ToList());
            }



            public ValueTask<int> GetCountAsync(bool onlyIfCheap, CancellationToken cancellationToken)
            {
                cancellationToken.ThrowIfCancellationRequested();
                return new ValueTask<int>(Count());
            }

            protected abstract T[] ToArray();

            protected abstract List<T> ToList();

            protected abstract int Count();

            protected abstract IEnumerator<T> GetEnumerator();
        }

        private abstract class AsyncICollectionEnumerableAdapterBase<T> : AsyncEnumerableAdapterBase<T>, ICollection<T>
        {
            private readonly ICollection<T> _source;

            protected AsyncICollectionEnumerableAdapterBase(ICollection<T> source)
            {
                _source = source;
            }


            protected override int Count() => _source.Count;

            protected override IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

            protected override T[] ToArray() => _source.ToArray();

            protected override List<T> ToList() => _source.ToList();


            IEnumerator<T> IEnumerable<T>.GetEnumerator() => _source.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => _source.GetEnumerator();

            void ICollection<T>.Add(T item) => _source.Add(item);

            void ICollection<T>.Clear() => _source.Clear();

            bool ICollection<T>.Contains(T item) => _source.Contains(item);

            void ICollection<T>.CopyTo(T[] array, int arrayIndex) => _source.CopyTo(array, arrayIndex);

            bool ICollection<T>.Remove(T item) => _source.Remove(item);

            int ICollection<T>.Count => _source.Count;

            bool ICollection<T>.IsReadOnly => _source.IsReadOnly;
        }


        private sealed class AsyncEnumerableAdapter<T> : AsyncEnumerableAdapterBase<T>
        {
            private readonly IEnumerable<T> _source;


            public AsyncEnumerableAdapter(IEnumerable<T> source)
            {
                _source = source;
            }

            public override AsyncIteratorBase<T> Clone() => new AsyncEnumerableAdapter<T>(_source);

            protected override int Count() => _source.Count();

            protected override IEnumerator<T> GetEnumerator() => _source.GetEnumerator();

            protected override T[] ToArray() => _source.ToArray();

            protected override List<T> ToList() => _source.ToList();
        }

        private sealed class AsyncIListEnumerableAdapter<T> : AsyncICollectionEnumerableAdapterBase<T>, IList<T>
        {
            private readonly IList<T> _source;

            public AsyncIListEnumerableAdapter(IList<T> source) : base(source)
            {
                _source = source;
            }

            public override AsyncIteratorBase<T> Clone() => new AsyncIListEnumerableAdapter<T>(_source);

            int IList<T>.IndexOf(T item) => _source.IndexOf(item);

            void IList<T>.Insert(int index, T item) => _source.Insert(index, item);

            void IList<T>.RemoveAt(int index) => _source.RemoveAt(index);

            T IList<T>.this[int index]
            {
                get => _source[index];
                set => _source[index] = value;
            }
        }
        private sealed class AsyncICollectionEnumerableAdapter<T> : AsyncICollectionEnumerableAdapterBase<T>
        {
            private readonly ICollection<T> _source;

            public AsyncICollectionEnumerableAdapter(ICollection<T> source) : base(source)
            {
                _source = source;
            }

            public override AsyncIteratorBase<T> Clone() => new AsyncICollectionEnumerableAdapter<T>(_source);

        }

    }
}
