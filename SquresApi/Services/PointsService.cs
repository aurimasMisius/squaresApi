using Models;
using Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class PointsService : IPointsService
    {
        private readonly PointsRepository _pointsRepository;

        public PointsService(PointsRepository pointsRepository)
        {
            _pointsRepository = pointsRepository;
        }

        public async Task<Guid> AddNewList(List<Point> points)
        {
            Guid id = Guid.NewGuid();
            PointsMetadata pointsMetadata = CreateMetadata(id, points);
            await _pointsRepository.Create(pointsMetadata).ConfigureAwait(false);
                
            return id;
        }

        public async Task AddPoint(Guid listId, Point point)
        {
            PointsMetadata pointsMetadata = await _pointsRepository.Find(listId);
            pointsMetadata.Points.Add(point);
            await _pointsRepository.Update(pointsMetadata).ConfigureAwait(false);
        }

        public async Task RemovePoint(Guid listId, Point point)
        {
            PointsMetadata pointsMetadata = await _pointsRepository.Find(listId);
            List<Point> pointsToRemove = pointsMetadata.Points.Where(x => x.X == point.X && x.Y == point.Y).ToList();
            foreach (Point pointToRemove in pointsToRemove)
            {
                pointsMetadata.Points.Remove(pointToRemove);
            }
            await _pointsRepository.Update(pointsMetadata).ConfigureAwait(false);
        }

        public async Task<List<Point>> GetPoints(Guid listId)
        {
            PointsMetadata pointsMetadata = await _pointsRepository.Find(listId).ConfigureAwait(false);
            return pointsMetadata.Points;
        }

        private PointsMetadata CreateMetadata(Guid id, List<Point> points)
        {
            return new PointsMetadata
            {
                Id = id,
                Points = points
            };
        }
    }
}