using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.CoreTest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für EdgeTest
    /// </summary>
    [TestClass]
    public class EdgeTest
    {
        public EdgeTest()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Ruft den Textkontext mit Informationen über
        ///den aktuellen Testlauf sowie Funktionalität für diesen auf oder legt diese fest.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Zusätzliche Testattribute
        //
        // Sie können beim Schreiben der Tests folgende zusätzliche Attribute verwenden:
        //
        // Verwenden Sie ClassInitialize, um vor Ausführung des ersten Tests in der Klasse Code auszuführen.
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Verwenden Sie ClassCleanup, um nach Ausführung aller Tests in einer Klasse Code auszuführen.
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Mit TestInitialize können Sie vor jedem einzelnen Test Code ausführen. 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Mit TestCleanup können Sie nach jedem einzelnen Test Code ausführen.
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ConstructorSetsData()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();            
            var obj = new object();

            var edge = new Edge<object, object>(node1, node2);
            Assert.IsNull(edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.OmniDirectional, edge.EdgeDirection, "EdgeDirection invalid.");
            
            edge = new Edge<object, object>(node1, node2, obj);
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.OmniDirectional, edge.EdgeDirection, "EdgeDirection invalid.");
            
            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second);
            Assert.IsNull(edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.First2Second, edge.EdgeDirection, "EdgeDirection invalid.");

            edge = new Edge<object, object>(node1, node2, EdgeDirection.First2Second, obj);
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
            Assert.AreEqual(node1, edge.FirstNode, "FirstNode invalid.");
            Assert.AreEqual(node2, edge.SecondNode, "SecondNode invalid.");
            Assert.AreEqual(EdgeDirection.First2Second, edge.EdgeDirection, "EdgeDirection invalid.");
        }

        [TestMethod]
        public void Data_RaisesEvent()
        {
            var node1 = new Node<object, object>();
            var node2 = new Node<object, object>();
            var edge = new Edge<object, object>(node1, node2);
            var obj = new object();
            EventArgs args = null;
            int counter = 0;
            edge.DataChanged += new EventHandler<EventArgs>((sender, e) => { counter++; args = e; });

            edge.Data = obj;

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
            Assert.AreEqual(obj, edge.Data, "Data invalid.");
        }
    }
}
