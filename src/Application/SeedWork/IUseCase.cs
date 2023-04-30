namespace Application.SeedWork;
public interface IUseCase<TIn, TOut>
{
    public Task<TOut> Handle(TIn input, CancellationToken cancellationToken);
}
