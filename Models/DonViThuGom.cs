//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ELF.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonViThuGom
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViThuGom()
        {
            this.DonThuGoms = new HashSet<DonThuGom>();
            this.NguoiDungs = new HashSet<NguoiDung>();
        }
    
        public int maDVTG { get; set; }
        public string tenDVTG { get; set; }
        public string loaiThuGom { get; set; }
        public string diaChi { get; set; }
        public string soDienThoai { get; set; }
        public string hinhThucThuGom { get; set; }
        public string ghiChu { get; set; }
        public string logo { get; set; }
        public string trangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonThuGom> DonThuGoms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
    }
}
