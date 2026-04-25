using CoreFitness.Application.DTOs;

namespace CoreFitness.Application.Interfaces;

public interface IQuoteService
{
    Task<QuoteDTO?> GetRandomQuoteAsync();
}
