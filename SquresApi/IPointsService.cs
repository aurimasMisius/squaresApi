using System;

namespace Squares.Services
{
    public interface IPointsService
    {
        public int AddNewList();
        public void AddPoint(int listId);
        public void RemovePoint(int listId);
    }
}
