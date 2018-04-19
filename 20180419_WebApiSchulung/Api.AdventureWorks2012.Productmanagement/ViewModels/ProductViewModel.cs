using System.ComponentModel.DataAnnotations;

namespace Api.AdventureWorks2012.Productmanagement.ViewModels
{
    /// <summary>
    /// Minimalanforderung zum Erstellen eines Products.
    /// </summary>
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(25)]
        public string ProductNumber { get; set; }
    }
}