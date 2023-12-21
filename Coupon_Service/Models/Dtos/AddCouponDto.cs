using System.ComponentModel.DataAnnotations;

namespace Coupon_Service.Models.Dtos
{
    public class AddCouponDto
    {
        [Required]
        public string CouponCode { get; set; } = string.Empty;
        [Required]
        [Range(100, 100000)]
        public int CouponAmount { get; set; }
        [Required]
        [Range(1000, int.MaxValue)]
        public int CouponMinAmount { get; set; }
    }
}
