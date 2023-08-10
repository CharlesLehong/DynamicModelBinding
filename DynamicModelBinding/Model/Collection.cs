using System.ComponentModel.DataAnnotations;

namespace DynamicModelBinding.Model
{
    public class Collection
    {
        public int CollectionId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
