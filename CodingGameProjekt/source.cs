using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

class Point<T>
{
    public T X;
    public T Y;

    public static void Print (Point<T> Point)
    {
        Console.Error.WriteLine("X: {0} Y: {1}", Point.X, Point.Y);
    }
}

class Solution
{
    static void Main (string[] args)
    {
        string[] inputs;
        int N = int.Parse(Console.ReadLine());
        int M = int.Parse(Console.ReadLine());
        int L = int.Parse(Console.ReadLine());

        List<Point<int>> Spots = new List<Point<int>>();
        List<Point<int>> Orcs = new List<Point<int>>();
        Dictionary<int, Tuple<int, int>> PathPointsIndices = new Dictionary<int, Tuple<int, int>>();
        int StartPointIndice;
        int StopPointIndice;

        for ( int i = 0 ; i < N ; i++ )
        {
            inputs = Console.ReadLine().Split(' ');
            int XS = int.Parse(inputs[0]);
            int YS = int.Parse(inputs[1]);
            Spots.Add(new Point<int> { X = XS, Y = YS });
        }
        for ( int i = 0 ; i < M ; i++ )
        {
            inputs = Console.ReadLine().Split(' ');
            int XO = int.Parse(inputs[0]);
            int YO = int.Parse(inputs[1]);
            Orcs.Add(new Point<int> { X = XO, Y = YO });
        }
        for ( int i = 0 ; i < L ; i++ )
        {
            inputs = Console.ReadLine().Split(' ');
            int N1 = int.Parse(inputs[0]);
            int N2 = int.Parse(inputs[1]);
            PathPointsIndices.Add(i, new Tuple<int, int>(N1, N2));
        }
        StartPointIndice = int.Parse(Console.ReadLine());
        StopPointIndice = int.Parse(Console.ReadLine());

        Console.Error.WriteLine("Starting Point: ");
        Point<int>.Print(Spots[StartPointIndice]);
        Console.Error.WriteLine("Spots Points: ");
        Spots.ForEach(Point<int>.Print);
        PathPointsIndices.ToList().ForEach(a => Console.Error.WriteLine(a.Value));
        Orcs.ForEach(Point<int>.Print);
        Console.Error.WriteLine("Start: {0}.", StartPointIndice);
        Console.Error.WriteLine("Stop: {0}.", StopPointIndice);

        ////Find possible paths
        //Console.Error.WriteLine("Founded routes starting at {0}.", StartPointIndice);
        //var routes = PathPointsIndices.Where(p => p.Value.Item1 == StartPointIndice).ToDictionary(i => i.Key, i => i.Value);
        //routes.ToList().ForEach(a => Console.Error.WriteLine(a.Value));

        ////Check which point is possible to go //path shorter than orcs has
        //routes = routes.Where(r =>
        //    isPossible<int>(Spots[r.Value.Item1], Spots[r.Value.Item2], Orcs) == true
        //).ToDictionary(i => i.Key, i => i.Value);
        //Console.Error.WriteLine("Possible routes");
        //routes.ToList().ForEach(a => Console.Error.WriteLine(a.Value));

        //foreach ( var r in routes )
        //{
        //    Console.Error.WriteLine("Founded routes starting at {0}.", r.Value.Item2);
        //    var _routes = PathPointsIndices.
        //    Where(p => {
        //        return
        //        p.Value.Item1 == r.Value.Item2 &&
        //            isPossible<int>(Spots[p.Value.Item1], Spots[r.Value.Item2], Orcs) == true;
        //    }).ToDictionary(i => i.Key, i => i.Value);
        //    _routes.ToList().ForEach(a => Console.Error.WriteLine(a.Value));

        //    Console.Error.WriteLine("Possible routes");
        //    _routes.ToList().ForEach(a => Console.Error.WriteLine(a.Value));
        //}

        //routes.Keys.ToList().ForEach(r => PathPointsIndices.Remove(r));
        HashSet<string> result = FindPaths(routes: PathPointsIndices, pathPointsIndices: PathPointsIndices,
            orcs: Orcs, spots: Spots, startPointIndice: StartPointIndice, stopPointIndice: StopPointIndice, indices: StartPointIndice.ToString());
        Console.Error.WriteLine("Result:");
        if ( result.Count != 0 )
        {
            result.ToList().ForEach(Console.Error.WriteLine);

            var paths = result.ToList().Select(a => a.Split(' ').ToArray()).ToList();
            var pathsInt = new List<int[]>();
            foreach ( var r in paths )
            {
                pathsInt.Add(
                    Array.ConvertAll(r, e => Int32.Parse(e)));
            }

            var distance = pathsInt.Select(r => r.Aggregate((_e, e) => _e + e)).ToArray();

            for ( int i = 0 ; i < paths.Count ; i++ )
            {
                Console.Error.WriteLine("Dostance for path: {0} = {1}.", paths[i].Aggregate((_e, e) => _e + " " + e), distance[i]);
            }
            var ans = result.OrderBy(o => o.Length).ToList();

            Console.Error.WriteLine("Answer:");
            Console.WriteLine(ans.First());
        }
        else Console.WriteLine("IMPOSSIBLE");

        //Tests
        Console.Error.WriteLine("Tests:");
        Console.Error.WriteLine(CalcDistance<int>(new Point<int> { X = 0, Y = 0 }, new Point<int> { X = 1, Y = 1 }));
        Console.Error.WriteLine(Math.Pow(2, 2));
        //var res = calcDistance<string> (new Point<string>{X="asd",Y="ds"}, new Point<string>{X="asd",Y="ds"});
        Console.ReadKey();
    }

    public static double CalcDistance<T> (Point<T> P1, Point<T> P2)
    {
        dynamic P11 = P1, P22 = P2;
        double dd = Math.Pow(P11.X - P22.X, 2) + Math.Pow(P11.Y - P22.Y, 2);
        return Math.Sqrt(dd);
    }

    public static bool isPossible<T> (Point<T> from, Point<T> to, List<Point<T>> Orcs)
    {
        var d1 = CalcDistance(from, to);
        bool result = true;
        Orcs.ForEach(o =>
        {
            var d2 = CalcDistance(o, to);
            if ( d2 <= d1 ) result = false;
        });
        //return Orcs.Any(o => CalcDistance(o, to) >= d1);

        return result;
    }

    static bool init = false;
    public static HashSet<string> FindPaths (Dictionary<int, Tuple<int, int>> routes, Dictionary<int, Tuple<int, int>> pathPointsIndices,
        List<Point<int>> orcs, List<Point<int>> spots, int startPointIndice, int stopPointIndice, string indices)
    {
        HashSet<string> stringRouts = new HashSet<string>();
        Dictionary<int, Tuple<int, int>> _pathPointsIndices = new Dictionary<int, Tuple<int, int>>(pathPointsIndices);
        Dictionary<int, Tuple<int, int>> _routes = new Dictionary<int, Tuple<int, int>>(routes);
        Console.Error.WriteLine("FindPaths:");

        string _indicies = indices;

        //routes.Keys.ToList().ForEach(r => _pathPointsIndices.Remove(r));
        //Found possibly paths
        if ( !init )
        {
            var _indexesToReverse = _routes.Where(p => p.Value.Item2 == startPointIndice).Select(s => s.Key).ToList();

            Console.Error.Write("Indexes to reverse: \n");
            _indexesToReverse.ForEach(Console.Error.WriteLine);

            _indexesToReverse.ForEach(i =>
            {
                Console.Error.WriteLine("Before Reverse: {0}", _routes[i]);
                _routes[i] = new Tuple<int, int>(_routes[i].Item2, _routes[i].Item1);
                Console.Error.WriteLine("After Reverse: {0}", _routes[i]);
            });
            _routes = routes.Where(e => e.Value.Item1 == startPointIndice).ToDictionary(i => i.Key, i => i.Value);
            _routes.ToList().ForEach(r => _pathPointsIndices.Remove(r.Key));
            init = true;
        }

        foreach ( var r in _routes )
        {
            //Check whetever of given routes already goes to Stop(S) point.
            if ( r.Value.Item2 == stopPointIndice && isPossible<int>(spots[r.Value.Item1], spots[r.Value.Item2], orcs) ||
                 r.Value.Item1 == stopPointIndice && isPossible<int>(spots[r.Value.Item1], spots[r.Value.Item2], orcs) )
            {
                _indicies += " " + r.Value.Item2;
                stringRouts.Add(_indicies);
                Console.Error.WriteLine("Finished succes of route: {0}", _indicies);
                break;
            }
            Console.Error.WriteLine("Founded routes starting at {0}.", r.Value.Item2);
            //Find possibly paths
            var indexesToReverse = _pathPointsIndices.Where(p => p.Value.Item2 == r.Value.Item2).Select(s => s.Key).ToList();
            Console.Error.Write("Indexes to reverse: \n");
            indexesToReverse.ForEach(Console.Error.WriteLine);

            indexesToReverse.ForEach(i =>
            {
                Console.Error.WriteLine("Before Reverse: {0}", _pathPointsIndices[i]);
                _pathPointsIndices[i] = new Tuple<int, int>(_pathPointsIndices[i].Item2, _pathPointsIndices[i].Item1);
                Console.Error.WriteLine("After Reverse: {0}", _pathPointsIndices[i]);
            });

            _routes = _pathPointsIndices.Where(p => p.Value.Item1 == r.Value.Item2 &&
                                                        isPossible<int>(spots[p.Value.Item1], spots[p.Value.Item2], orcs) == true)
                .ToDictionary(i => i.Key, i => i.Value);

            _routes.ToList().ForEach(e => _pathPointsIndices.Remove(e.Key));
            Console.Error.WriteLine("Possible routes");
            _routes.ToList().ForEach(a => Console.Error.WriteLine(a));

            if ( _routes.Count != 0 )
            {
                stringRouts.UnionWith(
                    FindPaths(_routes, _pathPointsIndices,
                        orcs, spots, startPointIndice, stopPointIndice, _indicies + " " + r.Value.Item2)
                );
            }

            Console.Error.WriteLine("Finished of route: {0}", _indicies);

        }

        return stringRouts;
    }
}