using System;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mongo;

namespace Services
{
    public class SquaresService : ISquaresService
    {
        private readonly SquaresRepository _squaresRepository;

        public SquaresService(SquaresRepository squaresRepository)
        {
            _squaresRepository = squaresRepository;
        }

        public async Task CalculateSquares(List<Point> points, Guid pointsListId)
        {
            List<Square> squares = new List<Square>();

            for(int i = 0; i <= points.Count - 4; i++)
            {
                for(int j = 1; j <= points.Count - 3; j++)
                {
                    for (int k = 2; k <= points.Count - 2; k++)
                    {
                        for (int l = 3; l <= points.Count - 1; l++)
                        {
                            bool isSquare = CheckIfSquare(points[i], points[j], points[k], points[l]);
                            if (isSquare)
                            {
                                squares.Add(new Square(points[i], points[j], points[k], points[l]));
                            }
                        }
                    }
                }
            }

            SquaresMetadata squaresMetadata = CreateMetadata(pointsListId, squares);
            await _squaresRepository.Create(squaresMetadata).ConfigureAwait(false);
        }

        public async Task<SquaresMetadata> GetSquares(Guid pointsListId)
        {
            return await _squaresRepository.Find(pointsListId).ConfigureAwait(false);
        }

        public async Task SaveSquares(Guid pointsListId, List<Square> squares)
        {
            SquaresMetadata squaresMetadata = CreateMetadata(pointsListId, squares);
            await _squaresRepository.Create(squaresMetadata).ConfigureAwait(false);
        }

        private SquaresMetadata CreateMetadata(Guid id, List<Square> squares)
        {
            return new SquaresMetadata
            {
                Id = id,
                Squares = squares,
                Count = squares.Count
            };
        }

        private static int GetDistance(Point point1, Point point2)
        {
            return (point1.X - point2.X) * (point1.X - point2.X) + (point1.Y - point2.Y) * (point1.Y - point2.Y);
        }

        private static bool CheckIfSquare(Point p1, Point p2, Point p3, Point p4)
        {
            int d2 = GetDistance(p1, p2); // from p1 to p2
            int d3 = GetDistance(p1, p3); // from p1 to p3
            int d4 = GetDistance(p1, p4); // from p1 to p4

            if (d2 == 0 || d3 == 0 || d4 == 0)
                return false;

            // If lengths if (p1, p2) and (p1, p3) are same, then
            // following conditions must met to form a square.
            // 1) Square of length of (p1, p4) is same as twice
            // the square of (p1, p2)
            // 2) Square of length of (p2, p3) is same
            // as twice the square of (p2, p4)
            if (d2 == d3 && 2 * d2 == d4
                         && 2 * GetDistance(p2, p4) == GetDistance(p2, p3))
            {
                return true;
            }

            // The below two cases are similar to above case
            if (d3 == d4 && 2 * d3 == d2
                         && 2 * GetDistance(p3, p2) == GetDistance(p3, p4))
            {
                return true;
            }
            if (d2 == d4 && 2 * d2 == d3
                         && 2 * GetDistance(p2, p3) == GetDistance(p2, p4))
            {
                return true;
            }
            return false;
        }
    }
}
