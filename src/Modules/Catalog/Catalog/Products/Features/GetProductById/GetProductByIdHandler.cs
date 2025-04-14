﻿namespace Catalog.Products.Features.GetProductById
{
    internal class GetProductByIdHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken) 
                ?? throw new ProductNotFoundException(query.ProductId);

            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
