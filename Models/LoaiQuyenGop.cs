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
    
    public partial class LoaiQuyenGop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiQuyenGop()
        {
            this.QuyenGops = new HashSet<QuyenGop>();
        }
    
        public int maLQG { get; set; }
        public string tenLoai { get; set; }
        public string noiDung { get; set; }
        public string viTriQP { get; set; }
        public string ghiChu { get; set; }
        public string trangThai { get; set; }
        public string hinhAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuyenGop> QuyenGops { get; set; }
    }
}
