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
    
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            this.ChucNangTaiKhoans = new HashSet<ChucNangTaiKhoan>();
        }
    
        public int ID { get; set; }
        public string email { get; set; }
        public int maND { get; set; }
        public string matKhau { get; set; }
        public string xacNhanMatKhau { get; set; }
        public Nullable<System.DateTime> ngayTao { get; set; }
        public bool trangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChucNangTaiKhoan> ChucNangTaiKhoans { get; set; }
        public virtual NguoiDung NguoiDung { get; set; }
    }
}
