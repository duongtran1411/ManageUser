
namespace BLL_User.Model
{
    public class FilterDTO
    {
        public int SkipCount { get; set; }
        public int PageCount { get; set; } = 10;
        public string FilterSearch { get; set; }

    }
}
