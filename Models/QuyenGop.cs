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
    
    public partial class QuyenGop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuyenGop()
        {
            this.DiemTichLuys = new HashSet<DiemTichLuy>();
            this.ThongBaos = new HashSet<ThongBao>();
        }
    
        public int maQG { get; set; }
        public int maND { get; set; }
        public System.DateTime ngayQG { get; set; }
        public int maLQG { get; set; }
        public double soLuong { get; set; }
        public string donVi { get; set; }
        public string trangThai { get; set; }
        public string hinhAnh { get; set; }
        public string ghiChu { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DiemTichLuy> DiemTichLuys { get; set; }
        public virtual LoaiQuyenGop LoaiQuyenGop { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThongBao> ThongBaos { get; set; }
    }
}