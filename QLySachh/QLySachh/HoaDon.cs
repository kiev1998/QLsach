namespace QLySachh
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayDatHang { get; set; }

        [StringLength(20)]
        public string SDTKH { get; set; }

        [StringLength(50)]
        public string DiaChiKH { get; set; }

        [StringLength(20)]
        public string EmailKH { get; set; }

        [StringLength(50)]
        public string ThanhToan { get; set; }

        public int? MaKH { get; set; }

        public virtual ChiTietHD ChiTietHD { get; set; }

        public virtual KhachHang KhachHang { get; set; }
    }
}
