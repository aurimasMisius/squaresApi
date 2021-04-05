using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Services
{
    public interface IPointsService
    {
        public Task<Guid> AddNewList(List<Point> points);
        public Task AddPoint(Guid listId, Point point);
        public Task RemovePoint(Guid listId, Point point);
        public Task<List<Point>> GetPoints(Guid listId);
    }
}
