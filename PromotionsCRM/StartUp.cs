using PromotionsCRM.Data;
using Microsoft.EntityFrameworkCore;

namespace PromotionsCRM
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            PromotionsCrmDbContext dbContext = new PromotionsCrmDbContext();
        }
    }
}
