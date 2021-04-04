using System;
using Models;
using System.Collections.Generic;

namespace Services
{
    public class PointsService : IPointsService
    {
        public PointsService()
        {
        }

        public Guid AddNewList(List<Point> points)
        {
            try
            {
                Guid id = new Guid();
                //TODO: write to database
                
                return id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddPoint(Guid listId)
        {
            try
            {
                //todo: update database with new point
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemovePoint(Guid listId)
        {
            try
            {
                //todo: uodate database removing point
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Point> GetPoints(Guid listId)
        {
            List<Point> points = new();

            return points;
        }
    }
}