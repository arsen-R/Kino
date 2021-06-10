using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KinoSite.Models
{
    public class Pagenation
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public Pagenation(int count, int pageIndex, int totalPages)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageIndex);
        }
        public bool PreviosPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }
        public bool NextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
