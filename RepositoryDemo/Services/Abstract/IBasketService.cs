using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Dtos;
using RepositoryDemo.Results;
using IResult = RepositoryDemo.Results.IResult;

namespace RepositoryDemo.Services.Abstract;

public interface IBasketService
{
    Task<IDataResult<ResponseBasketIndexDto>> BasketIndex();
    Task<IResult> AddBasket(int? id, int addProductCount);
    Task<IDataResult<ProductCountPlusDto>> ProductCountPlus(int id);
    Task<IDataResult<ProductCountMinusDto>> ProductCountMinus(int id);
    Task<IDataResult<BasketRemoveDto>> RemoveProduct(int id);

}