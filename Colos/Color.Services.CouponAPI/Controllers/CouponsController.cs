using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Color.Services.CouponAPI.Data;
using Color.Services.CouponAPI.Models;
using Color.Services.CouponAPI.Models.DTOs;

namespace Color.Services.CouponAPI.Controllers
{
    //TODO: change all existing controllers if by the end of the course they include all the business logic in themselves - add repositories, services, separate request and response models, UoW, try to make DDD  
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ResponseDto _response;

        public CouponsController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        // GET: api/Coupons
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetCoupons()
        {
            try
            {
                _response.Result = await _context.Coupons.Select(model => _mapper.Map<CouponDto>(model)).ToListAsync();
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }
            return _response;
        }

        // GET: api/Coupons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto>> GetCoupon(int id)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(await _context.Coupons.FirstAsync(model => model.CouponId == id));
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        // GET: api/Coupons/GetByCode/5
        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<ResponseDto>> GetCouponByCode(string code)
        {
            try
            {
                _response.Result = _mapper.Map<CouponDto>(await _context.Coupons.FirstOrDefaultAsync(model => model.CouponCode == code));
                if (_response.Result == null)
                {
                    _response.IsSuccessful = false;
                }
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        // PUT: api/Coupons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> PutCoupon(CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);

                _context.Update(coupon);

                await _context.SaveChangesAsync();

                _response.Result = coupon;
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        // POST: api/Coupons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> PostCoupon(CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);

                await _context.Coupons.AddAsync(coupon);
                await _context.SaveChangesAsync();

                _response.Result=coupon;
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }

            return _response;
        }

        // DELETE: api/Coupons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteCoupon(int id)
        {
            try
            {
                var coupon = await _context.Coupons.FirstAsync(model => model.CouponId == id);

                _context.Coupons.Remove(coupon);

                await _context.SaveChangesAsync();

                _response.Result=coupon;
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                _response.Message = e.Message;
            }

            return _response;
        }
    }
}
