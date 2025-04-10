namespace Catalog.Products.Features.GetProductById
{
    public record GetProductByIdQuery(Guid ProductId)
        : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(ProductDto Product);

    internal class GetProductByIdHandler(CatalogDbContext dbContext)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.Id == query.ProductId, cancellationToken) 
                ?? throw new Exception($"Product not found: {query.ProductId}");

            var productDto = product.Adapt<ProductDto>();

            return new GetProductByIdResult(productDto);
        }
    }
}
