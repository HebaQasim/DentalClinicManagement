//using Microsoft.EntityFrameworkCore;

//namespace DentalClinicManagement.InfrastructureLayer.Repositories
//{
//    public class PaginatedList<T>
//    {
       

//        public List<T> Items { get; }
//        public int PageNumber { get; }
//        public int PageSize { get; }
//        public int TotalCount { get; }
//        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

//        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
//        {
//            Items = items;
//            TotalCount = count;
//            PageNumber = pageNumber;
//            PageSize = pageSize;
//        }

  

//        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
//        {
//            var count = await source.CountAsync();
//            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
//            return new PaginatedList<T>(items, count, pageNumber, pageSize);
//        }
//    }
//}
