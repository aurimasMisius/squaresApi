using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IPointsService
    {
        public Guid AddNewList(List<Point> points);
        public void AddPoint(Guid listId);
        public void RemovePoint(Guid listId);
        public List<Point> GetPoints(Guid listId);
    }
}
