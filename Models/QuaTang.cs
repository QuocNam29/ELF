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
    
    public partial class QuaTang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuaTang()
        {
            this.DonQuas = new HashSet<DonQua>();
        }
    
        public int maQuaTang { get; set; }
        public string tenQuaTang { get; set; }
        public int diemDoi { get; set; }
        public string trangThai { get; set; }
        public System.DateTime ngayTao { get; set; }
        public Nullable<System.DateTime> ngayThayDoi { get; set; }
        public string ghiChu { get; set; }
        public string hinhAnh { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DonQua> DonQuas { get; set; }
    }
}
