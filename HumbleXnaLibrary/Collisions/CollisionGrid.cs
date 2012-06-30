using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Humble.Collisions
{
    public class CollisionGrid<T>
    {

        private List<T> _all;
        private List<List<T> > _grid;
        private Action<T, T> _checkCollision;
        private Func<T, Vector2> _getPosition;
        private int _divisions, _divisionSize;
        private int _width, _height;

        public CollisionGrid(int divisions, int fieldWidth, int fieldHeight, Action<T, T> collisionChecker, Func<T, Vector2> positionGetter)
        {
            _checkCollision = collisionChecker;
            _getPosition = positionGetter;
            _divisions = divisions;
            _divisionSize = fieldWidth / divisions;
            _width = fieldWidth;
            _height = fieldHeight;
            _grid = new List<List<T>>(divisions);
            _all = new List<T>();
            for (int i = 0; i < divisions; i++)
                _grid.Add(null);

        }

        private int GetDivision(T a)
        {
            Vector2 pos = _getPosition(a);

            if (pos.X > _width)
                return -1;

            return (int)(pos.X / (float)_divisionSize);
        }

        public void Add(T a)
        {
            if (a == null)
                return;

            int div = GetDivision(a);

            // TODO : out of bounds elements should be added 
            if (div < 0 || div >= _divisions)
                return;

            if (_grid[div] == null)
                _grid[div] = new List<T>();

            _all.Add(a);
            _grid[div].Add(a);
        }

        public bool Remove(T a)
        {
            int div = GetDivision(a);
            if (div > 0 && div < _divisions && _grid[div] != null && _grid[div].Remove(a))
            {
                _all.Remove(a);
                return true;
            }

            // The object position has changed since last update
            foreach (List<T> l in _grid)
            {
                if (l != null && l.Remove(a))
                {
                    _all.Remove(a);
                    return true;
                }
            }
            return false;
        }

        private void CheckCollisionWithSection(T a, int section)
        {
            if (section < 0 || section >= _divisions || _grid[section] == null)
                return;
            foreach (T other in _grid[section])
            {
               if (a.Equals(other) == false)
                  _checkCollision(a, other);
            }
        }

        public List<T> GetAllElements()
        {
            return _all;
        }

        public void ProcessCollisions()
        {
            // REPOSITION ELEMENTS
            for (int i = 0; i < _divisions; i++)
            {
                if (_grid[i] == null)
                    continue;
                for (int j = 0; j < _grid[i].Count; j++ )
                {
                    T a = _grid[i][j];
                    int div = GetDivision(a);
                    if (div != i && div >= 0 && div < _divisions)
                    {
                        _grid[i].Remove(a);
                        if (_grid[div] == null)
                            _grid[div] = new List<T>();
                        _grid[div].Add(a);
                        j--;
                    }
                }
            }
            // CHECK COLLISION IN EACH SECTION
            for (int i = 0; i < _divisions; i++)
            {
                if (_grid[i] == null)
                    continue;
                for (int j = 0; j < _grid[i].Count; j++)
                {
                    T a = _grid[i][j];
                    CheckCollisionWithSection(a, i - 1);
                    CheckCollisionWithSection(a, i);
                    CheckCollisionWithSection(a, i + 1);
                }
            }
        }


    }
}
