using System;

namespace Squares.Services
{
    public class PointsService : IPointsService
    {
        public PointsService()
        {
        }
        
        public int AddNewList()
        {
        }
        
        public void AddPoint(int listId)
        {
            List<Point> points = GetPoints(listId);
        }
        
        public void RemovePoint(int listId)
        {
            List<Point> points = GetPoints(listId);
        }

        private List<Point> GetPoints(int listId)
        {
            List<Point> points = new();
            return points;
        }
    }
}