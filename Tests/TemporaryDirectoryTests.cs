﻿using Koek;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Tests
{
    [TestClass]
    public sealed class TemporaryDirectoryTests : BaseTestClass
    {
        [TestMethod]
        public void TemporaryFolderIsCreatedAndRemoved()
        {
            string temporaryDirectory;

            using (var folder = new TemporaryDirectory())
            {
                temporaryDirectory = folder.Path;
                Assert.IsTrue(Directory.Exists(folder.Path));
            }

            Assert.IsFalse(Directory.Exists(temporaryDirectory));
        }

        [TestMethod]
        public void Initializer_WithCustomPrefix_CreatesDirectoryWithCustomPrefix()
        {
            const string prefix = "ez5b8 y5ekht ";

            using (var folder = new TemporaryDirectory(prefix))
            {
                Assert.IsTrue(Path.GetFileName(folder.Path).StartsWith(prefix));
            }
        }

        [TestMethod]
        public void TemporaryFolder_WithCustomParent_IsCreatedAndRemoved()
        {
            var parentPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

            string temporaryDirectory;

            using (var folder = TemporaryDirectory.WithParentDirectory(parentPath))
            {
                temporaryDirectory = folder.Path;
                Assert.IsTrue(Directory.Exists(folder.Path));

                Assert.AreEqual(1, Directory.GetDirectories(parentPath).Length);
            }

            Assert.IsFalse(Directory.Exists(temporaryDirectory));
        }

    }
}