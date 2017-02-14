namespace MyAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("myaccount.User")]
    public partial class User
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string login { get; set; }

        [Required]
        [StringLength(255)]
        public string password { get; set; }
    }
}
