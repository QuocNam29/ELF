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
    
    public partial class PhuongThiTran
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhuongThiTran()
        {
            this.NguoiDungs = new HashSet<NguoiDung>();
        }
    
        public int maPhuong { get; set; }
        public string tenPhuong { get; set; }
        public int maQuan { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NguoiDung> NguoiDungs { get; set; }
        public virtual QuanHuyen QuanHuyen { get; set; }
    }
}
