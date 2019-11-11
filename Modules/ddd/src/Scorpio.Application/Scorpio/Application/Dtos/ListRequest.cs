using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Application.Dtos
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntityDto"></typeparam>
    public class ListRequest<TEntityDto> : IPagingRequest, IFilterRequest, ISortingRequest
    {
        string IFilterRequest.Where => WhereString;
        int IPagingRequest.SkipCount => SkipCount;
        int IPagingRequest.MaxResultCount => MaxResultCount;
        object[] IFilterRequest.Parameters => Parameters;
        string ISortingRequest.Sorting => string.Join(",", Sorting);

        public int SkipCount { get; set; }

        public int MaxResultCount { get; set; }

        protected object[] Parameters { get; set; }

        protected string WhereString { get; set; }

        protected List<string> Sorting { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public ListRequest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        public ListRequest(int skipCount, int maxResultCount)
        {
            SkipCount = skipCount;
            MaxResultCount = maxResultCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        public ListRequest(int skipCount, int maxResultCount, string sorting) : this(skipCount, maxResultCount)
        {
            Sorting.Add(sorting);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public ListRequest<TEntityDto> Where(string where,params object[] parameters)
        {
            WhereString = where;
            Parameters = parameters;
            return this;
        }

        public ListRequest<TEntityDto> Sort(string sorting)
        {
            Sorting.Clear();
            Sorting.Add(sorting);
            return this;
        }

        public ListRequest<TEntityDto> AddSort(string sorting)
        {
            Sorting.Add(sorting);
            return this;
        }
    }
}
