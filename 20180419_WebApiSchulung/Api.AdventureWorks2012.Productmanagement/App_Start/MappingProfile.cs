using System;
using Api.AdventureWorks2012.Productmanagement.Models;
using Api.AdventureWorks2012.Productmanagement.ViewModels;
using AutoMapper;

namespace Api.AdventureWorks2012.Productmanagement.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain nach ViewModel mappen
            CreateMap<Product, ProductViewModel>(); // Wenn die Properties gleich heißen werden sie automatisch gemappt!

            // ViewModel nach Domain mappen
            CreateMap<ProductViewModel, Product>()
                .ForMember(p => p.ProductID, opt => opt.Ignore()) // Die ProductId wird beim Mapping ignoriert
                // Defaultvalues für NOT NULL-Spalten bzw. CHECK CONSTRAINTs auf der DB:
                .ForMember(p => p.SellStartDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(p => p.ModifiedDate, opt => opt.UseValue(DateTime.Now))
                .ForMember(p => p.SafetyStockLevel, opt => opt.UseValue(1000))
                .ForMember(p => p.ReorderPoint, opt => opt.UseValue(1000))
                .ForMember(p => p.StandardCost, opt => opt.UseValue(0))
                .ForMember(p => p.ListPrice, opt => opt.UseValue(0))
                .ForMember(p => p.DaysToManufacture, opt => opt.UseValue(1))
                .ForMember(p => p.rowguid, opt => opt.UseValue(Guid.NewGuid()));
        }
    }
}