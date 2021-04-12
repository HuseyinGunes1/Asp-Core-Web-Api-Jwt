using JWT.CORE.Repository;
using JWT.CORE.Services;
using JWT.SHARED.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JWT.SERVİCE.Services
{
    public class ServicesGeneric<T, TDto> : IServiceGeneric<T, TDto> where T : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<T> _genericRepository;
        public ServicesGeneric(IUnitOfWork unitOfWork,IGenericRepository<T> genericRepository )      
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var NewEntity = ObjectMapper.MapperDeneme.Map<T>(entity);//gelen dto yu entitye çevirme
            await _genericRepository.AddAsync(NewEntity);
            await _unitOfWork.CommitAsync();
            var NewDto = ObjectMapper.MapperDeneme.Map<TDto>(NewEntity);//ıd veritabanına kaydedildi daha sonra cliente dto yu dönecem entity i dto ya çevirdim
            return Response<TDto>.Success(NewDto, 200);

        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var Product = ObjectMapper.MapperDeneme.Map<List<TDto>>(await _genericRepository.GetAllAsync());//gelen entityi maperladık
            return Response<IEnumerable<TDto>>.Success(Product, 200);//dto nesnesini geriye dönderdik
        }

        public async Task<Response<TDto>> GetIdAsync(int id)
        {
            var Product = await _genericRepository.GetIdAsync(id);
            if (Product == null)
            {
                return Response<TDto>.Fail("Id Bulunamadı", 404, true);
            }
            return Response<TDto>.Success(ObjectMapper.MapperDeneme.Map<TDto>(Product),200);
        }

        public async Task<Response<NoDataDto>> Remove(int id)
        {
            var IsExistEntity = await _genericRepository.GetIdAsync(id);
            if (IsExistEntity == null)
            {
                return  Response<NoDataDto>.Fail("Belirtilen Ürün Bulunamadı", 404,true);
            }
            _genericRepository.Remove(IsExistEntity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);
        }

        public async Task<Response<NoDataDto>> Update(TDto entity,int id)
        {
            var IsExistEntity = await _genericRepository.GetIdAsync(id);
            if (IsExistEntity == null)
            {
                return Response<NoDataDto>.Fail("Ürün Bulunamadı", 400,true);
            }
            var Entity = ObjectMapper.MapperDeneme.Map<T>(entity);
            _genericRepository.Update(Entity);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(204);//gövde olmadığından 204 döner

        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<T, bool>> predicate)
        {
            //where(x=>x.id) //x func taki entity id ise bool değeri
            var list = _genericRepository.Where(predicate);
            return Response<IEnumerable<TDto>>.Success(ObjectMapper.MapperDeneme.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
