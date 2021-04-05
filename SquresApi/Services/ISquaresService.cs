using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface ISquaresService
    {
        Task<SquaresMetadata> GetSquares(Guid pointsListId);
        Task CalculateSquares(List<Point> points, Guid pointsListId);
        Task SaveSquares(Guid pointsListId, List<Square> squares);
    }
}
