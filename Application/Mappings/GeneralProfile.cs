using Application.Features.Products.Commands.CreateProduct;
using Application.Features.LatLongs.Commands.UpdateLatlong;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Features.Users.Queries;
using Application.Features.LatLongs.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<UpdateLatlongCommand, latlonglist>();
            CreateMap<GetUserByIdQuery, UserMaster>();
            CreateMap<getLatLongreportQuery, latlongreport>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
        }
    }
}

