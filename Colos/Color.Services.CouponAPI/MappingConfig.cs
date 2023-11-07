using AutoMapper;
using Color.Services.CouponAPI.Models;
using Color.Services.CouponAPI.Models.DTOs;

namespace Color.Services.CouponAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        return  new MapperConfiguration(config =>
        {
            config.CreateMap<Coupon, CouponDto>();
            config.CreateMap<CouponDto, Coupon>();
        });
    }
}