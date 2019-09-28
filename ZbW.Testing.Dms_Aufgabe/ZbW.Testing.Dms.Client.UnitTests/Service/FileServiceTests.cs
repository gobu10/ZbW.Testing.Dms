using System;
using System.IO;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.UnitTests.Service {
    [TestFixture]
    internal class FileServiceTests {
        private string SOURCE_PATH = "some_source_sample_path";
        private string TARGET_PATH = "some_target_sample_path";

        [Test]
        public void CopyDocument_IsCallCorrect_CallsCopyCorrect() {
            //arrage
            var sutFileService = new FileService();

            // Es wird entwas ausgeführt. Mit *mock enden damit Sichergestellt ist, dass nur ein Mock verwendet wird
            var fileTestableMock = A.Fake<FileTestable>();  // MOCK

            // Standard Wert von Proeprty Injection wird überschieben
            // FileTestable mit Mock überschreiben
            sutFileService.FileTestable = fileTestableMock;

            //act
            sutFileService.CopyDocumentToTarge(SOURCE_PATH, TARGET_PATH);

            //assert
            // Wenn ein FakeItEasy-Mock geprüft werden soll kann mit FakeItEasy getestet werden
            A.CallTo(() => fileTestableMock.Copy(SOURCE_PATH, TARGET_PATH, true))
                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void CopyDocument_handleIOExceptionCorrect_ThrowException() {
            //arrage
            var sutFileService = new FileService();
            var fileTestableStub = A.Fake<FileTestable>(); // STUB

            sutFileService.FileTestable = fileTestableStub;

            // Wenn dieser Aufruf gemacht wird (egal mit welchem String), dann wird eine Exception geworfen
            // anstelle von Ignored könnnen auch explizite Strings angegeben werden
            A.CallTo(() => fileTestableStub.Copy(A<string>.Ignored, A<string>.Ignored, A<Boolean>.Ignored)).Throws<IOException>();

            //act 
            // durch TestDelegate wird der Aufruf nicht ausgeführt, nur in eine Funktion geschrieben
            void TestDelegate() => sutFileService.CopyDocumentToTarge(SOURCE_PATH, TARGET_PATH);

            //assert
            Assert.That(TestDelegate, Throws.Exception.TypeOf<CouldNotCopyFileException>());
            Assert.That(TestDelegate, Throws.Exception.InnerException.TypeOf<IOException>());
        }
    }
}
