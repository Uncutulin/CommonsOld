using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Commons.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Commons.Models
{
    /// <summary>
    /// Model used to create paginated tables with search fields.
    /// For questions and follow up: Santiago Calgaro.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Page<T> : Page where T : class, new()
    {
        public Page()
        {

        }

        public Page(int number, string url, DbSet<T> dbSet, Expression<Func<T, bool>> predicate)
        {
            Number = number;
            Size = 20;
            SetItems(dbSet, predicate);
            Url = url;
        }

        public Page(int number, string url, int size, DbSet<T> dbSet, Expression<Func<T, bool>> predicate)
        {
            Number = number;
            Size = size;
            SetItems(dbSet, predicate);
            Url = url;
        }

        public Page(int number, string url, int size, IQueryable<T> queryable)
        {
            Number = number;
            Size = size;
            SetItems(queryable);
            Url = url;
        }

        /// <summary>
        /// Set of items with .Skip(SizeId*(pageId-1)).Take(SizeId).ToList()
        /// </summary>
        public IEnumerable<T> Items { get; set; } = new List<T>();
        
        /// <summary>
        /// Set items and totalCount from db info.
        /// </summary>
        /// <param name="dbSet">Db table to query.</param>
        /// <param name="predicate">Linnq query.</param>
        private void SetItems(DbSet<T> dbSet, Expression<Func<T, bool>> predicate)
        {
            if (Number == 0) Number = 1;
            Items = dbSet.Where(predicate).Skip(Size * (Number - 1)).Take(Size).ToList();
            TotalCount = dbSet.Count(predicate);
        }

        /// <summary>
        /// Set items and totalCount from db info.
        /// </summary>
        /// <param name="queryable"></param>
        private void SetItems(IQueryable<T> queryable)
        {
            if (Number == 0) Number = 1;
            Items = queryable.Skip(Size * (Number - 1)).Take(Size).ToList();
            TotalCount = queryable.Count();
        }

        /// <summary>
        /// Manualy set items.
        /// </summary>
        public void SelectPage(string url, IEnumerable<T> items, int totalCount, int size = 20)
        {
            if (SearchText == null) SearchText = "";
            Size = size;
            Url = url;
            Items = items.ToList();
            TotalCount = totalCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="size"></param>
        /// <param name="dbSet"></param>
        /// <param name="predicate"></param>
        public void SelectPage(string url, DbSet<T> dbSet, Expression<Func<T, bool>> predicate, int size = 20)
        {
            if (SearchText == null) SearchText = "";
            Size = size;
            Url = url;
            SetItems(dbSet, predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryable"></param>
        /// <param name="size"></param>
        public void SelectPage(string url, IQueryable<T> queryable, int size = 20)
        {
            if (SearchText == null) SearchText = "";
            Size = size;
            Url = url;
            SetItems(queryable);
        }
        /// <summary>
        /// 
        /// </summary>am>
        public void SelectPage(HttpContext context, IQueryable<T> queryable, bool rememberPageState = false, int size = 20)
        {
            string pageStates = "_pageStates";

            if (rememberPageState && SearchText == null && Number == 0)
            {
                var list = context.Session.GetComplexData<List<PageState>>(pageStates);

                var page = list?.FirstOrDefault(x => x.Url == context.Request.Path && !x.Keys.Except(Keys).Any());

                if (page != null)
                {
                    Number = page.Number;
                    SearchText = page.SearchText;
                }

            }

            if (SearchText == null) SearchText = "";
            Size = size;
            Url = context.Request.Path;

            SetItems(queryable);

            if (!rememberPageState) return;
            
            if (context.Session.Keys.All(x => x != pageStates)) context.Session.SetComplexData(pageStates, new List<PageState>());
            var pages = context.Session.GetComplexData<List<PageState>>(pageStates);
            pages.RemoveAll(x => x.Url == Url);
            pages.Add(new PageState()
            {
                Url = Url,
                SearchText = SearchText,
                Number = Number,
                Keys = Keys
            });
            context.Session.SetComplexData(pageStates, pages);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="dbSet"></param>
        /// <param name="predicate"></param>
        /// <param name="size"></param>
        public void SelectPageFromDb(string url, DbSet<T> dbSet, Expression<Func<T, bool>> predicate, int size = 20)
        {
            SelectPage(url, dbSet, predicate, size);
        }
    }


    /// <summary>
    /// Base for Page<>
    /// </summary>
    public abstract class Page
    {
        /// <summary>
        /// Number of items to show in each page.
        /// </summary>
        public int Size { get; set; } = 20;

        /// <summary>
        /// Selected page number.
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Text to use in the search.
        /// </summary>
        public string SearchText { get; set; } 

        /// <summary>
        /// Url to call for the partial view.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Set here the total count of the query.
        /// You should use a diferent query to calculate count in order to improve performance.
        /// </summary>
        public int TotalCount { get; protected set; } = 0;

        /// <summary>
        /// 
        /// </summary>
        public List<(string, string)> Arguments { get; private set; } = new List<(string, string)>();
        public List<(string, string)> Keys { get; private set; } = new List<(string, string)>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddArgument(string name, string value)
        {
            Arguments.Add((name, value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void AddKey(string name, string value)
        {
            Arguments.Add((name, value));
            Keys.Add((name, value));
        }

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

        /// <summary>
        /// Calculate next page if exists.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns true if its the last page.
        /// </summary>
        /// <returns></returns>
        public bool IsLastPage()
        {
            return Number == PagesCount();
        }

        /// <summary>
        /// Calculate previous page if exists.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns true if its the first page.
        /// </summary>
        /// <returns></returns>
        public bool IsFirstPage()
        {
            return Number == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetArgumentValue(string name)
        {
            return Arguments.FirstOrDefault(x => x.Item1 == name).Item2;
        }
    }

    /// <summary>
    /// For saving page states on session memory.
    /// </summary>
    public class PageState
    {
        public string SearchText { get; set; }
        public int Number { get; set; }
        public string Url { get; set; }
        public List<(string, string)> Keys { get; set; }
    }
}
