using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.UnitTests.Service {
    [TestFixture]
    internal class DocumentServiceTests {
        [Test]
        public void CanFilterMetadata_HasValueBezeichung_HasValue() {
            //arrange
            var documentService = new DocumentService();
            var metadataItem = A.Fake<MetadataItem>();
            //			documentService.MetadataItems = A.Fake<List<MetadataItem>>();
            documentService.MetadataItems = A.CollectionOfDummy<MetadataItem>(10).ToList();

            var type = documentService.MetadataItems[0].Type;
            var searchParam = documentService.MetadataItems[0].Bezeichnung;

            //act
            var result = documentService.FilterMetadataItems(type, searchParam);


            //asset
            Assert.That(result, !Is.Empty);
        }
    }
}
