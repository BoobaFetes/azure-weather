using Application.Common;
using Domain.Common;
using Domain.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public abstract class RequestBase<T> : IExternalRequest<T> where T : IHaveId
    {
        protected static int PkValue = 0;
        public static Dictionary<int, T> InMemories { get; protected set; } = new Dictionary<int, T>();

        public IEnumerable<T> List(int? start, int? offset)
        {
            IEnumerable<T> result = InMemories.Values;
            if (start.HasValue && start > 0) { result = result.Skip(start.Value); }
            if (offset.HasValue) { result = result.Take(offset.Value); }
            return result;
        }

        public T? Get(int id)
        {
            T? value;
            if (!InMemories.TryGetValue(id, out value)) { return default(T); }

            return (T)value.Clone();
        }

        public T? Set(T item)
        {
            T? value;
            if (item.Id.HasValue)
            {
                if (!InMemories.TryGetValue(item.Id.Value, out value)) { return default(T); }
                InMemories[item.Id.Value] = (T)value.Clone();

                return (T)item.Clone();
            }

            value = this.NewInstance(PkValue, item);
            InMemories.Add(PkValue, value);
            PkValue++;

            return value;
        }
        protected abstract T NewInstance(int id, T item);

        public T? Delete(int id)
        {
            T? value;
            if (!InMemories.TryGetValue(id, out value)) { return default(T); }

            InMemories.Remove(id);
            return (T)value.Clone();
        }
    }
}
