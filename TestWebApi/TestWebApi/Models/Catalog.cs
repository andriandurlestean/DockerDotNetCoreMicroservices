using System;

namespace TestWebApi.Models
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}