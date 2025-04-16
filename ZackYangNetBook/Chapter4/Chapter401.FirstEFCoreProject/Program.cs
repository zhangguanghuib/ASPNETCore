using System.Linq;
using System.Threading.Tasks;

namespace Chapter401.FirstEFCoreProject
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await Program.InsertBooks();
            //await Program.QueryBooks();
            await Program.QueryBookOrderBy();
        }

        private static async Task InsertBooks()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                var b1 = new Book
                {
                    AuthorName = "杨中科",
                    Title = "零基础趣学C语言",
                    Price = 59.8,
                    PubTime = new DateTime(2019, 3, 1)
                };
                var b2 = new Book
                {
                    AuthorName = "Robert Sedgewick",
                    Title = "算法(第4版)",
                    Price = 99,
                    PubTime = new DateTime(2012, 10, 1)
                };
                var b3 = new Book
                {
                    AuthorName = "吴军",
                    Title = "数学之美",
                    Price = 69,
                    PubTime = new DateTime(2020, 5, 1)
                };
                var b4 = new Book
                {
                    AuthorName = "杨中科",
                    Title = "程序员的SQL金典",
                    Price = 52,
                    PubTime = new DateTime(2008, 9, 1)
                };
                var b5 = new Book
                {
                    AuthorName = "吴军",
                    Title = "文明之光",
                    Price = 246,
                    PubTime = new DateTime(2017, 3, 1)
                };
                ctx.Books.Add(b1);
                ctx.Books.Add(b2);
                ctx.Books.Add(b3);
                ctx.Books.Add(b4);
                ctx.Books.Add(b5);
                await ctx.SaveChangesAsync();
            }
        }

        public static async Task QueryBooks()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                Console.WriteLine("***All books**");
                foreach (var book in ctx.Books)
                {
                    Console.WriteLine($"{book.Id} {book.Title} {book.AuthorName} {book.Price} {book.PubTime}");
                }
                Console.WriteLine("***Books by 杨中科***");
                IEnumerable<Book> books2 = ctx.Books.Where(b => b.Price > 80);
                foreach (var book in books2)
                {
                    Console.WriteLine($"{book.Id} {book.Title} {book.AuthorName} {book.Price} {book.PubTime}");
                }

                await Task.CompletedTask.ConfigureAwait(false);
            }
        }

        public static async Task QueryBook2()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                Book b1 = ctx.Books.Single<Book>(b=>b.Title == "零基础趣学C语言");
                Console.WriteLine($"{b1.Id} {b1.Title} {b1.AuthorName} {b1.Price} {b1.PubTime}");
                Book? b2 = ctx.Books.SingleOrDefault<Book>(b => b.Id == 9);
                if (b2 == null)
                {
                    Console.WriteLine("没有找到ID为9的书");
                }
                else
                {
                    Console.WriteLine($"{b2.Id} {b2.Title} {b2.AuthorName} {b2.Price} {b2.PubTime}");
                }
                await Task.CompletedTask.ConfigureAwait(false);
            }
        }

       public static async Task QueryBookOrderBy()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                IEnumerable<Book> books = ctx.Books.OrderByDescending(b => b.Price);
                foreach (var book in books)
                {
                    Console.WriteLine($"{book.Id} {book.Title} {book.AuthorName} {book.Price} {book.PubTime}");
                }
                await Task.CompletedTask.ConfigureAwait(false);
            }
        }

        public static async Task QueryBookGroupBy()
        {
            using (TestDbContext ctx = new TestDbContext())
            {
                var groups = ctx.Books.GroupBy(b=>b.AuthorName).Select(g => new
                {
                    AuthorName = g.Key,
                    Count = g.Count(),
                    MaxPrice = g.Max(b => b.Price)
                });

                foreach (var group in groups)
                {
                    Console.WriteLine($"{group.AuthorName} {group.Count} {group.MaxPrice}");
                }

                await Task.CompletedTask.ConfigureAwait(false);
            }
        }
    }
}
