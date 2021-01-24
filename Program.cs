using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortestPath
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();

            List<(Employee, Employee)> OrgChart = new List<(Employee, Employee)>()
            {
                (Employee.Eugene, Employee.Jose),
                (Employee.Eugene, Employee.Kelvin),
                (Employee.Eugene, Employee.Terence),
                (Employee.Jose, Employee.Dennis),
                (Employee.Jose, Employee.Eunice),
                (Employee.Jose, Employee.Bryan),
                (Employee.Eunice, Employee.Gabriel),
                (Employee.Eunice, Employee.Jimmy)
            };

            graph.BuildGraph(OrgChart);

            void PrintShortestPath(Employee src, Employee dest)
            {
                var result = graph.BFS(src, dest);

                if (result == null || !result.Any())
                    Console.WriteLine("No paths found");

                Console.WriteLine($"shortestPath ({src}, {dest}) = {string.Join(" > ", result)}");
            }

            PrintShortestPath(Employee.Gabriel, Employee.Jimmy);
            PrintShortestPath(Employee.Jimmy, Employee.Bryan);
            PrintShortestPath(Employee.Jimmy, Employee.Kelvin);
            PrintShortestPath(Employee.Eunice, Employee.Jimmy);

            Console.ReadLine();
        }
    }

    public class Graph
    {
        Dictionary<Node, List<Node>> organisationChart = new Dictionary<Node, List<Node>>();

        public void BuildGraph(List<(Employee parent, Employee child)> orgChart)
        {
            /* Build Dictionary<employee, list of related employees>

               Key: Eugene, Value: [Jose, Kelvin, Terence]
               Key: Jose, Value: [Eugene, Dennis, Eunice, Bryan]
               Key: Kelvin, Value: [Eugene]
               Key: Terence, Value: [Eugene]
               Key: Dennis, Value: [Jose]
               Key: Eunice, Value: [Jose]
               Key: Bryan, Value: [Jose]
               Key: Gabriel, Value: [Eunice]
               Key: Jimmy, Value: [Eunice]
            */

            foreach (var edge in orgChart)
            {
                if (!organisationChart.ContainsKey(new Node(edge.parent)))
                    organisationChart.Add(new Node(edge.parent), new List<Node>());

                organisationChart[new Node(edge.parent)].Add(new Node(edge.child));

                if (!organisationChart.ContainsKey(new Node(edge.child)))
                    organisationChart.Add(new Node(edge.child), new List<Node>());

                organisationChart[new Node(edge.child)].Add(new Node(edge.parent));
            }
        }

        public Dictionary<Node, List<Node>> GetGraph()
        {
            return organisationChart;
        }

        // Get shortest path from src to dest
        public List<string> BFS(Employee src, Employee dest)
        {
            // store visited nodes
            var visited = new HashSet<Node>();

            var queue = new Queue<Node>();

            if (!organisationChart.ContainsKey(new Node(src)) || !organisationChart.ContainsKey(new Node(dest)))
                return null;

            queue.Enqueue(new Node(src));

            var paths = new List<string>();

            // loop till queue is empty
            while (queue.Any())
            {
                var node = queue.Dequeue();

                // skip if node is visited before
                if (!visited.Contains(node))
                {
                    // mark current node as visited
                    visited.Add(node);

                    paths.Add(node.Employee.ToString());

                    if (organisationChart[node].Contains(new Node(dest)))
                    {
                        // found
                        paths.Add(dest.ToString());

                        return paths;
                    }
                    else
                    {
                        // get adjacent nodes
                        foreach (var adjacentNode in organisationChart[node])
                        {
                            if (!visited.Contains(adjacentNode))
                                queue.Enqueue(adjacentNode);
                        }

                        if (organisationChart[node].Count() == 1 && visited.Contains(organisationChart[node][0]))
                        {
                            paths.Remove(node.Employee.ToString());
                        }
                    }
                }
            }

            return null;
        }
    }

    public class Node
    {
        public Employee Employee { get; private set; }

        public Node(Employee employee)
        {
            Employee = employee;
        }

        public override int GetHashCode() => (int)Employee;
        public override bool Equals(object other) => (other as Node)?.Employee == Employee;
    }

    public enum Employee
    {
        Eugene = 1,
        Jose,
        Kelvin,
        Terence,
        Dennis,
        Eunice,
        Bryan,
        Gabriel,
        Jimmy
    }
}
