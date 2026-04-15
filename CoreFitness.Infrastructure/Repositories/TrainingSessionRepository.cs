using CoreFitness.Domain.Entities.TrainingSessions;
using CoreFitness.Domain.Entities.TrainingSessions.ValueObjects;
using CoreFitness.Domain.Interfaces.TrainingSessions;

namespace CoreFitness.Infrastructure.Repositories
{
    public class TrainingSessionRepository(CoreFitnessDbContext context) : BaseRepository<TrainingSession, TrainingSessionId>(context),
        ITrainingSessionRepository
    {
        public Task<IEnumerable<TrainingSession>> GetUpcomingAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
