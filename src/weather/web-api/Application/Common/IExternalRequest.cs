using Domain.Common;
using Domain.WeatherForecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IExternalRequest<T> where T : IHaveId
    {
        /// <summary>
        /// get all or a part of items
        /// </summary>
        /// <param name="start">the index to start the search (base 0)</param>
        /// <param name="offset">the offset to apply</param>
        /// <returns></returns>
        IEnumerable<T> List(int? start = null, int? offset = null);

        /// <summary>
        /// gets the item by identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T? Get(int id);


        /// <summary>
        /// sets the item in a data persistance system
        /// </summary>
        /// <param name="item">the item to save, if id == 0 the operation will be an insert, otherwhise it'will be an update</param>
        /// <returns>the saved item or null when operation can't be performed</returns>
        T? Set(T item);

        /// <summary>
        /// deletes the item within a data persistance system
        /// </summary>
        /// <param name="id">the identifier of the item</param>
        /// <returns>the delete item or null when operation can't be performed</returns>
        T? Delete(int id);
    }
}
