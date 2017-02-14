namespace MyAccount.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("myaccount.Transaction")]
    public partial class Transaction
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        public float value { get; set; }

        public bool validated { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        public int category_id { get; set; }

        public int account_id { get; set; }
    }
}
