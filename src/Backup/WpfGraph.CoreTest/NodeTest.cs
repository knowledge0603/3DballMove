using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Palmmedia.WpfGraph.Core;

namespace Palmmedia.WpfGraph.CoreTest
{
    /// <summary>
    /// Zusammenfassungsbeschreibung für NodeTest
    /// </summary>
    [TestClass]
    public class NodeTest
    {
        public NodeTest()
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
            var obj = new object();

            var node = new Node<object, object>();
            Assert.IsNull(node.Data, "Data invalid.");
            
            node = new Node<object, object>(obj);
            Assert.AreEqual(obj, node.Data, "Data invalid.");
        }

        [TestMethod]
        public void Data_RaisesEvent()
        {
            var node = new Node<object, object>();
            var obj = new object();
            EventArgs args = null;
            int counter = 0;
            node.DataChanged += new EventHandler<EventArgs>((sender, e) => { counter++; args = e; });

            node.Data = obj;

            Assert.AreEqual(1, counter, "Event not raised.");
            Assert.IsNotNull(args, "EventArgs must not be null.");
            Assert.AreEqual(obj, node.Data, "Data invalid.");
        }
    }
}
