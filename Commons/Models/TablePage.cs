using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Models
{
    public class TablePage<T>
    {
        /// <summary>
        /// Set of items with .Skip(SizeId*(pageId-1)).Take(SizeId).ToList()
        /// </summary>
        public IEnumerable<T> Items { get; set; } = new List<T>();

        public int TotalCount { get; set; }

        public int Size { get; set; } = 20;

        public int Number { get; set; }

        public string Search { get; set; }

        /// <summary>
        /// Execution time calculates pages count;
        /// </summary>
        /// <returns></returns>
        public int PagesCount()
        {
            if (TotalCount == 0 || Size == 0) return 1;

            if (TotalCount % Size == 0)
            {
                return (TotalCount / Size);
            }
            else
            {
                return (TotalCount / Size + 1);
            }
        }

        public int NextPage()
        {
            int next = Number + 1;

            if (next <= PagesCount())
            {
                return next;
            }
            else
            {
                return PagesCount();
            }
        }

        public bool IsLastPage()
        {
            return Number == PagesCount();
        }

        public int PreviousPage()
        {
            int pre = Number - 1;

            if (pre >= 1)
            {
                return pre;
            }
            else
            {
                return 1;
            }
        }

        public bool IsFirstPage()
        {
            return Number == 1;
        }
    }

}
