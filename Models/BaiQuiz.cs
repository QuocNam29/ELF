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
    
    public partial class BaiQuiz
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BaiQuiz()
        {
            this.CauHoi_BaiQuiz = new HashSet<CauHoi_BaiQuiz>();
        }
    
        public int maBaiQuiz { get; set; }
        public System.DateTime NgayTao { get; set; }
        public double TongDiem { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CauHoi_BaiQuiz> CauHoi_BaiQuiz { get; set; }
    }
}