using System.ComponentModel.DataAnnotations;

namespace MyWebApi.Data
{
    public class LoaiModel
    {
        [Required]
        public string TenName { get; set; }
    }
}
