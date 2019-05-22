using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Newtonsoft.Json;

namespace Neo4jProject.database
{
    class InitiateData:IDisposable
    {
        private readonly IDriver _driver;

        public InitiateData(string uri, string username, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
            InitiateDatabase();
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public void InitiateDatabase()
        {
            using (var session = _driver.Session())
            {
                session.WriteTransaction(tx =>
                {
                    tx.Run("Load csv with headers from " + @"file:///producers.csv" + "as n "+
                            "create(s: Producers)" +
                            "set s = n;");
                    tx.Run("Load csv with headers from " + @"file:///PrimaryConsumers.csv" + "as n " +
                             "create(s: Producers)" +
                             "set s = n;");
                    tx.Run("Load csv with headers from " + @"file:///SecondaryConsumers.csv" + "as n " +
                            "create(s: Producers)" +
                            "set s = n;");
                    tx.Run("Load csv with headers from " + @"file:///TertiaryConsumers.csv" + "as n " +
                            "create(s: Producers)" +
                            "set s = n;");
                    tx.Run("Match (p:Producers),(pr:PrimaryConsumers)" +
                        "Create(p) -[:Provide]->(pr)" +
                        "Create(pr) -[:Consume]->(p)");
                    tx.Run("Match (p:PrimaryConsumers),(s:SecondaryConsumers)" +
                        "Create(p) -[:Provide]->(s)" +
                        "Create(s) -[:Consume]->(p)");
                    tx.Run("Match(s: SecondaryConsumers), (t: TertiaryConsumers)" +
                        "Create(s) -[:Provide]->(t)" +
                        "Create(t) -[:Consume]->(s)");
                });
            }
        }



    }
}
