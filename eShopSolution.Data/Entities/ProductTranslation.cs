﻿namespace eShopSolution.Data.Entities
{
    public class ProductTranslation
    {
        public int Id { set; get; } 
        public int ProductId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { set; get; }
        public string LanguageId { set; get; }

        public Product Product { set; get; }
        public Language Language { set; get; }

    }
}