﻿using System.Web.UI;

namespace AventyrligaKontakter
{
    public static class PageExtensions
    {
        public static object GetTempData(this Page page, string key)
        {
            var value = page.Session[key];
            page.Session.Remove(key);
            return value;
        }

        public static object PeekTempData(this Page page, string key)
        {
            return page.Session[key];
        }

        public static void SetTempData(this Page page, string key, object value)
        {
            page.Session[key] = value;
        }
    }
}