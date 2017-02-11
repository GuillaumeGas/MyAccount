namespace MyAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("myaccount.transaction")]
    public partial class transaction
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        public float value { get; set; }

        public bool validated { get; set; }

        [Required]
        public int id_category { get; set; }

        [Required]
        public int id_account { get; set; }

        [Required]
        public DateTime date { get; set; }
    }
}
