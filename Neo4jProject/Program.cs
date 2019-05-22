using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jProject;

namespace Neo4jProject
{
    class EcosystemFoodChain
    {

        static void Main(string[] args)
        {
            using (var test = new DatabaseExecution("bolt://localhost:7687", "neo4j", "nhatphong671998"))
            {
                string query = "match(p:Producers) where p.Name='Grasses' return p";
                test.PrintProducer(test.GetProducer(query));
            }
            Console.ReadKey();
        }
    }
}
