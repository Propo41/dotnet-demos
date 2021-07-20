using System.ComponentModel.DataAnnotations;
namespace DebugDemo.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }

        public bool IsGlutenFree { get; set; }
    }
}