using Discount_gRPC.Data;
using Discount_gRPC.Models;
using Discount_gRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount_gRPC.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : Protos.DiscountService.DiscountServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
            if(coupon == null)
                coupon = new Coupon { ProductName = "No Discount", Description = "No discount available", Amount = 0 };

            logger.LogInformation("Discount retrieved for ProductName: {ProductName}, Amount: {Amount}", coupon.ProductName, coupon.Amount);

            var cuponModel = coupon.Adapt<CouponModel>();
            return cuponModel;
        }
        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var cupon = request.Coupon.Adapt<Coupon>();
            if(cupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));
            await dbContext.AddAsync(cupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount created for ProductName: {ProductName}, Amount: {Amount}", cupon.ProductName, cupon.Amount);
            var couponModel = cupon.Adapt<CouponModel>();
            return couponModel;
        }
        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var cupon = request.Coupon.Adapt<Coupon>();
            if (cupon == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Coupon is null"));
            dbContext.Coupons.Update(cupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount updated for ProductName: {ProductName}, Amount: {Amount}", cupon.ProductName, cupon.Amount);
            var couponModel = cupon.Adapt<CouponModel>();
            return couponModel;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName);
            if(coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName={request.ProductName} is not found."));
            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount deleted for ProductName: {ProductName}", request.ProductName);
            return new DeleteDiscountResponse { Success = true };
        }
    }
}